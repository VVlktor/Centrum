using System.Collections.ObjectModel;
using System.Net.Http;
using Centrum.Classes;

namespace Centrum.Pages;

public partial class NewsPage : ContentPage
{
    private readonly HttpClient _httpClient = new HttpClient();
    public ObservableCollection<NewsItem> CollectionOfNews { get; set; }
    string NewsApiKey;
    int whichNews=3;

    NewsData News;

    public NewsPage(string GetNewsApiKey)
	{
        NewsApiKey = GetNewsApiKey;   
        InitializeComponent();  
        LoadNews();
	}

	public async void LoadNews()
	{
        DateTime Date = DateTime.Now.AddDays(-10);
        string formattedDate = Date.ToString("yyyy-MM-dd");
        string link = $"https://api.worldnewsapi.com/search-news?api-key="+NewsApiKey+ "&earliest-publish-date=" + formattedDate + "&language=pl&number=30";
        var response = await _httpClient.GetAsync(link);
        if (!response.IsSuccessStatusCode)
        {
             throw new Exception($"Failed to fetch news data: {response.StatusCode}");
        }
        var content = await response.Content.ReadAsStringAsync();
        News = System.Text.Json.JsonSerializer.Deserialize<NewsData>(content);
        //string content = "{\"offset\":0,\"number\":3,\"available\":1161,\"news\":[{\"id\":195290299,\"title\":\"Chcia³a nagraæ wideo w Zakopanem. \\\"Halo, nie ma tak! Chcecie, to sobie zap³aæcie\\\"\",\"text\":\"W pi¹tek 1 marca znana z serialu \\\"M jak mi³oœæ\\\" Hanna Turnau opublikowa³a w mediach spo³ecznoœciowych nagranie z Zakopanego. Aktorka chcia³a nagraæ film dla swoich widzów, jednak na przeszkodzie stan¹³ jej jeden z mê¿czyzn, który pracowa³ na Krupówkach. Zakopane. Chcia³a nagrywaæ na Krupówkach. Sprzeciwi³ siê mê¿czyzna przebrany za bia³ego misia Na popularnej w Zakopanem ulicy oraz znanym turystom deptaku o ka¿dej porze roku mo¿na spotkaæ osoby w strojach postaci z bajek czy przebrane za zwierzaki, które pobieraj¹ op³aty od przechodniów za wspólne zdjêcie czy nagranie. Aktorka Hanna Turnau przekona³a siê o tym, gdy próbowa³a nagraæ film na ulicy. Mê¿czyzna, który pracowa³ na Krupówkach w stroju niedŸwiedzia od razu zareagowa³. - Halo, nie ma tak! Chcecie, to sobie zap³aæcie za foto nie ma sprawy - zwróci³ siê do Turnau mê¿czyzna, gdy zauwa¿y³, ¿e kobieta mo¿e kierowaæ kamerê w jego stronê. - Ale my tu nagrywamy - odpowiedzia³a mu aktorka. - Obojêtnie, czy nagrywasz, czy cokolwiek. My tutaj stoimy i jak chcesz z nami (zdjêcie - red.), to zap³aæ nam - skwitowa³ mê¿czyzna. Turnau skomentowa³a tylko, ¿e \\\"Zakopane to jest bardzo sympatyczne miasto\\\" i zakoñczy³a nagranie. Kobieta opublikowa³a te¿ nagranie z ca³ej sytuacji na Instagramie. \\\"Kiedy chcesz nagrywaæ self-tape na Krupówkach lepiej weŸ gotówkê. Z ¿ycia wziête\\\" - podpisa³a swój film, który opublikowa³a w mediach spo³ecznoœciowych. Zakopane. ¯¹daj¹ op³aty za wspólne zdjêcie lub film U¿ytkownicy serwisu nie kryli oburzenia. \\\"A paragony pan daje po zap³acie za zdjêcie?\\\" - pytali. Inni zwracali uwagê, ¿e teraz ludzie s¹ wyj¹tkowo \\\"pazerni na kasê\\\" i przyznawali, ¿e sami spotkali siê z podobnych zachowaniem w Zakopanem. \\\"Op³ata od selfie. W sumie, dlaczego nie\\\" - skomentowa³ film w serwisie X Krzysztof Bontal poœrednik kredytowy i redaktor portalu Pierwszemieszkanie.com. ¯artowano równie¿, ¿e samo obserwowanie tych górskich okolic w Google Maps sporo kosztuje. \\\"Przejecha³em kursorem przez Zakopane i pobra³o mi 20 z³\\\" - pisa³ z ironi¹ jeden z internautów w serwisie X.\",\"url\":\"https://wiadomosci.gazeta.pl/wiadomosci/7,114883,30757140,chciala-nagrywac-ale-bialy-mis-zazadal-oplaty-zakopane.html\",\"image\":\"https://bi.im-g.pl/im/9e/55/1d/z30757278IER,Aktorka-probowala-nagrywac-film-w-Zakopanem.jpg\",\"publish_date\":\"2024-03-02 12:05:00\",\"author\":\"Karolina Trela\",\"authors\":[\"Karolina Trela\"],\"language\":\"pl\",\"source_country\":\"pl\"},{\"id\":195290301,\"title\":\"Rosjanie pods³uchali rozmowê oficerów Bundeswehry. Niemiecki kontrwywiad bada sprawê\",\"text\":\"Ujawnione nagranie to jedna kilkudziesiêciominutowa wideorozmowa - eksperci uwa¿aj¹, ¿e autentyczna. Mia³o do niej dojœæ 19 lutego przez aplikacjê do rozmów zdalnych. Specjaliœci twierdz¹, ¿e aplikacja, z której skorzystali wojskowi, nie jest szczególnie zabezpieczona, co mog³o u³atwiæ przechwycenie rozmowy. Dodatkowo jeden z czterech uczestnicz¹cych w wymianie opinii oficerów Luftwaffe, inspektor Si³ Powietrznych Niemiec gen. Ingo Gerhartz, by³ w tym czasie w Azji. Rosja. Ujawniono nagranie z rozmowy oficerów Bundeswehry Rozmowa dotyczy³a pocisków Taurus, których kanclerz Niemiec Olaf Scholz nie chce przekazywaæ Ukrainie. Wojskowi twierdzili w rozmowie, ¿e gdyby jednak Niemcy przekaza³y Taurusy, to by³oby ich najwy¿ej sto, a pierwsza transza - piêædziesi¹t sztuk - by³aby gotowa za kilka miesiêcy. Oficerowie mówili te¿ o tym, ¿e trzeba dwudziestu pocisków, by zniszczyæ most krymski ³¹cz¹cy Rosjê z Krymem, ale nie ma dobrych danych dotycz¹cych rosyjskiego systemu obrony mostu. W rozmowie padaj¹ te¿ informacje o si³ach sojuszniczych. Rzeczniczka urzêdu wojskowego kontrwywiadu poda³a, ¿e zastosowano wszystkie mo¿liwe œrodki, ¿eby sprawdziæ, czy mo¿liwa jest wiêksza skala pods³uchów w si³ach lotniczych. Kontrwywiad nie skomentowa³ samej treœci rozmowy. \\\"Rosjanie boj¹ siê, ¿e kanclerz mo¿e ulec presji\\\" \\\"Rozmowa nie jest spektakularna - jest przygotowaniem do zbriefowania ministra i kanclerza. Mowa o tym, ile czasu trzeba do pe³nego wyszkolenia ¿o³nierzy ukraiñskich, co mo¿na zrobiæ ¿eby skróciæ ten czas i co zrobiæ, ¿eby wtedy Bundeswehra nie uczestniczy³a w planowaniu ataków\\\" - skomentowa³a na portalu X wicedyrektora Oœrodka Studiów Wschodnich Justyna Gotkowska. Jak zaznaczy³a, by³a mowa o tym, \\\"w czym Taurusy s¹ lepsze od francuskich i brytyjskich pocisków\\\". \\\"Mog¹ uderzyæ w most kerczeñski czy sk³ady amunicji. Rozmowa zawiera wiele ciekawych technicznych szczegó³ów, ale to ju¿ da³ bardziej zainteresowanych. Pewne jest, ¿e nie ma w niej nic skandalicznego\\\" - oceni³a ekspertka. Wed³ug Justyny Gotkowskiej \\\"Rosjanie boj¹ siê, ¿e kanclerz mo¿e ulec presji i chc¹ temu zapobiec, poprzez takie wrzutki\\\".\",\"url\":\"https://wiadomosci.gazeta.pl/wiadomosci/7,114881,30757237,rosjanie-podsluchali-rozmowe-oficerow-bundeswehry-niemiecki.html\",\"image\":\"https://bi.im-g.pl/im/92/55/1d/z30757266IER,Zdjecie-ilustracyjne.jpg\",\"publish_date\":\"2024-03-02 12:02:00\",\"author\":\"Marcin Koz³owski\",\"authors\":[\"Marcin Koz³owski\"],\"language\":\"pl\",\"source_country\":\"pl\"},{\"id\":195290303,\"title\":\"Rozeœmiany £awrow z szefem wêgierskiego MSZ. Spotkali siê w dniu pogrzebu Nawalnego\",\"text\":\"Ministerstwo Spraw Zagranicznych Wêgier do tej pory nie odnios³o siê oficjalnie do sprawy œmierci rosyjskiego opozycjonisty Aleksieja Nawalnego, który 16 lutego zmar³ w kolonii karnej. Jedynym politykiem z Fideszu, który zabra³ g³os, by³ przewodnicz¹cy parlamentarnej komisji spraw zagranicznych Zsolt Nemeth. Nie skrytykowa³ on jednak W³adimira Putina, a jedynie system rosyjskiego wiêziennictwa. Z kolei podczas g³osowania w Parlamencie Europejskim nad rezolucj¹ potêpiaj¹c¹ œmieræ Nawalnego europos³owie z partii Viktora Orbana wstrzymali siê od g³osu. Wêgierska partia rz¹dz¹ca dba za to o dobre stosunki z Rosj¹ i W³adimirem Putinem. Szef wêgierskiego MSZ pochwali³ siê spotkaniem z £awrowem. W dniu pogrzebu Nawalnego Peter Szijjarto, szef wêgierskiego resortu spraw zagranicznych, spotka³ siê w tureckiej Antalyi (podczas trwaj¹cego tam forum dyplomatycznego) ze swoim rosyjskim odpowiednikiem Siergiejem £awrowem, czym polityk nie omieszka³ pochwaliæ siê w mediach spo³ecznoœciowych. Opublikowa³ na Facebooku nagranie, na którym widaæ rozmowê polityków usadzonych obok siebie przy stole, podczas której wyraŸnie maj¹ dobre humory. Rozmowy na forum mia³y miejsce w tym samym czasie, gdy w Moskwie trwa³ pogrzeb Aleksieja Nawalnego. Kwestia œmierci Nawalnego pojawi³a siê natomiast podczas inauguracji obrad wêgierskiego parlamentu po przerwie zimowej. Pamiêæ opozycjonisty zosta³a uczczona minut¹ ciszy. Wiêkszoœæ rz¹dz¹ca nie uszanowa³a jednak tego symbolu, a sam Orban komentowa³, ¿e \\\"szowinista nie zas³uguje na szacunek w wêgierskim parlamencie\\\" - wskazuje Daily News Hungary. Rosyjskie media przemilcza³y pogrzeb Nawalnego. Mówili o innym zmar³ym W g³ównych rosyjskich kana³ach telewizyjnych, jak Rossija 1 czy NTV w pi¹tek pró¿no by³o szukaæ wzmianki na temat pogrzebu Aleksieja Nawalnego w Moskwie - zwróci³ uwagê niezale¿ny serwis Agenstwo. Mo¿na by³o za to obejrzeæ d³ugie materia³y na temat œmierci ostatniego premiera ZSRR Niko³aja Ry¿kowa, o którego œmierci poinformowano w œrodê 28 lutego. Jego pogrzeb odbywa³ siê w Moskwie tego samego dnia co opozycjonisty. Ry¿kow z robotnika w fabryce na Uralu sta³ siê drug¹ najwa¿niejsz¹ osob¹ w pañstwie (formalnie, bo w rzeczywistoœci nie podejmowa³ ¿adnych decyzji, a by³ podporz¹dkowany partii). Ry¿kow popiera³ wprowadzenie wojsk rosyjskich na Krym w 2014 roku oraz aneksjê ukraiñskich obwodów w 2022 roku.\",\"url\":\"https://wiadomosci.gazeta.pl/wiadomosci/7,114881,30757181,rozesmiany-lawrow-z-szefem-wegierskiego-msz-spotkali-sie-w.html\",\"image\":\"https://bi.im-g.pl/im/7c/55/1d/z30757244IER,Siergiej-Lawrow-i-Peter-Szijjarto.jpg\",\"publish_date\":\"2024-03-02 12:06:00\",\"author\":\"Joanna Szwalikowska\",\"authors\":[\"Joanna Szwalikowska\"],\"language\":\"pl\",\"source_country\":\"pl\"}]}";
        //News = System.Text.Json.JsonSerializer.Deserialize<NewsData>(content);
        CollectionOfNews = new ObservableCollection<NewsItem> { News.News[0], News.News[1], News.News[2] };
        BindingContext = this;
    }

    private async void CheckOutNews(object sender, TappedEventArgs e)
    {
        var ChoosenNews = e.Parameter as NewsItem;
        if (ChoosenNews != null)
        {
            await Navigation.PushAsync(new NavigationPage(new PageOfNews(ChoosenNews)));
        }
    }

    private void Scrolling(object sender, ScrolledEventArgs e)
    {
        if (!(sender is ScrollView scrollView))
        {
            return;
        }
        var scrollSpace = scrollView.ContentSize.Height - scrollView.Height;
        if (scrollSpace > e.ScrollY+10)
        {
            return;
        }
        else
        {
            if (whichNews<=27)
            {
                CollectionOfNews.Add(News.News[whichNews]);
                whichNews++;
                CollectionOfNews.Add(News.News[whichNews]);
                whichNews++;
            }
        }
    }
}