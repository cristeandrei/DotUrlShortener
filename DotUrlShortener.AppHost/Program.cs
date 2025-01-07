var builder = DistributedApplication.CreateBuilder(args);

var dotUrlShortener = builder.AddProject<Projects.DotUrlShortener_Api>("DotUrlShortener.Api");

builder
    .AddProject<Projects.DotUrlShortener_Ui>("DotUrlShortener.Ui")
    .WithExternalHttpEndpoints()
    .WithReference(dotUrlShortener);

builder.Build().Run();
