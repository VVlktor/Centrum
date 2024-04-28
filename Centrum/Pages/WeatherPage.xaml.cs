using Centrum.Classes;

namespace Centrum.Pages;

public partial class WeatherPage : ContentPage
{
	WeatherData weatherData;

	public WeatherPage(WeatherData ReceivedWeatherData)
	{
		weatherData = ReceivedWeatherData;
		InitializeComponent();
		setImage();
		setData();

	}

	public void setData()
	{
		LabelTemp.Text = $"{weatherData.Current.Temp_c} °C";
		LabelPlace.Text = $"{weatherData.Location.Name}";
		LabelPressure.Text = $"{weatherData.Current.Pressure_mb} hPa";
		LabelWind.Text = $"{weatherData.Current.Wind_kph} Km/h";
		LabelHumidity.Text = $"{weatherData.Current.Avg_humidity} %";
		LabelDataInfo.Text = $"Dane z: {weatherData.Current.Last_updated}";
        LabelSunrise.Text = $"{weatherData.Current.Sunrise}";
        LabelSunset.Text = $"{weatherData.Current.Sunset}";
    }

	public void setImage()
	{
		if (weatherData.Current.Chance_of_snow >= 70)
			imagePogody.Source = "snowyday.png";
		else if (weatherData.Current.Chance_of_rain >= 60)
			imagePogody.Source = "rainyday.png";
		else
			imagePogody.Source = "sunnyday.png";
	}
}