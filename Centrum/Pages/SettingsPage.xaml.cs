namespace Centrum.Pages;

public partial class SettingsPage : ContentPage
{
	List<string> ListOfCapitals = new List<string>() { "Warsaw", "London","Roma","Paris","Berlin","Madrid","New York"};

	public SettingsPage()
	{
		InitializeComponent();
		string choosenCapital = Preferences.Default.Get("Location", "Warsaw");
		LocationPicker.SelectedIndex = ListOfCapitals.IndexOf(choosenCapital);
		LabelLocationChanged.IsVisible = false;
	}

    private void ChangedLocation(object sender, EventArgs e)
    {
		Preferences.Default.Set("Location",ListOfCapitals[LocationPicker.SelectedIndex]);
		LabelLocationChanged.IsVisible = true;
    }
}