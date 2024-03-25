using Microsoft.Maui.Controls.Shapes;

namespace Centrum.Pages;

public partial class LearningPage : ContentPage
{
	public LearningPage()
	{
		InitializeComponent();
        string[,] Characters = { { "あ", "a" }, { "い", "i" }, { "う", "u" }, { "え", "e" }, { "お", "o" }, { "か", "ka" }, { "き", "ki" }, { "く", "ku" }, { "け", "ke" }, { "こ", "ko" }, { "さ", "sa" }, { "し", "shi" }, { "す", "su" }, { "せ", "se" }, { "そ", "so" }, { "た", "ta" }, { "ち", "chi" }, { "つ", "tsu" }, { "て", "te" }, { "と", "to" }, { "な", "na" }, { "に", "ni" }, { "ぬ", "nu" }, { "ね", "ne" }, { "の", "no" }, { "は", "ha" }, { "ひ", "hi" }, { "ふ", "fu" }, { "へ", "he" }, { "ほ", "ho" }, { "ま", "ma" }, { "み", "mi" }, { "む", "mu" }, { "め", "me" }, { "も", "mo" }, { "や", "ya" }, { "ゆ", "yu" }, { "よ", "yo" }, { "ら", "ra" }, { "り", "ri" }, { "る", "ru" }, { "れ", "re" }, { "ろ", "ro" }, { "わ", "wa" }, { "を", "o" }, { "ん", "n" } };
        for (int i=0; i<44; i=i+4)
        {
            StackLayout stackRow = new StackLayout() { Orientation=StackOrientation.Horizontal, HorizontalOptions=LayoutOptions.Center };
            for (int j = 0; j < 4; j++)
            {
                StackLayout WewnetrznyLayout = new StackLayout();

                Label labelJap = new Label 
                { 
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    FontSize=20,
                    Text = Characters[i+j,0]
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
        StackLayout stackLastRow = new StackLayout() { Orientation = StackOrientation.Horizontal, HorizontalOptions = LayoutOptions.Center };
        for (int i =44; i<46; i++)
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
            stackLastRow.Add(border);
        }
        LayoutOfCharacters.Add(stackLastRow);
    }

    private void AnswerClicked(object sender, TappedEventArgs e)
    {
		var whichAnswer = e.Parameter as string;
    }

}