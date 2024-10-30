using WeatherForecast.Models;

namespace Weather_Forecast.Models
{
    public class CityWeather
    {
        public double MaxTemp { get; set; }
        public double MinTemp { get; set; }
        public double Precip { get; set; }
        public int DailyChanceOfRain { get; set; }

        public CityWeather() { }

        public CityWeather(Forecast forecast)
        {
            MaxTemp = forecast.Forecastday[0].Day.Maxtemp_C;
            MinTemp = forecast.Forecastday[0].Day.Mintemp_C;
            Precip = forecast.Forecastday[0].Day.TotalPrecip_MM;
            DailyChanceOfRain = forecast.Forecastday[0].Day.Daily_Chance_Of_Rain;
        }
    }
}
