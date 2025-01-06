using DotUrlShortenerUi.Clients.Interfaces;
using DotUrlShortenerUi.OpenApiClients.DotUrlShortener;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Microsoft.Kiota.Serialization.Json;

namespace DotUrlShortenerUi.Clients;

public class DotUrlShortenerClient : IDotUrlShortenerClient
{
    private readonly HttpClient _httpClient;
    private DotUrlShortenerOpenApiClient _dotUrlShortenerOpenApiClient;

    public DotUrlShortenerClient(HttpClient httpClient)
    {
        var urlShortenerOpenApiClient = new HttpClientRequestAdapter(
            new AnonymousAuthenticationProvider(),
            new JsonParseNodeFactory(),
            new JsonSerializationWriterFactory(),
            httpClient,
            new ObservabilityOptions());

        _dotUrlShortenerOpenApiClient = new DotUrlShortenerOpenApiClient(urlShortenerOpenApiClient);
    }

    public async Task<IList<OpenApiClients.DotUrlShortener.Models.WeatherForecast>> GetWeatherforecast()
    {
        var weatherforecast = await _dotUrlShortenerOpenApiClient.Weatherforecast.GetAsync();

        if (weatherforecast == null)
        {
            return [];
        }

        return weatherforecast;
    }
}
