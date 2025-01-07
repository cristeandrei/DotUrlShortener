using DotUrlShortener.Ui.Clients.Interfaces;
using DotUrlShortener.Ui.OpenApiClients.DotUrlShortener;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Microsoft.Kiota.Serialization.Json;

namespace DotUrlShortener.Ui.Clients;

internal sealed class DotUrlShortenerClient(HttpClient httpClient) : IDotUrlShortenerClient
{
    public async Task<
        IList<OpenApiClients.DotUrlShortener.Models.WeatherForecast>
    > GetWeatherforecast()
    {
        using var urlShortenerOpenApiClient = new HttpClientRequestAdapter(
            new AnonymousAuthenticationProvider(),
            new JsonParseNodeFactory(),
            new JsonSerializationWriterFactory(),
            httpClient,
            new ObservabilityOptions()
        );

        var dotUrlShortenerOpenApiClient = new DotUrlShortenerOpenApiClient(
            urlShortenerOpenApiClient
        );

        var weatherForecasts = await dotUrlShortenerOpenApiClient.Weatherforecast.GetAsync();

        return weatherForecasts
            ?? (IList<OpenApiClients.DotUrlShortener.Models.WeatherForecast>)([]);
    }
}
