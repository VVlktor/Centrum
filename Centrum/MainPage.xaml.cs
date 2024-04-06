using Centrum.Classes;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json.Nodes;
using Centrum.Pages;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Centrum
{
    public partial class MainPage : ContentPage
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private string WeatherApiKey;
        private string NewsApiKey;
        private string CurrencyApiKey;
        WeatherData weatherData;
        bool IsDataLoaded = false;

        public MainPage()
        {
            LoadApiKeys();
            InitializeComponent();
            
        }

        public async void LoadApiKeys()
        {
            try
            {
                using var stream = await FileSystem.OpenAppPackageFileAsync("HiddenData.json");
                using var reader = new StreamReader(stream);
                string fileContent = await reader.ReadToEndAsync();
                HiddenDataTokens obj = JsonConvert.DeserializeObject<HiddenDataTokens>(fileContent);
                WeatherApiKey = obj.WEATHER_API_KEY;
                NewsApiKey = obj.NEWS_API_KEY;
                CurrencyApiKey = obj.CURRENCY_API_KEY;
                if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
                {
                    LoadWeatherData();
                }
                else
                {
                    indicatorPogody.IsVisible=false;
                    labelDostepNet.IsVisible = true;
                }
                
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        public async void LoadWeatherData()
        {
            weatherData = await GetWeatherAsync();
            if (weatherData != null)
            {
                IsDataLoaded = true;
                widgetPogody.IsVisible = true;
                LabelMiasto.Text = $"{weatherData.Location.Name}";
                LabelTemp.Text = $"{weatherData.Current.Temp_c} °C";
                LabelWind.Text = $"{weatherData.Current.Wind_kph} Km/h";
                LabelPres.Text = $"{weatherData.Current.Pressure_mb} hPa";
                LabelDanePogoda.Text = $"Dane z:\n{weatherData.Current.Last_updated}";
                if (weatherData.Current.Chance_of_snow>=70)
                {
                    ImagePogody.Source = "snowyday.png";
                }
                else if (weatherData.Current.Chance_of_rain >= 70 )
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
            string location = Preferences.Default.Get("Location", "Warsaw");
            string link = $"https://api.weatherapi.com/v1/forecast.json?key=" + WeatherApiKey+"&q="+location;
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

        private void ShowNewPage(object sender, TappedEventArgs e)
        {
            var whichPage = e.Parameter as string;
            switch (whichPage)
            {
                case "Autor":
                    GoToGithub();
                    break;
                case "Newsy":
                    GoToNewsPage();
                    break;
                case "Pogoda":
                    GoToWeatherPage();
                    break;
                case "Notatnik":
                    GoToNotePage();
                    break;
                case "Ustawienia":
                    GoToSettings();
                    break;
                case "Nauka":
                    GoToLearning();
                    break;
                case "Waluta":
                    GoToCurrencyPage();
                    break;
                default:
                    break;
            }
        }

        public void GoToGithub()
        {
            Launcher.OpenAsync("https://github.com/VVlktor");
        }

        public async void GoToNewsPage()
        {
            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            { 
                await Navigation.PushAsync(new NavigationPage(new NewsPage(NewsApiKey)));
            }
                
        }

        public async void GoToWeatherPage()
        {
            if (IsDataLoaded && Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {
                await Navigation.PushAsync(new NavigationPage(new WeatherPage(weatherData)));
            }
        }

        public async void GoToNotePage()
        {
            await Navigation.PushAsync(new NavigationPage(new NotePage()));
        }

        public async void GoToSettings()
        {
            await Navigation.PushAsync(new NavigationPage(new SettingsPage()));
        }

        public async void GoToLearning()
        {
            await Navigation.PushAsync(new NavigationPage(new LearningPage()));
        }

        public async void GoToCurrencyPage()
        {
            await Navigation.PushAsync(new NavigationPage(new CurrencyPage(CurrencyApiKey)));
        }
    }
}
