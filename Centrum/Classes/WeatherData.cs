﻿using System;
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
        [JsonPropertyName("localtime")]
        public string Localtime { get; set; }
    }

    public class Current
    {
        [JsonPropertyName("wind_kph")]
        public double Wind_kph { get; set; }
        [JsonPropertyName("pressure_mb")]
        public double Pressure_mb { get; set; }
        [JsonPropertyName("temp_c")]
        public double Temp_c { get; set; }
        [JsonPropertyName("last_updated")]
        public string Last_updated { get; set;}
        public int Chance_of_rain {  get; set; } = 0;
        public double Max_temp_c { get; set; }
        public int Chance_of_snow {  get; set; }
        public string Sunrise { get; set; }
        public string Sunset { get; set; }
        public int Avg_humidity {  get; set; }
    }


    public class WeatherData
    {
        [JsonPropertyName("location")]
        public Location Location { get; set; }
        [JsonPropertyName("current")]
        public Current Current { get; set; }
    }
}
