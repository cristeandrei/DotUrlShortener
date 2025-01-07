using Microsoft.AspNetCore.Mvc.Testing;

namespace DotUrlShortener.Api.Tests.Integration;

[TestClass]
public sealed class WeatherForecastEndpointTests
{
    [TestMethod]
    [TestCategory("Integration")]
    public async Task GetWeatherForecastShouldReturn()
    {
        await using var webApplicationFactory = new WebApplicationFactory<Program>();

        var httpClient = webApplicationFactory.CreateClient();

        var uri = new Uri("/weatherforecast");

        var response = await httpClient.GetAsync(uri);

        Assert.AreEqual(false, response.IsSuccessStatusCode);
    }
}
