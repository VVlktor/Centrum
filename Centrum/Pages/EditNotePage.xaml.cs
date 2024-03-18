namespace Centrum.Pages;

public partial class EditNotePage : ContentPage
{
	public EditNotePage(string selectedFile)
	{
		InitializeComponent();
		Title = selectedFile;
		LoadNote(selectedFile);
	}

	public void LoadNote(string selectedFile)
	{
		string filePath= Path.Combine(FileSystem.Current.AppDataDirectory, @"YourTxtFiles", selectedFile);
        string content = File.ReadAllText(filePath);
		NoteEditor.Text = content;
	}
}