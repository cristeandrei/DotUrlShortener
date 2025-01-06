var builder = DistributedApplication.CreateBuilder(args);

var dotUrlShortener = builder
    .AddProject<Projects.DotUrlShortenerApi>("doturlshortenerapi");

builder
    .AddProject<Projects.DotUrlShortenerUi>("doturlshortenerui")
    .WithExternalHttpEndpoints()
    .WithReference(dotUrlShortener);


builder.Build().Run();
