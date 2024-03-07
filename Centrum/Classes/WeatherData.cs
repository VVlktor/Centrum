using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Centrum.Classes
{
    public class Location
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("country")]
        public string Country { get; set; }
    }

    public class Condition
    {
        // Assuming there are properties within the condition object
        // You should add properties here based on the actual JSON structure of the condition object
    }

    public class Current
    {
        [JsonPropertyName("wind_kph")]
        public double Wind_kph { get; set; }
        [JsonPropertyName("pressure_mb")]
        public double pressure_mb { get; set; }
        [JsonPropertyName("temp_c")]
        public double Temp_c { get; set; }
    }

    public class WeatherData
    {
        [JsonPropertyName("location")]
        public Location Location { get; set; }
        [JsonPropertyName("current")]
        public Current Current { get; set; }
    }
}
