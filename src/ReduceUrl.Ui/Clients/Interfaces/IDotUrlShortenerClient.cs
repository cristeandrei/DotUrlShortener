using ReduceUrl.Ui.OpenApi.Clients.ReduceUrl.Models;

namespace ReduceUrl.Ui.Clients.Interfaces;

internal interface IReduceUrlClient
{
    Task<IList<WeatherForecast>> GetWeatherforecast();
}
