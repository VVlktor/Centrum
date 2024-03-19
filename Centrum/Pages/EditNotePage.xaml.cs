namespace Centrum.Pages;

public partial class EditNotePage : ContentPage
{
	string thisFile;
	public EditNotePage(string selectedFile)
	{
		thisFile = selectedFile;
		InitializeComponent();
		Title = selectedFile;
		LoadNote();
	}

	public void LoadNote()
	{
		string filePath= Path.Combine(FileSystem.Current.AppDataDirectory, @"YourTxtFiles", thisFile);
        string content = File.ReadAllText(filePath);
		NoteEditor.Text = content;
	}

    protected override void OnDisappearing()
    {
        string editedContent = NoteEditor.Text;
		if(File.Exists(Path.Combine(FileSystem.Current.AppDataDirectory, @"YourTxtFiles", thisFile))){
			File.WriteAllText(Path.Combine(FileSystem.Current.AppDataDirectory, @"YourTxtFiles", thisFile), editedContent);
		}
        base.OnDisappearing();
    }

    private async void DeleteFile(object sender, EventArgs e)
    {
		File.Delete(Path.Combine(FileSystem.Current.AppDataDirectory, @"YourTxtFiles", thisFile));
        await Navigation.PopAsync();
    }

    private void FileInfo(object sender, EventArgs e)
    {

    }
}