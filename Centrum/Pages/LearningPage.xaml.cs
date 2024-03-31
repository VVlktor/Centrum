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
        SetCharactersArray();
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
                if (areVibrationsOn)
                {
                    TimeSpan vibrationLength = TimeSpan.FromSeconds(0.15);
                    Vibration.Default.Vibrate(vibrationLength);
                }
                await Task.Delay(200);
                if (areVibrationsOn)
                {
                    TimeSpan vibrationLength = TimeSpan.FromSeconds(0.15);
                    Vibration.Default.Vibrate(vibrationLength);
                }
                await Task.Delay(800);
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

    public void SetCharactersArray()
    {
        string lng = Preferences.Default.Get("LearningLanguage", "Japoński");
        switch (lng)
        {
            case "Grecki":
                string[,] CG = { { "α", "alpha" }, { "β", "beta" }, { "γ", "gamma" }, { "δ", "delta" }, { "ε", "epsilon" }, { "ζ", "zeta" }, { "η", "eta" }, { "θ", "theta" }, { "ι", "iota" }, { "κ", "kappa" }, { "λ", "lambda" }, { "μ", "mu" }, { "ν", "nu" }, { "ξ", "xi" }, { "ο", "omicron" }, { "π", "pi" }, { "ρ", "rho" }, { "σ", "sigma" }, { "τ", "tau" }, { "υ", "upsilon" }, { "φ", "phi" }, { "χ", "chi" }, { "ψ", "psi" }, { "ω", "omega" } };
                Characters = CG;
                break;
            case "Arabski":
                string[,] CA = { { "ا", "a" }, { "ب", "b" }, { "ت", "t" }, { "ث", "th" }, { "ج", "j" }, { "ح", "h" }, { "خ", "kh" }, { "د", "d" }, { "ذ", "dh" }, { "ر", "r" }, { "ز", "z" }, { "س", "s" }, { "ش", "sh" }, { "ص", "s" }, { "ض", "d" }, { "ط", "t" }, { "ظ", "dh" }, { "ع", "a" }, { "غ", "gh" }, { "ف", "f" }, { "ق", "q" }, { "ك", "k" }, { "ل", "l" }, { "م", "m" }, { "ن", "n" }, { "ه", "h" }, { "و", "w" }, { "ي", "y" } };
                Characters = CA;
                break;
            case "Rosyjski":
                string[,] CR = { { "а", "a" }, { "б", "b" }, { "в", "v" }, { "г", "g" }, { "д", "d" }, { "е", "e" }, { "ё", "yo" }, { "ж", "zh" }, { "з", "z" }, { "и", "i" }, { "й", "j" }, { "к", "k" }, { "л", "l" }, { "м", "m" }, { "н", "n" }, { "о", "o" }, { "п", "p" }, { "р", "r" }, { "с", "s" }, { "т", "t" }, { "у", "u" }, { "ф", "f" }, { "х", "h" }, { "ц", "ts" }, { "ч", "ch" }, { "ш", "sh" }, { "щ", "sht" }, { "ъ", "j" }, { "ы", "i" }, { "ь", "j" }, { "э", "e" }, { "ю", "yu" }, { "я", "ya" } };
                Characters = CR;
                break;
            default:
                string[,] CJ = { { "あ", "a" }, { "い", "i" }, { "う", "u" }, { "え", "e" }, { "お", "o" }, { "か", "ka" }, { "き", "ki" }, { "く", "ku" }, { "け", "ke" }, { "こ", "ko" }, { "さ", "sa" }, { "し", "shi" }, { "す", "su" }, { "せ", "se" }, { "そ", "so" }, { "た", "ta" }, { "ち", "chi" }, { "つ", "tsu" }, { "て", "te" }, { "と", "to" }, { "な", "na" }, { "に", "ni" }, { "ぬ", "nu" }, { "ね", "ne" }, { "の", "no" }, { "は", "ha" }, { "ひ", "hi" }, { "ふ", "fu" }, { "へ", "he" }, { "ほ", "ho" }, { "ま", "ma" }, { "み", "mi" }, { "む", "mu" }, { "め", "me" }, { "も", "mo" }, { "や", "ya" }, { "ゆ", "yu" }, { "よ", "yo" }, { "ら", "ra" }, { "り", "ri" }, { "る", "ru" }, { "れ", "re" }, { "ろ", "ro" }, { "わ", "wa" }, { "を", "o" }, { "ん", "n" } };
                Characters = CJ;
                break;
        } 
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

                Label labelJap = new Label
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

                WewnetrznyLayout.Add(labelJap);
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

            Label labelJap = new Label
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

            WewnetrznyLayout.Add(labelJap);
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
}