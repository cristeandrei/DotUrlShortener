namespace DotUrlShortenerUi.Clients.Interfaces;

public interface IDotUrlShortenerClient
{
    Task<IList<OpenApiClients.DotUrlShortener.Models.WeatherForecast>> GetWeatherforecast();
}
