using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Centrum.Classes
{
    class HiddenDataTokens
    {
       [JsonPropertyName("WEATHER_API_KEY")]
       public string WEATHER_API_KEY { get; set; }
    }
}
