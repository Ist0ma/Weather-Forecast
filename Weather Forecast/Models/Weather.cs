namespace WeatherForecast.Models
{
    public class Responce<T>
    {
        public bool IsSuccess { get; set; }
        public T Item { get; set; }
    }

    public class Weather
    {
        public Location Location { get; set; }
        public Forecast Forecast { get; set; }
    }
    public class Day
    {
        public double Maxtemp_C { get; set; }
        public double Mintemp_C { get; set; }
        public double TotalPrecip_MM { get; set; }
        public int Daily_Chance_Of_Rain { get; set; }
    }

    public class Forecast
    {
        public List<Forecastday> Forecastday { get; set; }
    }

    public class Forecastday
    {
        public string Date { get; set; }
        public Day Day { get; set; }
    }

    public class Location
    {
        public string Name { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public string TzId { get; set; }
        public int LocaltimeEpoch { get; set; }
        public string Localtime { get; set; }
    }
}
