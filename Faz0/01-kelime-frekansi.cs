

public class kelimefrekansi
{
    public static void Main(string[] args)
    {
        string metin = "Bu bir örnek metindir. Bu metin, metin içindeki bu kelime frekansını hesaplamak için kullanılacaktır.";
        Console.WriteLine(metin);
        KelimeFrekansiHesapla(metin);
        //KelimeFrekansiHesaplaNoLinq(metin);

    }

    public static void KelimeFrekansiHesapla(string metin)
    {
        string[] kelimeler = metin.ToLower().Split(new char[] { ' ', '.', ',', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);
        Dictionary<string, int> frekans = new Dictionary<string, int>();

        foreach (string kelime in kelimeler)
        {

            if (frekans.ContainsKey(kelime))
            {
                frekans[kelime]++;
            }
            else
            {
                frekans[kelime] = 1;
            }
        }
        foreach (var item in frekans.OrderByDescending(x => x.Value).ThenBy(x => x.Key))
        {
            if (item.Value >= 3)
                Console.WriteLine($"{item.Key}: {item.Value}");
        }
    }


    public static void KelimeFrekansiHesaplaNoLinq(string metin)
    {
        string[] kelimeler = metin.ToLower().Split(new char[] { ' ', '.', ',', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);
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



        foreach (var item in frekansList)
        {
            if (item.Value >= 3)
                Console.WriteLine($"{item.Key}: {item.Value}");
        }
    }
}


