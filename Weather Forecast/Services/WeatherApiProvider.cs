using System.Net.Http.Json;
using Weather_Forecast.Providers;
using WeatherForecast.Models;

namespace WeatherForecast.Services
{
    public class WeatherApiProvider : WeatherProvider
    {
        private readonly string ApiKey;
        public WeatherApiProvider()
        {
            ApiKey = Weather_Forecast.Properties.Settings.Default.WeatherApiKey;
        }

        public async Task<Responce<Weather>> GetForecast(double latitude, double longitude)
        {
            HttpClient httpClient = new HttpClient();

            Responce<Weather> responce = new();

            var result = await httpClient.GetAsync($"http://api.weatherapi.com/v1/forecast.json?key={ApiKey}&q={latitude.ToString(System.Globalization.CultureInfo.InvariantCulture)},{longitude.ToString(System.Globalization.CultureInfo.InvariantCulture)}");

            if (result.IsSuccessStatusCode)
            {
                Weather? cityWeather = await result.Content.ReadFromJsonAsync<Weather>();
                responce.IsSuccess = cityWeather != null;
                responce.Item = cityWeather;
            }

            return responce;
        }
    }
}
