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
        SetData();
    }

    public void SetData()
    {
        temporaryLabel.Text = dataOfCurrency.coversionRates["USD"].ToString();
    }
}