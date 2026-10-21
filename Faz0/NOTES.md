# 01 - Kelime Frekansı

## Ne öğrendim

- frekans[k]++ hem okuma hem yazma işlemi iki iş yaptığını TryGetValue bunu bire indirmesi
- dictionary de henüz anahtar olmadığı için hata fırlatması muhtemelken frekans[k]=1 hata vermesini engelliyor. okuma ve yazma farklı davranıyor.
- toLower ve toUpper  kullanımlarında dil bağımlılığı kısmına dikkat edilmesi gerektiği.
- char sınıfı metodlarının kullanımı detaylı bilmesemde bu tarz işlemler için mutlaka bir metodun mevcut olma ihtimali ve bunun araştırılması gerektiği
- SRP de return eden metod unit test ile doğrulanabilir fakatekrana yazan bir metodu doğrulanamaz.

## Nerede zorlandım

O kadar paslanmışım ki nereden başlayacağımı bilemedim. Algoritma kuramadım. Neleri kullanmam gerektiğini algıalayamadım ve kavram kargaşaşası yaşadım. 
Kodun mantığını anlamak için biraz araştırma yaptım. 


## Sorular

1.Döngüde her kelime için Dictionary’de kaç kez arama yapılıyor? ContainsKey çağrısını, sonra frekans[x]++ satırını düşün; ++ önce okur, sonra yazar. Bunu tek aramaya indiren bir Dictionary metodu var, bul.
2.if bloğunu kaldırıp sadece frekans[kucukKelime]++ yazsaydın, ilk kelimede ne olurdu? Neden?
3.Türk I problemi: ToLower() hangi kültürü kullanır? Bu kod Türkçe Windows’ta ve İngilizce bir Linux sunucuda “IŞIK” kelimesi için aynı sonucu verir mi? Bu, gerçek projelerde ciddi hatalara yol açan ünlü bir problem; araştır ve NOTES.md’ye yaz. İpucu: Dictionary’nin constructor’ı bir StringComparer alabiliyor.
4.Ayraç listesine bak. Metinde ;, :, tırnak, parantez, satır sonu ya da tab olursa ne olur? Ayraçları tek tek saymak yerine char sınıfının hangi metotları işini görürdü?
5.KelimeFrekansiHesapla hem hesaplıyor hem ekrana yazıyor. Bunu test etmek isteseydin neden zorlanırdın? Sonucu return eden bir yapı nasıl olurdu? (İpucu: SRP)
6.Sınıf adı kelimefrekansi. C#’ta sınıf isimlendirme kuralı nedir?


## Soruların cevapları
1.frekans.ContainsKey(kelime) ile anahtarın varlığı kontrol edilir (1. arama) frekans[kelime]++ yapıldığında 2. arama TryGetValue metodu tek metodla iki işi yapar.
2.metin içinde kelime bulamadığında hata verirdi. fakat zaten kelimeler metin iinden split edildiği için hata veremezdi.
3.evet sunucu diline göre farklılık gösterier IŞIK ingilizcede isik olarak çevirir. dolayısıyla CUltureInfo belirtmek gerekir her sunucuda türkçe çalıştrımak istediğimizde.
4.bilmiyorum.
5.SRP ye göre her metod bir işten sorumlu olmalı. hesaplma işi ile ekrana yazma işi ayrılmalı. metod değitiğinde diğer metoun etkinememesi veya onda da değişiklik yapma zorunluluğu ortadan kalması için önemli.
6.uygun değil camel case olmalı.

## Doğru Cevaplar
1. Kısmen doğru. Aslında 3 işlem var. frekans[k]++ tek satır gibi görünüyor ama iki iş yapıyor: önce indexer ile değeri okuyor, sonra yeni değeri yazıyor. 
Toplam: ContainsKey + okuma + yazma = 3 arama.
TryGetValue bunu 2'ye indirir: biri okurken, biri yazarken. Tek aramaya inmenin yolu da var: CollectionsMarshal.GetValueRefOrAddDefault. 
Bunu performans haftasında göreceğiz. Şimdilik TryGetValue ile düzeltmen yeterli.

2. Yanlış, ve bu önemli bir kavram karışıklığı. Soru metinle değil, Dictionary ile ilgiliydi. Her kelime ilk kez görüldüğünde Dictionary’de henüz o anahtar yok.
++ önce okumaya çalışır, indexer’ın okuma kısmı anahtarı bulamaz ve KeyNotFoundException fırlatır. Yani kod daha ilk kelimede çöker.
İlginç bir detay: indexer’ın yazma kısmı (frekans[k] = 1) anahtar yoksa hata vermez, yeni kayıt ekler. Okuma ve yazmanın farklı davranması, mülakatlarda sevilen bir sorudur.

3. Doğru yöndesin, iki düzeltme var.

İngilizce kültürde sonuç “isik” değil, “işik” olur. Ş her kültürde ş’ye dönüşür; değişen sadece I. Sonuç olarak “IŞIK” ve “ışık” farklı kelimeler sayılır.
Çözüm her zaman “Türkçe kültürü belirt” değil, amaca göre seçmek:
Kullanıcıya ait Türkçe metin için kültürü açıkça belirt: new CultureInfo("tr-TR").
Teknik değerler için (dosya adı, e-posta, enum, cache key) kültürden bağımsız karşılaştır: ToLowerInvariant veya StringComparer.OrdinalIgnoreCase. 
Sunucu Türkçe olursa "FILE".ToLower() sonucu "fıle" olur ve teknik karşılaştırmalar bozulur.
Altın kural: Kodun, sunucunun dil ayarına gizlice bağlı olmamalı.

4. “Bilmiyorum” demen dürüst ve doğru bir cevap. char sınıfında karakter türünü soran metotlar var:
char.IsLetter, char.IsLetterOrDigit, char.IsWhiteSpace, char.IsPunctuation. Bunları kullanınca ayraçları tek tek saymazsın.
Kelime “harflerden oluşan ardışık karakter dizisi” olur; metni karakter karakter gezer, harf görünce kelimeyi büyütür, harf olmayan bir karakter görünce kelimeyi bitirirsin.

Kendine sor: “Ankara’da” bir kelime mi, iki kelime mi? Kararını NOTES.md’ye yaz.

5. Doğru. Bir şey ekleyeyim: SRP’nin resmi tanımı “bir sınıfın değişmek için tek bir nedeni olmalı” şeklindedir, ve senin söylediğin buna çok yakın. 
Pratik faydası test edilebilirlik. Sonucu return eden bir metodu unit test ile doğrulayabilirsin, ama ekrana yazan bir metodu doğrulayamazsın. 
Aynı metodu yarın bir API’den de kullanabilirsin.

6. Yanlış. C#’ta sınıf ve metot isimleri PascalCase yazılır (KelimeFrekansi). camelCase yerel değişkenler ve parametreler içindir (kucukKelime). 
Kendi kodunda ikisini de doğru kullanmışsın; sadece kuralın adı karışmış.

Bugünün 5 İngilizce terimi: key (anahtar), value (değer), lookup (arama), whitespace (boşluk karakterleri), culture (kültür/yerel ayar). Hepsi bu ödevden.