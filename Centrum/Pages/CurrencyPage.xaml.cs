using System.Text.Json;
using Centrum.Classes;

namespace Centrum.Pages;

public partial class CurrencyPage : TabbedPage
{
	string CurrencyApiKey;
    private readonly HttpClient _httpClient = new HttpClient();
    CurrencyData dataOfCurrency;

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
        CurrencyCollView.ItemsSource = dataOfCurrency.coversionRatesList.Where(d => waluty.Contains(d.Key)).ToList();
        IndicatorCollView.IsVisible = false;
        CurrencyCollView.IsVisible = true;
    }

   
    private async void ShowCurrInfo(object sender, TappedEventArgs e)
    {
        var curr = (KeyValuePair<string, double>)e.Parameter;
        await DisplayAlert($"Infomacje o {curr.Key}", $"Cena (za 1 PLN): {curr.Value:0.00} {curr.Key}\nCena (za 1 {curr.Key}): {(1/curr.Value):0.00} PLN\nOstatnia aktualizacja: {dataOfCurrency.lastTimeUpdate}", "OK");
    }
}