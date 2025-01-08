using DotUrlShortener.Ui.OpenApi.Clients.DotUrlShortener.Models;

namespace DotUrlShortener.Ui.Clients.Interfaces;

internal interface IDotUrlShortenerClient
{
    Task<IList<WeatherForecast>> GetWeatherforecast();
}
