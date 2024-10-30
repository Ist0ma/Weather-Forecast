using WeatherForecast.Models;

namespace Weather_Forecast.Providers
{
    public interface WeatherProvider
    {
        Task<Responce<Weather>> GetForecast(double latitude, double longitude);
    }
}
