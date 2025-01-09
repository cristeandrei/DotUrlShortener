using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using OpenTelemetry.Trace;
using ReduceUrl.Data.DbContexts;
using ReduceUrl.Data.Models;

namespace ReduceUrl.Data.Aspire.Migration;

internal sealed class MigrationWorker(
    IServiceProvider serviceProvider,
    IHostApplicationLifetime hostApplicationLifetime
) : BackgroundService
{
    public const string ActivitySourceName = "Migrations";
    private static readonly ActivitySource ActivitySource = new(ActivitySourceName);

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        using var activity = ActivitySource.StartActivity(ActivityKind.Client);

        try
        {
            using var scope = serviceProvider.CreateScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<ReduceUrlDbContext>();

            await EnsureDatabaseAsync(dbContext, cancellationToken);

            await dbContext.Database.MigrateAsync(cancellationToken);

            await SeedDataAsync(dbContext, cancellationToken);
        }
        catch (Exception ex)
        {
            activity?.RecordException(ex);
            throw;
        }

        hostApplicationLifetime.StopApplication();
    }

    private static async Task EnsureDatabaseAsync(
        ReduceUrlDbContext dbContext,
        CancellationToken cancellationToken
    )
    {
        var dbCreator = dbContext.GetService<IRelationalDatabaseCreator>();

        var strategy = dbContext.Database.CreateExecutionStrategy();

        await strategy.ExecuteAsync(async () =>
        {
            // Create the database if it does not exist.
            // Do this first so there is then a database to start a transaction against.
            // Also create __EFMigrationsHistory if it doesn't exist, ef core will check it when executing the migration
            // If it doesn't find the table, it will create it. But ef wil also log an error, and we want to avoid that since its a false positive
            if (!await dbCreator.ExistsAsync(cancellationToken))
            {
                await dbCreator.CreateAsync(cancellationToken);

                await dbContext.Database.ExecuteSqlInterpolatedAsync(
                    $"""
                    CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
                    "MigrationId" text NOT NULL,
                    "ProductVersion" text NOT NULL,
                    CONSTRAINT "PK_HistoryRow" PRIMARY KEY ("MigrationId"));
                    """,
                    cancellationToken
                );
            }
        });
    }

    private static async Task SeedDataAsync(
        ReduceUrlDbContext dbContext,
        CancellationToken cancellationToken
    )
    {
        var firstTicket = new ReducedUrl() { Path = "Hello", CreationDateTime = DateTime.UtcNow };

        var strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            // Seed the database
            await using var transaction = await dbContext.Database.BeginTransactionAsync(
                cancellationToken
            );
            await dbContext.ReducedUrls.AddAsync(firstTicket, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        });
    }
}
