using Centrum.Classes;
using System.Text.Json;
using Newtonsoft.Json;
namespace Centrum
{
    public partial class MainPage : ContentPage
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private string WeatherApiKey;

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
               pogoda.Text = $"Temperatura: {weatherData.Current.Temp_c}\n";
            }
        }

        public async Task<WeatherData> GetWeatherAsync()
        {
            string link = $"https://api.weatherapi.com/v1/current.json?key=" + WeatherApiKey + "&q=Warsaw";
            var response = await _httpClient.GetAsync(link);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to fetch weather data: {response.StatusCode}");
            }
            var content = await response.Content.ReadAsStringAsync();
            WeatherData dane = System.Text.Json.JsonSerializer.Deserialize<WeatherData>(content);
            return dane;
        }
    }


    
}
