using Centrum.Classes;
using Microsoft.Maui.Controls.Shapes;

namespace Centrum.Pages;

public partial class LearningPage : ContentPage
{
    string[,] Characters;
    int correctAnswer = 0;
    bool canBeChecked = true;
    bool areVibrationsOn = Preferences.Default.Get("LearningVibration", true);

    Dictionary<int, Label> przyciskiOdpowiedzi;
    Dictionary<int, Border> borderyOdpowiedzi;

    public LearningPage()
	{
		InitializeComponent();
        SetDictionary();
        AddTableOfCharacters();
        NextQuestion();
    }

    private async void AnswerClicked(object sender, TappedEventArgs e)
    {
        if (canBeChecked)
        {
            canBeChecked = false;
            var whichAnswer = e.Parameter as string;
            await CheckAnswer(whichAnswer, sender);
            NextQuestion();
            canBeChecked = true;
        }
		
    }

    public void SetDictionary()
    {
        Characters=Services.GetLearningData();
        przyciskiOdpowiedzi = new Dictionary<int, Label>()
        {
            { 1, Odpowiedz1 },
            { 2, Odpowiedz2 },
            { 3, Odpowiedz3 },
            { 4, Odpowiedz4 }
        };
        borderyOdpowiedzi = new Dictionary<int, Border>()
        {
            { 1, BorderOdpowiedz1 },
            { 2, BorderOdpowiedz2 },
            { 3, BorderOdpowiedz3 },
            { 4, BorderOdpowiedz4 }
        };
    }

    public async Task CheckAnswer(string answ, object sender)
    {
        var borderRec = sender as Border;
        if (borderRec != null)
        {
            if (answ == correctAnswer.ToString())
            {
                borderRec.BackgroundColor = Color.FromArgb("#5ad647");
                if (areVibrationsOn)
                {
                    TimeSpan vibrationLength = TimeSpan.FromSeconds(0.5);
                    Vibration.Default.Vibrate(vibrationLength);
                }
                await Task.Delay(1000);
                borderRec.BackgroundColor = Color.FromArgb("#ffffff");
            }
            else
            {
                borderRec.BackgroundColor = Color.FromArgb("#fa2828");
                borderyOdpowiedzi[correctAnswer].BackgroundColor = Color.FromArgb("#5ad647");
                for(int i=0; i<2; i++)
                {
                    if (areVibrationsOn)
                    {
                        TimeSpan vibrationLength = TimeSpan.FromSeconds(0.15);
                        Vibration.Default.Vibrate(vibrationLength);
                    }
                    await Task.Delay(200);
                }
                await Task.Delay(600);
                borderRec.BackgroundColor = Color.FromArgb("#ffffff");
                borderyOdpowiedzi[correctAnswer].BackgroundColor = Color.FromArgb("#ffffff");
            }
            Task.Delay(400);
        }
    }

    public void NextQuestion()
    {
        Random rnd = new Random();
        List<int> usedAnswers = new List<int>();
        int whichAnswer = rnd.Next(0, Characters.Length / 2);
        usedAnswers.Add(whichAnswer);
        for (int j=1; j<5; j++)
        {
            int wylosowanaOdp = rnd.Next(0, Characters.Length / 2);
            while (usedAnswers.Contains(wylosowanaOdp))
            {
                wylosowanaOdp = rnd.Next(0, Characters.Length / 2);
            }
            usedAnswers.Add(wylosowanaOdp);
            przyciskiOdpowiedzi[j].Text = $"{Characters[wylosowanaOdp, 1]}";
        }
        correctAnswer = rnd.Next(1,5);
        przyciskiOdpowiedzi[correctAnswer].Text = $"{Characters[whichAnswer, 1]}";
        QuestionLabel.Text = $"{Characters[whichAnswer,0]}";
    }

    public void AddTableOfCharacters()
    {
        int i = 0;
        for (i=0;  i < Characters.Length/2-4; i = i + 4)
        {
            StackLayout stackRow = new StackLayout() { Orientation = StackOrientation.Horizontal, HorizontalOptions = LayoutOptions.Center };
            for (int j = 0; j < 4; j++)
            {
                StackLayout WewnetrznyLayout = new StackLayout();

                Label labelForei = new Label
                {
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    FontSize = 20,
                    Text = Characters[i + j, 0]
                };

                Label labelEng = new Label
                {
                    HorizontalOptions = LayoutOptions.Center,
                    FontSize = 16,
                    Text = Characters[i + j, 1]
                };

                WewnetrznyLayout.Add(labelForei);
                WewnetrznyLayout.Add(labelEng);

                Border border = new Border()
                {
                    WidthRequest = 80,
                    HeightRequest = 80,
                    Margin = 5,
                    StrokeThickness = 2,
                    Stroke = new SolidColorBrush(Color.FromRgb(211, 211, 211)),
                    StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(5) }
                };

                border.Content = WewnetrznyLayout;
                stackRow.Add(border);


            }
            LayoutOfCharacters.Add(stackRow);

        }

        StackLayout LastStackRow = new StackLayout() { Orientation = StackOrientation.Horizontal, HorizontalOptions = LayoutOptions.Center };

        for (i = i; i<Characters.Length/2; i++)
        {
            StackLayout WewnetrznyLayout = new StackLayout();

            Label labelForei = new Label
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                FontSize = 20,
                Text = Characters[i, 0]
            };

            Label labelEng = new Label
            {
                HorizontalOptions = LayoutOptions.Center,
                FontSize = 16,
                Text = Characters[i, 1]
            };

            WewnetrznyLayout.Add(labelForei);
            WewnetrznyLayout.Add(labelEng);

            Border border = new Border()
            {
                WidthRequest = 80,
                HeightRequest = 80,
                Margin = 5,
                StrokeThickness = 2,
                Stroke = new SolidColorBrush(Color.FromRgb(211, 211, 211)),
                StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(5) }
            };

            border.Content = WewnetrznyLayout;
            LastStackRow.Add(border);
        }
        LayoutOfCharacters.Add(LastStackRow);   
    }

    private void ChangeArrowDirection(object sender, TappedEventArgs e)
    {
        int currentRotation = ArrowLayoutOfChar.Rotation == 0 ? 180 : 0;
        ArrowLayoutOfChar.RotateTo(currentRotation, 250);
    }
}