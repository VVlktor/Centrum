using Centrum.Classes;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json.Nodes;

namespace Centrum
{
    public partial class MainPage : ContentPage
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private string WeatherApiKey;
        int dailyChanceOfRain=9888;

        public MainPage()
        {
            LoadWeather();
            InitializeComponent();
            
        }

        public async void LoadWeather()
        {
            try
            {
                using var stream = await FileSystem.OpenAppPackageFileAsync("HiddenData.json");
                using var reader = new StreamReader(stream);
                string fileContent = await reader.ReadToEndAsync();
                HiddenDataTokens obj = JsonConvert.DeserializeObject<HiddenDataTokens>(fileContent);
                WeatherApiKey = obj.WEATHER_API_KEY;
                LoadWeatherData();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        public async void LoadWeatherData()
        {
            var weatherData = await GetWeatherAsync();
            if (weatherData != null)
            {
                widgetPogody.IsVisible = true;
                LabelMiasto.Text = $"{weatherData.Location.Name}";
                LabelTemp.Text = $"{weatherData.Current.Temp_c} °C";
                LabelWind.Text = $"{weatherData.Current.Wind_kph} Km/h";
                LabelPres.Text = $"{weatherData.Current.Pressure_mb} hPa";
                LabelDanePogoda.Text = $"Dane z:\n{weatherData.Current.Last_updated}";
                if (weatherData.Current.Chance_of_rain >= 50 )
                {
                    ImagePogody.Source = "rainyday.png";
                }
                else
                {
                    ImagePogody.Source = "sunnyday.png";
                }
                indicatorPogody.IsVisible = false;
                
            }
        }

        public async Task<WeatherData> GetWeatherAsync()
        {
            string link = $"https://api.weatherapi.com/v1/forecast.json?key=" + WeatherApiKey + "&q=Warsaw";
            var response = await _httpClient.GetAsync(link);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to fetch weather data: {response.StatusCode}");
            }
            var content = await response.Content.ReadAsStringAsync();
            WeatherData dane = System.Text.Json.JsonSerializer.Deserialize<WeatherData>(content);
            JObject jsonObj = JObject.Parse(content);
            dane.Current.Chance_of_rain = jsonObj["forecast"]["forecastday"][0]["day"]["daily_chance_of_rain"].Value<int>();
            return dane;
        }

        private void ShowNewPage(object sender, TappedEventArgs e)
        {
            var whichPage = e.Parameter as string;
        }
    }


    
}
