using System.Net.Http;
using Centrum.Classes;

namespace Centrum.Pages;

public partial class NewsPage : ContentPage
{
    private readonly HttpClient _httpClient = new HttpClient();
    string NewsApiKey;
    NewsData News;

    public NewsPage(string GetNewsApiKey)
	{
        NewsApiKey = GetNewsApiKey;   
        InitializeComponent();  
        LoadNews();
	}

	public async void LoadNews()
	{
        string link = $"https://api.worldnewsapi.com/search-news?api-key="+NewsApiKey+"&language=pl&number=3";
        var response = await _httpClient.GetAsync(link);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Failed to fetch news data: {response.StatusCode}");
        }
        var content = await response.Content.ReadAsStringAsync();
        News = System.Text.Json.JsonSerializer.Deserialize<NewsData>(content);
        //ShowNews();
    }
}