using System.Collections.ObjectModel;
using System.Net.Http;
using Centrum.Classes;

namespace Centrum.Pages;

public partial class NewsPage : ContentPage
{
    private readonly HttpClient _httpClient = new HttpClient();
    public ObservableCollection<NewsItem> CollectionOfNews { get; set; }
    string NewsApiKey;
    int whichNews=3;

    NewsData News;

    public NewsPage(string GetNewsApiKey)
	{
        NewsApiKey = GetNewsApiKey;
        InitializeComponent();
        LoadNews();        
	}

	public async void LoadNews()
	{
        DateTime Date = DateTime.Now.AddDays(-10);
        string formattedDate = Date.ToString("yyyy-MM-dd");
        string link = $"https://api.worldnewsapi.com/search-news?api-key="+NewsApiKey+ "&earliest-publish-date=" + formattedDate + "&language=pl&number=30";
        var response = await _httpClient.GetAsync(link);
        if (!response.IsSuccessStatusCode)
        {
             throw new Exception($"Failed to fetch news data: {response.StatusCode}");
        }
        var content = await response.Content.ReadAsStringAsync();
        News = System.Text.Json.JsonSerializer.Deserialize<NewsData>(content);
        bool isImageVisible = Preferences.Default.Get("isNewsImageVisible", false);
        foreach (var h in News.News)
        {
            h.IsImageVisible=isImageVisible;
        }
        CollectionOfNews = new ObservableCollection<NewsItem> { News.News[0], News.News[1], News.News[2], News.News[3], News.News[4], News.News[5], News.News[6] };
        BindingContext = this;
    }

    private async void CheckOutNews(object sender, TappedEventArgs e)
    {
        var ChoosenNews = e.Parameter as NewsItem;
        if (ChoosenNews != null)
        {
            await Navigation.PushAsync(new NavigationPage(new PageOfNews(ChoosenNews)));
        }
    }

    private void Scrolling(object sender, ScrolledEventArgs e)
    {
        if (!(sender is ScrollView scrollView))
        {
            return;
        }
        var scrollSpace = scrollView.ContentSize.Height - scrollView.Height;
        if (scrollSpace > e.ScrollY+10)
        {
            return;
        }
        else
        {
            if (whichNews<=27)
            {
                CollectionOfNews.Add(News.News[whichNews]);
                whichNews++;
                CollectionOfNews.Add(News.News[whichNews]);
                whichNews++;
            }
        }
    }
}