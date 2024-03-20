using System.Collections.ObjectModel;
using Centrum.Classes;

namespace Centrum.Pages;

public partial class NotePage : ContentPage
{
    ObservableCollection<NoteFile> fileNames  { get; set; } = new ObservableCollection<NoteFile>();

    public NotePage()
	{
		InitializeComponent();
        BindingContext = this;
        LoadFiles();
    }

    public void LoadFiles()
    {
        fileNames.Clear();
        string mainDir = Path.Combine(FileSystem.Current.AppDataDirectory, "YourTxtFiles");
        if (!Directory.Exists(mainDir))
        {
            Directory.CreateDirectory(mainDir);
        }
        string[] Paths = Directory.GetFiles(mainDir);
        foreach (string FilePath in Paths)
        {
            FileInfo fileInfo = new FileInfo(FilePath);
            fileNames.Add(new NoteFile { Name = Path.GetFileName(FilePath), LastAccess = fileInfo.LastAccessTime.ToString() });
        }
        ListViewOfFiles.ItemsSource = fileNames;
    }


    private async void FileTapped(object sender, TappedEventArgs e)
    {
        var tappedFile = e.Parameter as NoteFile;
        await Navigation.PushAsync(new EditNotePage(tappedFile.Name));
    }

    private async void AddNewFile(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new NavigationPage(new AddNotePage()));
    }


    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadFiles();
    }
}