using Microsoft.Maui.Storage;

namespace Centrum.Pages;

public partial class AddNotePage : ContentPage
{
	public AddNotePage()
	{
		InitializeComponent();
	}

    private async void GoBack(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    private async void CreateFile(object sender, EventArgs e)
    {
        string fileNameFromEntry = FileNameEntry.Text;
        if (fileNameFromEntry == null) return;
        string filePath = Path.Combine(FileSystem.Current.AppDataDirectory,@"YourTxtFiles", fileNameFromEntry);
        filePath = filePath + ".txt";
        File.Create(filePath);
        await Navigation.PopModalAsync();
    }
}