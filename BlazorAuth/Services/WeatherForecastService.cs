using BlazorAuth.Models;

namespace BlazorAuth.Services
{
    public class WeatherForecastService : IWeatherForecastService
    {
        public async Task<IEnumerable<WeatherForecast>> GetWeatherForecastsAsync()
        {
            return await Task.FromResult(WeatherForecastSeed.Seed());
        }
    }
}
