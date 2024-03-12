using Centrum.Classes;

namespace Centrum.Pages;

public partial class PageOfNews : ContentPage
{
    NewsItem News = new NewsItem();
    public PageOfNews(NewsItem ReceivedNews)
    {
        InitializeComponent();
        News = ReceivedNews;
        ShowNewsData();
    }

    public void ShowNewsData()
    {
        NewsLabelTitle.Text = $"{News.Title}";
        NewsLabelDate.Text = $"{News.PublishDate}";
        NewsImage.Source = News.Image;
        NewsLabelText.Text = $"{News.Text}";
        string autorzy = "";
        foreach (var x in News.Authors)
        {
            autorzy = autorzy + x + ", ";
        }
        if (autorzy.Length >= 2)
        {
            autorzy = autorzy.Remove(autorzy.Length - 2);
        }
        NewsLabelAuthors.Text = autorzy;
    }

}