using Centrum.Classes;
using Centrum.Pages;

namespace Centrum
{
    public partial class MainPage : ContentPage
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private HiddenDataTokens ApiKeys;
        WeatherData weatherData;
        bool IsDataLoaded = false;

        public MainPage()
        {
            LoadApiKeys();
            InitializeComponent();
        }

        public async void LoadApiKeys()
        {
            ApiKeys = await Services.GetApiKeys();
            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {
                LoadWeatherData();
            }
            else
            {
                labelDostepNet.IsVisible = true;
                indicatorPogody.IsVisible=false;
            }
        }

        public async void LoadWeatherData()
        {
            weatherData = await Services.GetWeatherData(ApiKeys.WEATHER_API_KEY);
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
                    ImagePogody.Source = "snowyday.png";
                else if (weatherData.Current.Chance_of_rain >= 70 )
                    ImagePogody.Source = "rainyday.png";
                else
                    ImagePogody.Source = "sunnyday.png";
                indicatorPogody.IsVisible = false;
            }
        }

        private async void ShowNewPage(object sender, TappedEventArgs e)
        {
            var whichPage = e.Parameter as string;
            switch (whichPage)
            {
                case "Autor":
					bool czyPrzejsc = await DisplayAlert("Czy kontynuować?", "Opuścisz aplikacje", "Tak", "Nie");
					if (czyPrzejsc)
						await Launcher.OpenAsync("https://github.com/VVlktor");
					break;
                case "Newsy":
					if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
						await Navigation.PushAsync(new NavigationPage(new NewsPage(ApiKeys.NEWS_API_KEY)));
					break;
                case "Pogoda":
					if (IsDataLoaded && Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
						await Navigation.PushAsync(new NavigationPage(new WeatherPage(weatherData)));
					break;
                case "Notatnik":
					await Navigation.PushAsync(new NavigationPage(new NotePage()));
					break;
                case "Ustawienia":
					await Navigation.PushAsync(new NavigationPage(new SettingsPage()));
					break;
                case "Nauka":
					await Navigation.PushAsync(new NavigationPage(new LearningPage()));
					break;
                case "Waluta":
					if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
						await Navigation.PushAsync(new NavigationPage(new CurrencyPage(ApiKeys.CURRENCY_API_KEY)));
					break;
                case "Bluetooth":
					await Navigation.PushAsync(new NavigationPage(new BluetoothPage()));
					break;
                default:
                    break;
            }
        }
    }
}
