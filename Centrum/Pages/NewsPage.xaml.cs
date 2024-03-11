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
        //string content = "{\"offset\":0,\"number\":3,\"available\":1161,\"news\":[{\"id\":195290299,\"title\":\"Chcia�a nagra� wideo w Zakopanem. \\\"Halo, nie ma tak! Chcecie, to sobie zap�a�cie\\\"\",\"text\":\"W pi�tek 1 marca znana z serialu \\\"M jak mi�o��\\\" Hanna Turnau opublikowa�a w mediach spo�eczno�ciowych nagranie z Zakopanego. Aktorka chcia�a nagra� film dla swoich widz�w, jednak na przeszkodzie stan�� jej jeden z m�czyzn, kt�ry pracowa� na Krup�wkach. Zakopane. Chcia�a nagrywa� na Krup�wkach. Sprzeciwi� si� m�czyzna przebrany za bia�ego misia Na popularnej w Zakopanem ulicy oraz znanym turystom deptaku o ka�dej porze roku mo�na spotka� osoby w strojach postaci z bajek czy przebrane za zwierzaki, kt�re pobieraj� op�aty od przechodni�w za wsp�lne zdj�cie czy nagranie. Aktorka Hanna Turnau przekona�a si� o tym, gdy pr�bowa�a nagra� film na ulicy. M�czyzna, kt�ry pracowa� na Krup�wkach w stroju nied�wiedzia od razu zareagowa�. - Halo, nie ma tak! Chcecie, to sobie zap�a�cie za foto nie ma sprawy - zwr�ci� si� do Turnau m�czyzna, gdy zauwa�y�, �e kobieta mo�e kierowa� kamer� w jego stron�. - Ale my tu nagrywamy - odpowiedzia�a mu aktorka. - Oboj�tnie, czy nagrywasz, czy cokolwiek. My tutaj stoimy i jak chcesz z nami (zdj�cie - red.), to zap�a� nam - skwitowa� m�czyzna. Turnau skomentowa�a tylko, �e \\\"Zakopane to jest bardzo sympatyczne miasto\\\" i zako�czy�a nagranie. Kobieta opublikowa�a te� nagranie z ca�ej sytuacji na Instagramie. \\\"Kiedy chcesz nagrywa� self-tape na Krup�wkach lepiej we� got�wk�. Z �ycia wzi�te\\\" - podpisa�a sw�j film, kt�ry opublikowa�a w mediach spo�eczno�ciowych. Zakopane. ��daj� op�aty za wsp�lne zdj�cie lub film U�ytkownicy serwisu nie kryli oburzenia. \\\"A paragony pan daje po zap�acie za zdj�cie?\\\" - pytali. Inni zwracali uwag�, �e teraz ludzie s� wyj�tkowo \\\"pazerni na kas�\\\" i przyznawali, �e sami spotkali si� z podobnych zachowaniem w Zakopanem. \\\"Op�ata od selfie. W sumie, dlaczego nie\\\" - skomentowa� film w serwisie X Krzysztof Bontal po�rednik kredytowy i redaktor portalu Pierwszemieszkanie.com. �artowano r�wnie�, �e samo obserwowanie tych g�rskich okolic w Google Maps sporo kosztuje. \\\"Przejecha�em kursorem przez Zakopane i pobra�o mi 20 z�\\\" - pisa� z ironi� jeden z internaut�w w serwisie X.\",\"url\":\"https://wiadomosci.gazeta.pl/wiadomosci/7,114883,30757140,chciala-nagrywac-ale-bialy-mis-zazadal-oplaty-zakopane.html\",\"image\":\"https://bi.im-g.pl/im/9e/55/1d/z30757278IER,Aktorka-probowala-nagrywac-film-w-Zakopanem.jpg\",\"publish_date\":\"2024-03-02 12:05:00\",\"author\":\"Karolina Trela\",\"authors\":[\"Karolina Trela\"],\"language\":\"pl\",\"source_country\":\"pl\"},{\"id\":195290301,\"title\":\"Rosjanie pods�uchali rozmow� oficer�w Bundeswehry. Niemiecki kontrwywiad bada spraw�\",\"text\":\"Ujawnione nagranie to jedna kilkudziesi�ciominutowa wideorozmowa - eksperci uwa�aj�, �e autentyczna. Mia�o do niej doj�� 19 lutego przez aplikacj� do rozm�w zdalnych. Specjali�ci twierdz�, �e aplikacja, z kt�rej skorzystali wojskowi, nie jest szczeg�lnie zabezpieczona, co mog�o u�atwi� przechwycenie rozmowy. Dodatkowo jeden z czterech uczestnicz�cych w wymianie opinii oficer�w Luftwaffe, inspektor Si� Powietrznych Niemiec gen. Ingo Gerhartz, by� w tym czasie w Azji. Rosja. Ujawniono nagranie z rozmowy oficer�w Bundeswehry Rozmowa dotyczy�a pocisk�w Taurus, kt�rych kanclerz Niemiec Olaf Scholz nie chce przekazywa� Ukrainie. Wojskowi twierdzili w rozmowie, �e gdyby jednak Niemcy przekaza�y Taurusy, to by�oby ich najwy�ej sto, a pierwsza transza - pi��dziesi�t sztuk - by�aby gotowa za kilka miesi�cy. Oficerowie m�wili te� o tym, �e trzeba dwudziestu pocisk�w, by zniszczy� most krymski ��cz�cy Rosj� z Krymem, ale nie ma dobrych danych dotycz�cych rosyjskiego systemu obrony mostu. W rozmowie padaj� te� informacje o si�ach sojuszniczych. Rzeczniczka urz�du wojskowego kontrwywiadu poda�a, �e zastosowano wszystkie mo�liwe �rodki, �eby sprawdzi�, czy mo�liwa jest wi�ksza skala pods�uch�w w si�ach lotniczych. Kontrwywiad nie skomentowa� samej tre�ci rozmowy. \\\"Rosjanie boj� si�, �e kanclerz mo�e ulec presji\\\" \\\"Rozmowa nie jest spektakularna - jest przygotowaniem do zbriefowania ministra i kanclerza. Mowa o tym, ile czasu trzeba do pe�nego wyszkolenia �o�nierzy ukrai�skich, co mo�na zrobi� �eby skr�ci� ten czas i co zrobi�, �eby wtedy Bundeswehra nie uczestniczy�a w planowaniu atak�w\\\" - skomentowa�a na portalu X wicedyrektora O�rodka Studi�w Wschodnich Justyna Gotkowska. Jak zaznaczy�a, by�a mowa o tym, \\\"w czym Taurusy s� lepsze od francuskich i brytyjskich pocisk�w\\\". \\\"Mog� uderzy� w most kercze�ski czy sk�ady amunicji. Rozmowa zawiera wiele ciekawych technicznych szczeg��w, ale to ju� da� bardziej zainteresowanych. Pewne jest, �e nie ma w niej nic skandalicznego\\\" - oceni�a ekspertka. Wed�ug Justyny Gotkowskiej \\\"Rosjanie boj� si�, �e kanclerz mo�e ulec presji i chc� temu zapobiec, poprzez takie wrzutki\\\".\",\"url\":\"https://wiadomosci.gazeta.pl/wiadomosci/7,114881,30757237,rosjanie-podsluchali-rozmowe-oficerow-bundeswehry-niemiecki.html\",\"image\":\"https://bi.im-g.pl/im/92/55/1d/z30757266IER,Zdjecie-ilustracyjne.jpg\",\"publish_date\":\"2024-03-02 12:02:00\",\"author\":\"Marcin Koz�owski\",\"authors\":[\"Marcin Koz�owski\"],\"language\":\"pl\",\"source_country\":\"pl\"},{\"id\":195290303,\"title\":\"Roze�miany �awrow z szefem w�gierskiego MSZ. Spotkali si� w dniu pogrzebu Nawalnego\",\"text\":\"Ministerstwo Spraw Zagranicznych W�gier do tej pory nie odnios�o si� oficjalnie do sprawy �mierci rosyjskiego opozycjonisty Aleksieja Nawalnego, kt�ry 16 lutego zmar� w kolonii karnej. Jedynym politykiem z Fideszu, kt�ry zabra� g�os, by� przewodnicz�cy parlamentarnej komisji spraw zagranicznych Zsolt Nemeth. Nie skrytykowa� on jednak W�adimira Putina, a jedynie system rosyjskiego wi�ziennictwa. Z kolei podczas g�osowania w Parlamencie Europejskim nad rezolucj� pot�piaj�c� �mier� Nawalnego europos�owie z partii Viktora Orbana wstrzymali si� od g�osu. W�gierska partia rz�dz�ca dba za to o dobre stosunki z Rosj� i W�adimirem Putinem. Szef w�gierskiego MSZ pochwali� si� spotkaniem z �awrowem. W dniu pogrzebu Nawalnego Peter Szijjarto, szef w�gierskiego resortu spraw zagranicznych, spotka� si� w tureckiej Antalyi (podczas trwaj�cego tam forum dyplomatycznego) ze swoim rosyjskim odpowiednikiem Siergiejem �awrowem, czym polityk nie omieszka� pochwali� si� w mediach spo�eczno�ciowych. Opublikowa� na Facebooku nagranie, na kt�rym wida� rozmow� polityk�w usadzonych obok siebie przy stole, podczas kt�rej wyra�nie maj� dobre humory. Rozmowy na forum mia�y miejsce w tym samym czasie, gdy w Moskwie trwa� pogrzeb Aleksieja Nawalnego. Kwestia �mierci Nawalnego pojawi�a si� natomiast podczas inauguracji obrad w�gierskiego parlamentu po przerwie zimowej. Pami�� opozycjonisty zosta�a uczczona minut� ciszy. Wi�kszo�� rz�dz�ca nie uszanowa�a jednak tego symbolu, a sam Orban komentowa�, �e \\\"szowinista nie zas�uguje na szacunek w w�gierskim parlamencie\\\" - wskazuje Daily News Hungary. Rosyjskie media przemilcza�y pogrzeb Nawalnego. M�wili o innym zmar�ym W g��wnych rosyjskich kana�ach telewizyjnych, jak Rossija 1 czy NTV w pi�tek pr�no by�o szuka� wzmianki na temat pogrzebu Aleksieja Nawalnego w Moskwie - zwr�ci� uwag� niezale�ny serwis Agenstwo. Mo�na by�o za to obejrze� d�ugie materia�y na temat �mierci ostatniego premiera ZSRR Niko�aja Ry�kowa, o kt�rego �mierci poinformowano w �rod� 28 lutego. Jego pogrzeb odbywa� si� w Moskwie tego samego dnia co opozycjonisty. Ry�kow z robotnika w fabryce na Uralu sta� si� drug� najwa�niejsz� osob� w pa�stwie (formalnie, bo w rzeczywisto�ci nie podejmowa� �adnych decyzji, a by� podporz�dkowany partii). Ry�kow popiera� wprowadzenie wojsk rosyjskich na Krym w 2014 roku oraz aneksj� ukrai�skich obwod�w w 2022 roku.\",\"url\":\"https://wiadomosci.gazeta.pl/wiadomosci/7,114881,30757181,rozesmiany-lawrow-z-szefem-wegierskiego-msz-spotkali-sie-w.html\",\"image\":\"https://bi.im-g.pl/im/7c/55/1d/z30757244IER,Siergiej-Lawrow-i-Peter-Szijjarto.jpg\",\"publish_date\":\"2024-03-02 12:06:00\",\"author\":\"Joanna Szwalikowska\",\"authors\":[\"Joanna Szwalikowska\"],\"language\":\"pl\",\"source_country\":\"pl\"}]}";
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