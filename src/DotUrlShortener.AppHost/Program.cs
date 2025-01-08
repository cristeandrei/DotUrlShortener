var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres").WithPgAdmin().WithDataVolume();

var postgresdb = postgres.AddDatabase("postgresdb");

builder
    .AddProject<Projects.DotUrlShortener_Data_Aspire_Migration>("doturlshortenerdataaspiremigration")
    .WithReference(postgresdb);

var dotUrlShortener = builder
    .AddProject<Projects.DotUrlShortener_Api>("doturlshortenerapi")
    .WithReference(postgresdb);

builder
    .AddProject<Projects.DotUrlShortener_Ui>("doturlshortenerui")
    .WithExternalHttpEndpoints()
    .WithReference(dotUrlShortener);

builder.Build().Run();
