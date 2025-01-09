using ReduceUrl.Data;
using ReduceUrl.Data.Repositories.Interfaces;
using ReduceUrl.ServiceDefaults;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddReduceUrlDataAccessLayer();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

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
