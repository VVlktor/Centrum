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
                HiddenDataTokens obj = await Services.GetApiKeys();
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
            weatherData = await Services.GetWeatherData(WeatherApiKey);
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

        public async void GoToGithub()
        {
            bool czyPrzejsc = await DisplayAlert("Czy kontynuować?", "Opuścisz aplikacje", "Tak", "Nie");
            if (czyPrzejsc)
            {
                Launcher.OpenAsync("https://github.com/VVlktor");
            }
            
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
            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {
                await Navigation.PushAsync(new NavigationPage(new CurrencyPage(CurrencyApiKey)));
            }
        }
    }
}
