using System.Security.Cryptography;

namespace DotUrlShortenerApi;

internal static class WeatherForecastProvider
{
    private static readonly string[] Summaries =
    [
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    ];

    public static IList<WeatherForecast> GetWeatherForecasts()
    {
        return Enumerable.Range(1, 5).Select(static index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    RandomNumberGenerator.GetInt32(-20, 55),
                    Summaries[RandomNumberGenerator.GetInt32(Summaries.Length)]
                ))
            .ToArray();
    }
}