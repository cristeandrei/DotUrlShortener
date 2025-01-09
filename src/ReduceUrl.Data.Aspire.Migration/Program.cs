using Microsoft.EntityFrameworkCore;
using Npgsql;
using ReduceUrl.Data.Aspire.Migration;
using ReduceUrl.Data.DbContexts;
using ReduceUrl.ServiceDefaults;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddHostedService<MigrationWorker>();

builder
    .Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddSource(MigrationWorker.ActivitySourceName));

builder.AddNpgsqlDataSource(connectionName: "postgresdb");

builder.Services.AddDbContext<ReduceUrlDbContext>(
    (s, o) =>
    {
        var npgsqlDataSource = s.GetRequiredService<NpgsqlDataSource>();

        o.UseNpgsql(npgsqlDataSource);
    }
);

var host = builder.Build();

host.Run();
