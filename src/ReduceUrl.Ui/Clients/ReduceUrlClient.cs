using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Microsoft.Kiota.Serialization.Json;
using ReduceUrl.Ui.Clients.Interfaces;
using ReduceUrl.Ui.OpenApi.Clients.ReduceUrl;
using ReduceUrl.Ui.OpenApi.Clients.ReduceUrl.Models;

namespace ReduceUrl.Ui.Clients;

internal sealed class ReduceUrlClient(HttpClient httpClient) : IReduceUrlClient
{
    public async Task<IList<ReducedUrl>> GetReduceUrls()
    {
        using var httpClientRequestAdapter = new HttpClientRequestAdapter(
            new AnonymousAuthenticationProvider(),
            new JsonParseNodeFactory(),
            new JsonSerializationWriterFactory(),
            httpClient,
            new ObservabilityOptions()
        );

        var reduceUrlHttpClient = new ReduceUrlHttpClient(httpClientRequestAdapter);

        var reduceUrls = await reduceUrlHttpClient.ReducedUrls.GetAsync();

        return reduceUrls ?? (IList<ReducedUrl>)([]);
    }
}
