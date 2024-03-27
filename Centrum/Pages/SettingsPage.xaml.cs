namespace Centrum.Pages;

public partial class SettingsPage : ContentPage
{
	List<string> ListOfCapitals = new List<string>() { "Warsaw", "London","Roma","Paris","Berlin","Madrid","New York"};

	public SettingsPage()
	{
		InitializeComponent();
		string choosenCapital = Preferences.Default.Get("Location", "Warsaw");
		LocationPicker.SelectedIndex = ListOfCapitals.IndexOf(choosenCapital);
		LearningLanguagePicker.SelectedItem = Preferences.Default.Get("LearningLanguage", "Japoñski");
		LabelLocationChanged.IsVisible = false;
	}

    private void ChangedLocation(object sender, EventArgs e)
    {
		Preferences.Default.Set("Location",ListOfCapitals[LocationPicker.SelectedIndex]);
		LabelLocationChanged.IsVisible = true;
    }

    private void ChangedLearningLanguage(object sender, EventArgs e)
    {
		var picker = (Picker)sender;
		if (picker.SelectedIndex != -1)
		{
			string lng=(string)picker.SelectedItem;
			Preferences.Default.Set("LearningLanguage",lng);
		}
    }
}