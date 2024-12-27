using BlazorAuth.Models;

namespace BlazorAuth.Services
{
    public interface IWeatherForecastService
    {
        //Get
        Task<IEnumerable<WeatherForecast>> GetWeatherForecastsAsync();
    }
}
