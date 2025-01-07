using FluentAssertions;

namespace DotUrlShortener.Api.Tests.Unit;

[TestClass]
public sealed class WeatherForecastsProviderTests
{
    [TestMethod]
    public void ShouldReturnTheWeatherForecast()
    {
        var forecasts = WeatherForecastProvider.GetWeatherForecasts();

        forecasts.Should().BeEmpty();
    }
}
