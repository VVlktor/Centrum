namespace Centrum.Pages;

public partial class LearningPage : ContentPage
{
	public LearningPage()
	{
		InitializeComponent();
	}

    private void AnswerClicked(object sender, TappedEventArgs e)
    {
		var whichAnswer = e.Parameter as string;
    }
}