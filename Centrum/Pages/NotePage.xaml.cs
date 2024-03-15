using System.Collections.ObjectModel;

namespace Centrum.Pages;

public partial class NotePage : ContentPage
{
    public class Fd
    {
        public string nazwa {  get; set; }
    }

    ObservableCollection<Fd> fileNames  { get; set; }

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
        fileNames = new ObservableCollection<Fd>();
        foreach (string FilePath in Paths)
        {
            fileNames.Add(new Fd { nazwa = Path.GetFileName(FilePath) });
        }
    }
}