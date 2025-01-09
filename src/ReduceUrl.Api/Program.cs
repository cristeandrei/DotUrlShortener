using Microsoft.EntityFrameworkCore;
using Npgsql;
using ReduceUrl.Api;
using ReduceUrl.Data.DbContexts;
using ReduceUrl.ServiceDefaults;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddNpgsqlDataSource(connectionName: "postgresdb");

builder.Services.AddDbContextPool<ReduceUrlDbContext>(
    (s, o) =>
    {
        var npgsqlDataSource = s.GetRequiredService<NpgsqlDataSource>();

        o.UseNpgsql(npgsqlDataSource);
    }
);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<IReduceUrlRepository, ReduceUrlRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    _ = app.MapOpenApi();
    _ = app.MapScalarApiReference(static options => options.Servers = []);
}

app.UseHttpsRedirection();

app.MapGet(
        "/reducedUrls",
        async (IReduceUrlRepository reduceUrlRepository) => await reduceUrlRepository.GetAll()
    )
    .WithName("GetReducedUrls");

app.Run();
