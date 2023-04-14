using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using TennisBookings.Web.Configuration;
using TennisBookings.Web.Domain;
using TennisBookings.Web.External;

namespace TennisBookings.Web.Services
{
    public class WeatherForecaster : IWeatherForecaster
    {
        private readonly IWeatherApiClient _weatherApiClient;

        public WeatherForecaster(IWeatherApiClient weatherApiClient, IOptions<WeatherForecastingConfiguration> options)
        {
            _weatherApiClient = weatherApiClient;
            ForecastEnabled = options.Value?.EnableWeatherForecast ?? false;
        }

        public bool ForecastEnabled { get; }

        public async Task<CurrentWeatherResult> GetCurrentWeatherAsync()
        {
            var currentWeather = await _weatherApiClient.GetWeatherForecastAsync();

            if (currentWeather == null) return null;

            var result = new CurrentWeatherResult
            {
                Description = currentWeather.Weather.Description
            };

            return result;
        }
    }
}
