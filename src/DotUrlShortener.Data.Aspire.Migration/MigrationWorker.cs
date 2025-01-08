using System.Diagnostics;
using DotUrlShortener.Data.DbContexts;
using DotUrlShortener.Data.Entities;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Trace;

namespace DotUrlShortener.Data.Aspire.Migration;

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

            var dbContext = scope.ServiceProvider.GetRequiredService<DotUrlShortenerDbContext>();

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

    private static async Task SeedDataAsync(
        DotUrlShortenerDbContext dbContext,
        CancellationToken cancellationToken
    )
    {
        ShortenedUrl firstTicket = new() { Path = "Hello", CreationDateTime = DateTime.UtcNow };

        var strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            // Seed the database
            await using var transaction = await dbContext.Database.BeginTransactionAsync(
                cancellationToken
            );
            await dbContext.ShortenedUrls.AddAsync(firstTicket, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        });
    }
}
