using DotUrlShortenerApi.Tests.Common;
using Microsoft.AspNetCore.Mvc.Testing;

namespace DotUrlShortenerApi.Tests.Integration;

[TestClass]
public sealed class WeatherForecastEndpointTests
{
    [TestMethod]
    [TestCategory(TestCategories.Integration)]
    public async Task GetWeatherForecastShouldReturn()
    {
        var webApplicationFactory = new WebApplicationFactory<Program>();

        var httpClient = webApplicationFactory.CreateClient();

        var response = await httpClient.GetAsync("weatherforecast");

        Assert.AreEqual(response.IsSuccessStatusCode, true);
    }
}