# Kütüphane Yönetim Sistemi

Basit bir **C# konsol uygulaması** olan Kütüphane Yönetim Sistemi, kitap ve üye yönetimi ile ödünç alma işlemlerini kolayca takip etmenizi sağlar. JSON dosyaları ile veri kalıcılığı sağlar.

## Özellikler

- Kitap ekleme, listeleme, güncelleme ve silme  
- Üye ekleme ve listeleme  
- Kitap ödünç alma ve iade işlemleri  
- Geciken kitapların takibi  
- Verilerin JSON formatında kaydedilmesi

## Teknolojiler

- C# (.NET)  
- Newtonsoft.Json

## Kurulum

1. Projeyi klonlayın:  
   ```bash
   git clone https://github.com/kullaniciadi/kutuphane-yonetim-sistemi.git
2.Visual Studio veya herhangi bir C# IDE ile açın.

3.Projeyi derleyip çalıştırın.

4.Konsol ekranında menüden yapmak istediğiniz işlemi seçin.

## Kullanım


Program açıldıktan sonra aşağıdaki menü seçenekleri görünür:
1 - Kitap Ekle  
2 - Kitapları Listele  
3 - Kitap Güncelle  
4 - Kitap Sil  
5 - Üye Ekle  
6 - Üyeleri Listele  
7 - Ödünç Al  
8 - İade Et  
9 - Geciken Kitaplar  
0 - Çıkış  


## Özellikler


Kitap Ekle: ISBN, başlık, yazar ve sayfa sayısı bilgilerini girerek kitap ekleyebilirsiniz.

Kitapları Listele: Sistemde kayıtlı tüm kitapları görüntüler.

Kitap Güncelle: ISBN üzerinden kitap bilgilerini güncelleyebilirsiniz.

Kitap Sil: ISBN ile kitap silme işlemi yapılır.

Üye Ekle: Yeni üye kaydı oluşturur.

Üyeleri Listele: Tüm üyeleri listeler.

Ödünç Al: Üye seçip kitap ödünç alma işlemi yapabilirsiniz.

İade Et: Ödünç alınan kitabı iade edebilirsiniz.

Geciken Kitaplar: Teslim tarihi geçen ödünç kitapları listeler.

Çıkış: Verileri kaydederek programdan çıkış yapar.


## Proje Yapısı


- Program.cs — Ana program dosyası ve iş mantığı

- Model klasörü — Kitap.cs, Uye.cs, Odunc.cs gibi modeller içerir

- kitaplar.json, uyeler.json, oduncler.json — Veri dosyaları (program çalıştıkça güncellenir)


