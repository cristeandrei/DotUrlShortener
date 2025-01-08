using DotUrlShortener.Data;
using DotUrlShortener.Data.Aspire.Migration;
using DotUrlShortener.ServiceDefaults;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddHostedService<MigrationWorker>();

builder
    .Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddSource(MigrationWorker.ActivitySourceName));

builder.AddDatabase();

var host = builder.Build();

host.Run();
