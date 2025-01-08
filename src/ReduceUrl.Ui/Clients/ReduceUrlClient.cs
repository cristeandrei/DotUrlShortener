using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Microsoft.Kiota.Serialization.Json;
using ReduceUrl.Ui.Clients.Interfaces;
using ReduceUrl.Ui.OpenApi.Clients.ReduceUrl;
using ReduceUrl.Ui.OpenApi.Clients.ReduceUrl.Models;

namespace ReduceUrl.Ui.Clients;

internal sealed class ReduceUrlClient(HttpClient httpClient) : IReduceUrlClient
{
    public async Task<IList<WeatherForecast>> GetWeatherforecast()
    {
        using var httpClientRequestAdapter = new HttpClientRequestAdapter(
            new AnonymousAuthenticationProvider(),
            new JsonParseNodeFactory(),
            new JsonSerializationWriterFactory(),
            httpClient,
            new ObservabilityOptions()
        );

        var reduceUrlHttpClient = new ReduceUrlHttpClient(httpClientRequestAdapter);

        var weatherForecasts = await reduceUrlHttpClient.Weatherforecast.GetAsync();

        return weatherForecasts ?? (IList<WeatherForecast>)([]);
    }
}
