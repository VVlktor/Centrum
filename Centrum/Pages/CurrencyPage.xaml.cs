namespace Centrum.Pages;

public partial class CurrencyPage : ContentPage
{
	string CurrencyApiKey;

	public CurrencyPage(string GetCurrencyApiKey)
	{
		CurrencyApiKey = GetCurrencyApiKey;
		InitializeComponent();
		GetData();
	}

	public void GetData()
	{
        //https://v6.exchangerate-api.com/v6/apikey/latest/PLN
    }
}