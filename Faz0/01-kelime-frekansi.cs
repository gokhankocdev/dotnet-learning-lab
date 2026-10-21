

public class kelimefrekansi
{
    public static void Main(string[] args)
    {
        string metin = "Bu bir örnek metindir. Bu metin, metin içindeki kelime frekansını hesaplamak için kullanılacaktır.";
        Console.WriteLine(metin);
        KelimeFrekansiHesapla(metin);
        
    }

    public static void KelimeFrekansiHesapla(string metin)
    {
        string[] kelimeler = metin.Split(new char[] { ' ', '.', ',', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);
        Dictionary<string, int> frekans = new Dictionary<string, int>();
        foreach (string kelime in kelimeler)
        {
            string kucukKelime = kelime.ToLower();
            if (frekans.ContainsKey(kucukKelime))
            {
                frekans[kucukKelime]++;
            }
            else
            {
                frekans[kucukKelime] = 1;
            }
        }
        foreach (var item in frekans.OrderByDescending(x => x.Value).ThenBy(x => x.Key))
        {
            Console.WriteLine($"{item.Key}: {item.Value}");
        }
    }
}


