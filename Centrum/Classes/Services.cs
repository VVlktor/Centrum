using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Centrum.Classes
{
    static class Services
    {
        public static string[,] GetLearningData()
        {
            string lng = Preferences.Default.Get("LearningLanguage", "Japoński");
            switch (lng)
            {
                case "Grecki":
                    return new string[,] { { "α", "alpha" }, { "β", "beta" }, { "γ", "gamma" }, { "δ", "delta" }, { "ε", "epsilon" }, { "ζ", "zeta" }, { "η", "eta" }, { "θ", "theta" }, { "ι", "iota" }, { "κ", "kappa" }, { "λ", "lambda" }, { "μ", "mu" }, { "ν", "nu" }, { "ξ", "xi" }, { "ο", "omicron" }, { "π", "pi" }, { "ρ", "rho" }, { "σ", "sigma" }, { "τ", "tau" }, { "υ", "upsilon" }, { "φ", "phi" }, { "χ", "chi" }, { "ψ", "psi" }, { "ω", "omega" } };
                case "Arabski":
                     return new string[,] { { "ا", "a" }, { "ب", "b" }, { "ت", "t" }, { "ث", "th" }, { "ج", "j" }, { "ح", "h" }, { "خ", "kh" }, { "د", "d" }, { "ذ", "dh" }, { "ر", "r" }, { "ز", "z" }, { "س", "s" }, { "ش", "sh" }, { "ص", "s" }, { "ض", "d" }, { "ط", "t" }, { "ظ", "dh" }, { "ع", "a" }, { "غ", "gh" }, { "ف", "f" }, { "ق", "q" }, { "ك", "k" }, { "ل", "l" }, { "م", "m" }, { "ن", "n" }, { "ه", "h" }, { "و", "w" }, { "ي", "y" } };
                case "Rosyjski":
                    return new string[,] { { "а", "a" }, { "б", "b" }, { "в", "v" }, { "г", "g" }, { "д", "d" }, { "е", "e" }, { "ё", "yo" }, { "ж", "zh" }, { "з", "z" }, { "и", "i" }, { "й", "j" }, { "к", "k" }, { "л", "l" }, { "м", "m" }, { "н", "n" }, { "о", "o" }, { "п", "p" }, { "р", "r" }, { "с", "s" }, { "т", "t" }, { "у", "u" }, { "ф", "f" }, { "х", "h" }, { "ц", "ts" }, { "ч", "ch" }, { "ш", "sh" }, { "щ", "sht" }, { "ъ", "j" }, { "ы", "i" }, { "ь", "j" }, { "э", "e" }, { "ю", "yu" }, { "я", "ya" } };
                default:
                    return new string[,] { { "あ", "a" }, { "い", "i" }, { "う", "u" }, { "え", "e" }, { "お", "o" }, { "か", "ka" }, { "き", "ki" }, { "く", "ku" }, { "け", "ke" }, { "こ", "ko" }, { "さ", "sa" }, { "し", "shi" }, { "す", "su" }, { "せ", "se" }, { "そ", "so" }, { "た", "ta" }, { "ち", "chi" }, { "つ", "tsu" }, { "て", "te" }, { "と", "to" }, { "な", "na" }, { "に", "ni" }, { "ぬ", "nu" }, { "ね", "ne" }, { "の", "no" }, { "は", "ha" }, { "ひ", "hi" }, { "ふ", "fu" }, { "へ", "he" }, { "ほ", "ho" }, { "ま", "ma" }, { "み", "mi" }, { "む", "mu" }, { "め", "me" }, { "も", "mo" }, { "や", "ya" }, { "ゆ", "yu" }, { "よ", "yo" }, { "ら", "ra" }, { "り", "ri" }, { "る", "ru" }, { "れ", "re" }, { "ろ", "ro" }, { "わ", "wa" }, { "を", "o" }, { "ん", "n" } };
            }
        }

        public static async Task<NewsData> GetNews(string NewsApiKey)
        {
            HttpClient _httpClient = new HttpClient();
            string formattedDate = DateTime.Now.AddDays(-3).ToString("yyyy-MM-dd");
            string link = $"https://api.worldnewsapi.com/search-news?api-key=" + NewsApiKey + "&earliest-publish-date=" + formattedDate + "&language=pl&number=55";
            var response = await _httpClient.GetAsync(link);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to fetch news data: {response.StatusCode}");
            }
            var content = await response.Content.ReadAsStringAsync();
            NewsData News = System.Text.Json.JsonSerializer.Deserialize<NewsData>(content);
            bool isImageVisible = Preferences.Default.Get("isNewsImageVisible", false);
            foreach (var h in News.News)
            {
                h.IsImageVisible = isImageVisible;
            }
            return News;
        }

        public static async Task<HiddenDataTokens> GetApiKeys()
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync("HiddenData.json");
            using var reader = new StreamReader(stream);
            string fileContent = await reader.ReadToEndAsync();
            return JsonConvert.DeserializeObject<HiddenDataTokens>(fileContent);
        }

        public static async Task<WeatherData> GetWeatherData(string WeatherApiKey)
        {
            HttpClient _httpClient = new HttpClient();
            string location = Preferences.Default.Get("Location", "Warsaw");
            string link = $"https://api.weatherapi.com/v1/forecast.json?key=" + WeatherApiKey + "&q=" + location;
            var response = await _httpClient.GetAsync(link);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to fetch weather data: {response.StatusCode}");
            }
            var content = await response.Content.ReadAsStringAsync();
            WeatherData dane = System.Text.Json.JsonSerializer.Deserialize<WeatherData>(content);
            JObject jsonObj = JObject.Parse(content);
            dane.Current.Chance_of_rain = jsonObj["forecast"]["forecastday"][0]["day"]["daily_chance_of_rain"].Value<int>();
            dane.Current.Chance_of_snow = jsonObj["forecast"]["forecastday"][0]["day"]["daily_chance_of_snow"].Value<int>();
            dane.Current.Sunset = jsonObj["forecast"]["forecastday"][0]["astro"]["sunset"].Value<string>();
            dane.Current.Sunrise = jsonObj["forecast"]["forecastday"][0]["astro"]["sunrise"].Value<string>();
            dane.Current.Avg_humidity = jsonObj["forecast"]["forecastday"][0]["day"]["avghumidity"].Value<int>();
            return dane;
        }

        public static ObservableCollection<NoteFile> GetFiles()
        {
            ObservableCollection<NoteFile> fileNames = new ObservableCollection<NoteFile>();
            string mainDir = Path.Combine(FileSystem.Current.AppDataDirectory, "YourTxtFiles");
            if (!Directory.Exists(mainDir))
            {
                Directory.CreateDirectory(mainDir);
            }
            string[] Paths = Directory.GetFiles(mainDir);
            foreach (string FilePath in Paths)
            {
                FileInfo fileInfo = new FileInfo(FilePath);
                fileNames.Add(new NoteFile { Name = Path.GetFileName(FilePath), LastAccess = fileInfo.LastAccessTime.ToString() });
            }
            return fileNames;
        }

        public static async Task<CurrencyData> GetCurrencyData(string CurrencyApiKey)
        {
            HttpClient _httpClient = new HttpClient();
            string link = $"https://v6.exchangerate-api.com/v6/" + CurrencyApiKey + "/latest/PLN";
            var response = await _httpClient.GetAsync(link);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to fetch currency data: {response.StatusCode}");
            }
            string content = await response.Content.ReadAsStringAsync();
            CurrencyData data = System.Text.Json.JsonSerializer.Deserialize<CurrencyData>(content);
            return data;
        }
    }
}
