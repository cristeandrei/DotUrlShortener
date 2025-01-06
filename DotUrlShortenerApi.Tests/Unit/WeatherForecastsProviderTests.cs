using DotUrlShortenerApi.Tests.Common;

using FluentAssertions;

namespace DotUrlShortenerApi.Tests.Unit;

[TestClass]
public class WeatherForecastsProviderTests
{
    [TestMethod]
    [TestCategory(TestCategories.Unit)]
    public void ShouldReturnTheWeatherForecast()
    {
        var forecasts = WeatherForecastProvider.GetWeatherForecasts();

        forecasts.Should().NotBeEmpty();
    }
}