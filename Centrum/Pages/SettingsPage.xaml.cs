namespace Centrum.Pages;

public partial class SettingsPage : ContentPage
{
	List<string> ListOfCapitals = new List<string>() { "Warsaw", "London","Roma","Paris","Berlin","Madrid","New York"};

	public SettingsPage()
	{
		InitializeComponent();
        GetAllSetting();
	}

    public void GetAllSetting()
    {
        string choosenCapital = Preferences.Default.Get("Location", "Warsaw");
        LocationPicker.SelectedIndex = ListOfCapitals.IndexOf(choosenCapital);
        string unchangedFont = Preferences.Default.Get("NoteFont", "Serif");
        char[] changedFont = unchangedFont.ToCharArray();
        changedFont[0] = char.ToUpper(changedFont[0]);
        NoteFontPicker.SelectedItem = new string(changedFont);
        LearningVibration.IsToggled = Preferences.Default.Get("LearningVibration", true);
        NewsImage.IsToggled = Preferences.Default.Get("isNewsImageVisible", false);
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

    private void ChangedNoteFont(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        if (picker.SelectedIndex != -1)
        {
            string newFont = (string)picker.SelectedItem;
			newFont = newFont.ToLower();
            Preferences.Default.Set("NoteFont", newFont);
        }
    }

    private void LearningVibrationToggled(object sender, ToggledEventArgs e)
    {
        bool togglerStatus = LearningVibration.IsToggled;
        Preferences.Default.Set("LearningVibration", togglerStatus);
    }

    private void NewsImageToggled(object sender, ToggledEventArgs e)
    {
        bool togglerStatus = NewsImage.IsToggled;
        Preferences.Default.Set("isNewsImageVisible", togglerStatus);
    }
}