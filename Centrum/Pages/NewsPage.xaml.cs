using System.Collections.ObjectModel;
using System.Net.Http;
using Centrum.Classes;

namespace Centrum.Pages;

public partial class NewsPage : ContentPage
{
    private readonly HttpClient _httpClient = new HttpClient();
    public ObservableCollection<NewsItem> CollectionOfNews { get; set; }
    string NewsApiKey;
    int whichNews=6;

    NewsData News;

    public NewsPage(string GetNewsApiKey)
	{
        NewsApiKey = GetNewsApiKey;
        InitializeComponent();
        LoadNews();        
	}

	public async void LoadNews()
	{
        News = await Services.GetNews(NewsApiKey);
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
        if (!(sender is ScrollView scrollView)) return;
        var scrollSpace = scrollView.ContentSize.Height - scrollView.Height;
        if (scrollSpace > e.ScrollY+10) return;
        else if(whichNews <= 50)
        {
            CollectionOfNews.Add(News.News[whichNews]);
            whichNews++;
            CollectionOfNews.Add(News.News[whichNews]);
            whichNews++;
        }
    }
}