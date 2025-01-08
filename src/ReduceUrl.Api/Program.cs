using ReduceUrl.Api;
using ReduceUrl.Data;
using ReduceUrl.ServiceDefaults;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddDatabase();

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

app.MapGet("/weatherforecast", WeatherForecastProvider.GetWeatherForecasts)
    .WithName("GetWeatherForecast");

app.Run();
