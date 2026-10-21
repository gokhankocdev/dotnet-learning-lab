

public class KelimeFrekansi
{
    public static void Main(string[] args)
    {


    }

    public static void KelimeFrekansiHesapla(string metin)
    {
        char[] ayraclar = metin.Where(c => char.IsPunctuation(c) || char.IsWhiteSpace(c)).Distinct().ToArray();
        string[] kelimeler = metin.ToLower(new System.Globalization.CultureInfo("tr-TR")).Split(ayraclar, StringSplitOptions.RemoveEmptyEntries);

        Dictionary<string, int> frekans = new Dictionary<string, int>();

        foreach (string kelime in kelimeler)
        {
            frekans.TryGetValue(kelime, out int mevcutSayi);
            frekans[kelime] = mevcutSayi + 1;
        }


        Yazdir(frekans);
    }

    public static void Yazdir(Dictionary<string, int> frekans)
    {
        foreach (var item in frekans.OrderByDescending(x => x.Value).ThenBy(x => x.Key).Take(3))
        {
            Console.WriteLine($"{item.Key}: {item.Value}");
        }
    }


    public static void KelimeFrekansiHesaplaNoLinq(string metin)
    {
        char[] ayraclar = metin.Where(c => char.IsPunctuation(c) || char.IsWhiteSpace(c)).Distinct().ToArray();
        string[] kelimeler = metin.ToLower(new System.Globalization.CultureInfo("tr-TR")).Split(ayraclar, StringSplitOptions.RemoveEmptyEntries);
        Dictionary<string, int> frekans = new Dictionary<string, int>();
        foreach (string item in kelimeler)
        {

            if (frekans.ContainsKey(item))
            {
                frekans[item]++;
            }
            else
            {
                frekans[item] = 1;
            }
        }

        List<KeyValuePair<string, int>> frekansList = new List<KeyValuePair<string, int>>(frekans);

        frekansList.Sort((x, y) =>
        {
            int valueComparison = y.Value.CompareTo(x.Value);
            if (valueComparison == 0)
            {
                return x.Key.CompareTo(y.Key);
            }
            return valueComparison;
        });



        YazdirList(frekansList);
    }

    public static void YazdirList(List<KeyValuePair<string, int>> frekans)
    {
        foreach (var item in frekans.OrderByDescending(x => x.Value).ThenBy(x => x.Key).Take(3))
        {
            Console.WriteLine($"{item.Key}: {item.Value}");
        }
    }

}


