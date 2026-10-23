using System;
using System.Collections.Generic;
using System.Text;

namespace Faz0.ValueReference;

public static class ValueReferenceDersi
{
    class Kutu { public int Deger; }
    struct Nokta { public int X; }

    public static void Calistir()
    {
        // SORU 1
        int a = 5;
        int b = a;
        b = 10;
        Console.WriteLine(a);

        // SORU 2
        var k1 = new Kutu { Deger = 5 };
        var k2 = k1;
        k2.Deger = 10;
        Console.WriteLine(k1.Deger);

        // SORU 3
        var n1 = new Nokta { X = 5 };
        var n2 = n1;
        n2.X = 10;
        Console.WriteLine(n1.X);

        // SORU 4
        static void Degistir(Kutu k) { k.Deger = 99; }
        static void YeniAta(Kutu k) { k = new Kutu { Deger = 1 }; }

        var kutu = new Kutu { Deger = 5 };
        Degistir(kutu);
        Console.WriteLine(kutu.Deger);   // 4a
        YeniAta(kutu);
        Console.WriteLine(kutu.Deger);   // 4b

        // SORU 5
        List<Nokta> noktalar = [new Nokta { X = 1 }];
        var ilk = noktalar[0];
        ilk.X = 50;
        Console.WriteLine(noktalar[0].X);
        // Ek soru: "noktalar[0].X = 50;" yazsaydık ne olurdu?

        // SORU 6
        string s1 = "merhaba";
        string s2 = s1;
        s2 += " dünya";
        Console.WriteLine(s1);
        // string bir reference type. Peki sonuç neden böyle?

        // SORU 7 (ödevine bağlı)
        var frekans = new Dictionary<string, int> { ["bu"] = 3 };
        var sayi = frekans["bu"];
        sayi++;
        Console.WriteLine(frekans["bu"]);
    }

}
