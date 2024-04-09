using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Centrum.Classes
{
    public class CurrencyData
    {
        [JsonPropertyName("time_last_update_utc")]
        public string lastTimeUpdate { get; set; }
        [JsonPropertyName("conversion_rates")]
        public Dictionary<string, double> coversionRates { get; set; }

        public List<KeyValuePair<string,double>> coversionRatesList { get; set; }

    }
}
