using System.Collections.ObjectModel;

namespace Centrum.Pages;

public partial class NotePage : ContentPage
{
    ObservableCollection<string> fileNames  { get; set; } = new ObservableCollection<string>();

    public NotePage()
	{
		InitializeComponent();
        BindingContext = this;
        string mainDir = Path.Combine(FileSystem.Current.AppDataDirectory, "YourTxtFiles");
		if (!Directory.Exists(mainDir))
		{
			Directory.CreateDirectory(mainDir);
		}
        string[] Paths = Directory.GetFiles(mainDir);
        foreach (string FilePath in Paths)
        {
            fileNames.Add(Path.GetFileName(FilePath));
        }
        ListViewOfFiles.ItemsSource = fileNames;
    }

    private void FileTapped(object sender, TappedEventArgs e)
    {
        string tappedFile = e.Parameter as string;
        gf.Text= tappedFile;
    }
}