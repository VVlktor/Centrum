using Centrum.Classes;
using System.Text.Json;

namespace Centrum
{
    public partial class MainPage : ContentPage
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public MainPage()
        {
            InitializeComponent();
            LoadWeatherData();
        }

        public async void LoadWeatherData()
        {
            var weatherData = await GetWeatherAsync();
            if (weatherData != null)
            {
                pogoda.Text = $"{weatherData.Current.Temp_c}";
            }
        }

        public async Task<WeatherData> GetWeatherAsync()
        {
            var response = await _httpClient.GetAsync($"");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to fetch weather data: {response.StatusCode}");
            }
            var content = await response.Content.ReadAsStringAsync();
            WeatherData dane = JsonSerializer.Deserialize<WeatherData>(content);
            //pogoda.Text = content;
            return dane;
        }
    }


    
}
