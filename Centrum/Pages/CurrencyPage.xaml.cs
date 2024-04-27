using System.Text.Json;
using Centrum.Classes;

namespace Centrum.Pages;

public partial class CurrencyPage : TabbedPage
{
	string CurrencyApiKey;
    private readonly HttpClient _httpClient = new HttpClient();
    CurrencyData dataOfCurrency;
    bool zmieniam = false;

    public CurrencyPage(string GetCurrencyApiKey)
	{
		CurrencyApiKey = GetCurrencyApiKey;
        BindingContext = this;
		InitializeComponent();
        GetData();
    }

	public async void GetData()
	{
        string link = $"https://v6.exchangerate-api.com/v6/" + CurrencyApiKey + "/latest/PLN";
        var response = await _httpClient.GetAsync(link);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Failed to fetch currency data: {response.StatusCode}");
        }
        var content = await response.Content.ReadAsStringAsync();
        dataOfCurrency=JsonSerializer.Deserialize<CurrencyData>(content);
        dataOfCurrency.coversionRatesList = dataOfCurrency.coversionRates.ToList();
        SetData();
    }

    public void SetData()
    {
        List<string> waluty = new List<string>{ "USD", "EUR", "JPY", "GBP", "CNY", "AUD", "CAD", "CHF", "HKD", "SGD", "SEK", "KRW", "NOK", "NZD", "INR", "MXN", "TWD", "ZAR", "BRL", "DKK" };
        CurrencyPicker.ItemsSource=waluty;
        CurrencyPicker.SelectedItem = waluty[1];
        CurrencyCollView.ItemsSource = dataOfCurrency.coversionRatesList.Where(d => waluty.Contains(d.Key)).ToList();
        IndicatorCollView.IsVisible = false;
        IndicatorCollView2.IsVisible=false;
        CurrencyCollView.IsVisible = true;
        CurrCalc.IsVisible = true;
    }

    private async void ShowCurrInfo(object sender, TappedEventArgs e)
    {
        var curr = (KeyValuePair<string, double>)e.Parameter;
        await DisplayAlert($"Infomacje o {curr.Key}", $"Cena (za 1 PLN): {curr.Value:0.00} {curr.Key}\nCena (za 1 {curr.Key}): {(1/curr.Value):0.00} PLN\nOstatnia aktualizacja: {dataOfCurrency.lastTimeUpdate}", "OK");
    }

    private void PLNchanged(object sender, TextChangedEventArgs e)
    {
        if (!zmieniam)
        {
            zmieniam = true;
            double curr;
            if (double.TryParse(e.NewTextValue, out curr))
            {
                KalkEntryWaluta.Text = $"{string.Format("{0:0.00}", curr / dataOfCurrency.coversionRates[(string)CurrencyPicker.SelectedItem])}";
            }
            else
            {
                KalkEntryPLN.Text = string.Empty;
            }
            zmieniam =false;
        }
    }

    private void CurrChanged(object sender, TextChangedEventArgs e)
    {
        if (!zmieniam)
        {
            zmieniam = true;
            double curr;
            if (double.TryParse(e.NewTextValue, out curr))
            {
                KalkEntryPLN.Text = $"{string.Format("{0:0.00}",curr * dataOfCurrency.coversionRates[(string)CurrencyPicker.SelectedItem])}";
            }
            else
            {
                KalkEntryWaluta.Text = string.Empty;
            }
            zmieniam = false;
        }
    }

    private void changedCurr(object sender, EventArgs e)
    {
        KalkEntryPLN.Text = "";
        KalkEntryWaluta.Text = "";
    }

    private void CurrMoreInfo(object sender, TappedEventArgs e)
    {
        var selectedCurr = (StackLayout)((Border)sender).Content;
        if (selectedCurr != null)
        {
            var arrow = selectedCurr.Children.OfType<Image>().FirstOrDefault();
            if (arrow != null)
            {
                int currentRotation = arrow.Rotation == 0 ? 180 : 0;
                arrow.RotateTo(currentRotation, 500);
            }
            
        }
    }
}