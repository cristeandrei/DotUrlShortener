var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres").WithPgAdmin();

var postgresdb = postgres.AddDatabase("postgresdb");

builder
    .AddProject<Projects.ReduceUrl_Data_Aspire_Migration>("reduceurldataaspiremigration")
    .WithReference(postgresdb)
    .WaitFor(postgresdb);

var reduceUrlApi = builder
    .AddProject<Projects.ReduceUrl_Api>("reduceurlapi")
    .WithReference(postgresdb);

builder
    .AddProject<Projects.ReduceUrl_Ui>("reduceurlui")
    .WithExternalHttpEndpoints()
    .WithReference(reduceUrlApi);

builder.Build().Run();
