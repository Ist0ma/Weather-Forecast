using System.Text.Json.Serialization;

namespace Weather_Forecast.Models
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AltName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        [JsonIgnore]
        public string Region { get; set; }
        [JsonIgnore]
        public DateTime LastNotified { get; set; }
        [JsonIgnore]
        public DateTime LastUpdate { get; set; }
        [JsonIgnore]
        public CityWeather CityWeather { get; set; } = new();
    }
}
