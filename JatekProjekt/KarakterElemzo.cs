namespace JatekProjekt;

public class KarakterElemzo
{
    private readonly List<Karakter> karakterek;

    // Konstruktor: példányosítja az adatbázis-kezelőt és betölti az adatokat a listába
    public KarakterElemzo()
    {
        var dbKezelo = new AdatbazisKezelo();
        karakterek = dbKezelo.AdatokBetoltese();
    }

    public List<string> MagasSzintuKarakterek()
    {
        var eredmeny = karakterek
            .Where(k => k.Szint > 70)
            .Select(k => k.Nev);

        return eredmeny.ToList();
    }

    // Gyűjtsd ki az összes "Mágus" osztályú karakter nevét!
    public List<string> MagusOsztalyuNevek()
    {
        var eredmeny = karakterek
            .Where(k => k.Osztaly.Equals("Mágus"))
            .Select(k => k.Nev).ToList();

        return eredmeny;
    }

    // Add vissza az összes karakter nevét egy egyszerű listában!
    public List<string> OsszesKarakterNevListaba()
    {
        var eredmeny = karakterek.Select(k => k.Nev);

        return eredmeny.ToList();
    }

    // Szűrd ki azokat a karaktereket, amelyek valamelyik "EU-"-val kezdődő szerveren játszanak, és add vissza a nevüket listában!
    public List<string> EuSzerveresKarakterek()
    {
        var eredmeny = karakterek
            .Where(k => k.Szerver.StartsWith("EU-"))
            .Select(k => k.Nev);

        return eredmeny.ToList();
    }

    // Válaszd ki az összes egyedi (különböző) szervernevet, ahol a karakterek játszanak (Distinct segítségével)!
    public List<string> EgyediSzerverNevek()
    {
        var eredmeny = karakterek
            .Select(k => k.Szerver)
            .Distinct()
            .ToList();

        return eredmeny;
    }


    // ==========================================
    // 2. EGYSZERŰ AGGREGÁCIÓK ÉS ELDÖNTÉsek
    // ==========================================

    // Számold meg Sum segítségével, hogy mennyi a karakterek összes aranya a szerveren!
    public int OsszesAranyMennyiseg()
    {
        var eredmeny = karakterek
            .Select(k => k.Arany)
            .Sum();
        return eredmeny;
    }

    // Számítsd ki Average segítségével a karakterek átlagos szintjét double értékként!
    public double AtlagSzint()
    {
        var eredmeny = karakterek
            .Select(k => k.Szint)
            .Average();

        return eredmeny;
    }

    // Állapítsd meg Any segítségével, hogy van-e olyan "Tolvaj" osztályú karakter, akinek a szintje eléri vagy meghaladja a 40-et!
    public bool VaneMagasSzintuTolvaj()
    {
        var eredmeny = karakterek
            .Any(k => k.Osztaly.Equals("Tolvaj") && k.Szint >= 40);

        return eredmeny;
    }

    // Ellenőrizd Any segítségével, hogy létezik-e olyan karakter, akinek 10 000 aranynál több van a zsebében!
    public bool VanETizEzerAranynalTobb()
    {
        var eredmeny = karakterek
            .Any(k => k.Arany > 10000);

        return eredmeny;
    }

    // Keresd meg FirstOrDefault segítségével az első olyan karaktert, akinek az aranya meghaladja az 5000-et!
    public Karakter LegelsoOvenKarakter()
    {
        var eredmeny = karakterek.FirstOrDefault(k => k.Arany > 5000);

        return eredmeny;
    }


    // ==========================================
    // 3. RENDEZÉsek ÉS KERESÉsek
    // ==========================================

    // Add vissza az összes karaktert szint szerint csökkenő, azon belül pedig név szerint növekvő sorrendbe rendezve!
    public List<Karakter> KarakterekSzintSzerintRendezve()
    {
        var eredmeny = karakterek
            .Select(k => k)
            .OrderByDescending(k => k.Szint)
            .ThenBy(k => k.Nev).ToList();

        return eredmeny;
    }

    // Keresd meg OrderByDescending és FirstOrDefault segítségével annak a karakternek a nevét, akinek a legtöbb aranya van!
    public string LeggazdagabbKarakterNeve()
    {
        var eredmeny = karakterek
            .OrderByDescending(k => k.Arany)
            .FirstOrDefault();
        return eredmeny.Nev;
    }

    // Keresd meg OrderBy és FirstOrDefault segítségével a legszegényebb (legkevesebb aranyú) karakter nevét!
    public string LegszegenyebbKarakterNeve()
    {
        var eredmeny = karakterek
            .OrderBy(k => k.Arany)
            .FirstOrDefault();
        return eredmeny.Nev;
    }

    // Keresd meg Max segítségével a legmagasabb szintű karakter szintjét!
    public int LegmagasabbSzint()
    {
        var eredmeny = karakterek.Max(k => k.Szint);

        return eredmeny;
    }

    // Rendezd a karaktereket a nevük hossza szerint növekvő sorrendbe!
    public List<Karakter> KarakterekNevHosszaSzerint()
    {
        var eredmeny = karakterek
            .OrderBy(k => k.Nev.Length).ToList();

        return eredmeny;
    }


    // ==========================================
    // 4. CSOPORTOSÍTÁS ÉS ÖSSzetettebb LINQ MŰVELETek
    // ==========================================

    // Csoportosítsd a karaktereket az Osztaly tulajdonságuk alapján, és alakítsd át ToDictionary-vel úgy, hogy a kulcs az osztály neve, az érték pedig a karakterek darabszáma legyen!
    public Dictionary<string, int> OsztalyonkentKarakterSzam()
    {
        var eredmeny = karakterek
            .GroupBy(d => d.Osztaly)
            .ToDictionary(k => k.Key, k => k.Count());

        return eredmeny;
    }

    // Csoportosítsd a karaktereket a Szerver tulajdonságuk alapján, és számold meg, hány karakter játszik az egyes szervereken!
    public Dictionary<string, int> SzerverenKarakterekSzama()
    {
        var eredmeny = karakterek
            .GroupBy(k => k.Szerver)
            .ToDictionary(
                k => k.Key,
                k => k.Count()
            );
        return eredmeny;
    }

    // Gyűjtsd ki az összes "Harcos" osztályú karakter azonosítóját (Id) egy listába!
    public List<int> HarcosokAzonositoi()
    {
        var eredmeny = karakterek
            .Where(k => k.Osztaly.Equals("Harcos"))
            .Select(k => k.Id).ToList();

        return eredmeny;
    }

    // Állapítsd meg, hogy van-e olyan 50-es szintnél magasabb Mágus, akinek 3000-nél kevesebb aranya van!
    public bool VanETolvajSpecKondicioval()
    {
        var eredmeny = karakterek.Any(k => k.Osztaly.Equals("Mágus") && k.Szint > 50 && k.Arany < 3000);
        return eredmeny;
    }

    // Keresd meg és add vissza a legmagasabb szintű karakter objektumát!
    public Karakter LegmagasabbSzintuKarakterObjektum()
    {
        var eredmeny = karakterek
            .OrderByDescending(k => k.Szint)
            .FirstOrDefault();

        return eredmeny;
    }
}