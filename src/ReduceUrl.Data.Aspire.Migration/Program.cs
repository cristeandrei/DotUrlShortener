using ReduceUrl.Data;
using ReduceUrl.Data.Aspire.Migration;
using ReduceUrl.ServiceDefaults;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddHostedService<MigrationWorker>();

builder
    .Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddSource(MigrationWorker.ActivitySourceName));

builder.AddDatabase();

var host = builder.Build();

host.Run();
