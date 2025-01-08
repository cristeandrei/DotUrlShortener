using DotUrlShortener.Ui.Clients.Interfaces;
using DotUrlShortener.Ui.OpenApi.Clients.DotUrlShortener;
using DotUrlShortener.Ui.OpenApi.Clients.DotUrlShortener.Models;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Microsoft.Kiota.Serialization.Json;

namespace DotUrlShortener.Ui.Clients;

internal sealed class DotUrlShortenerClient(HttpClient httpClient) : IDotUrlShortenerClient
{
    public async Task<IList<WeatherForecast>> GetWeatherforecast()
    {
        using var urlShortenerOpenApiClient = new HttpClientRequestAdapter(
            new AnonymousAuthenticationProvider(),
            new JsonParseNodeFactory(),
            new JsonSerializationWriterFactory(),
            httpClient,
            new ObservabilityOptions()
        );

        var dotUrlShortenerOpenApiClient = new DotUrlShortenerHttpClient(urlShortenerOpenApiClient);

        var weatherForecasts = await dotUrlShortenerOpenApiClient.Weatherforecast.GetAsync();

        return weatherForecasts ?? (IList<WeatherForecast>)([]);
    }
}
