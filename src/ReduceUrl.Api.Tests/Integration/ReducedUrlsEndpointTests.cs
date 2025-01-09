using Microsoft.AspNetCore.Mvc.Testing;

namespace ReduceUrl.Api.Tests.Integration;

[TestClass]
public sealed class ReducedUrlsEndpointTests
{
    [TestMethod]
    [TestCategory("Integration")]
    public async Task GetReducedUrlsReturn()
    {
        await using var webApplicationFactory = new WebApplicationFactory<Program>();

        var httpClient = webApplicationFactory.CreateClient();

        var uri = new Uri("/reducedUrls", UriKind.Relative);

        var response = await httpClient.GetAsync(uri);

        Assert.AreEqual(true, response.IsSuccessStatusCode);
    }
}
