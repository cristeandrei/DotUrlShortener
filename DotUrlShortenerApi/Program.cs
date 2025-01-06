using DotUrlShortenerApi;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.Servers = [];
    });
}

app.UseHttpsRedirection();


app.MapGet("/weatherforecast", () =>
    {
        var forecast = WeatherForecastProvider.GetWeatherForecasts();
        return forecast;
    })
    .WithName("GetWeatherForecast");

app.Run();