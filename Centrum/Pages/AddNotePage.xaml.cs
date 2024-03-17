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

    private void CreateFile(object sender, EventArgs e)
    {
        string fileNameFromEntry = FileNameEntry.Text;
    }
}