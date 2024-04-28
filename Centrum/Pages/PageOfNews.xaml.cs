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
        autorzy = autorzy.Length >= 2 ? autorzy.Remove(autorzy.Length - 2) : autorzy;
        NewsLabelAuthors.Text = autorzy;
    }

}