namespace DotUrlShortener.Ui.Clients.Interfaces;

internal interface IDotUrlShortenerClient
{
    Task<IList<OpenApiClients.DotUrlShortener.Models.WeatherForecast>> GetWeatherforecast();
}
