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
		string noteFont = Preferences.Default.Get("NoteFont", "OpenSansRegular");
		NoteEditor.FontFamily = noteFont;
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

    private void InfoOfFile(object sender, EventArgs e)
    {
		var info = new FileInfo(Path.Combine(FileSystem.Current.AppDataDirectory, @"YourTxtFiles", thisFile));
		DisplayAlert($"Infomacje o pliku {info.Name}", $"Data utworzenia: {info.CreationTime}\nOstatni zapis: {info.LastWriteTime}\nRozmiar: {info.Length}", "OK");
    }
}