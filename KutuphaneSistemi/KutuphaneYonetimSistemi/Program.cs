using KutuphaneYonetimSistemi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Newtonsoft.Json;



namespace KutuphaneYonetimSistemi
{
    internal class Program
    {
        const string kitapDosya = "kitaplar.json";
        const string uyeDosya = "uyeler.json";
        const string oduncDosya = "oduncler.json"; // JSON dosya yolu


        static List<Kitap> kitaplar = new List<Kitap>();
        static List<Uye> uyeler = new List<Uye>();
        static List<Odunc> oduncler = new List<Odunc>();

        static void Main(string[] args)
        {
            KitaplariYukle();
            UyeleriYukle();
            OduncleriYukle();
        
            while (true)
            {
                Console.WriteLine("1 - Kitap Ekle");
                Console.WriteLine("2 - Kitapları Listele");
                Console.WriteLine("3 - Kitap Güncelle");
                Console.WriteLine("4 - Kitap Sil");
                Console.WriteLine("5 - Üye Ekle");
                Console.WriteLine("6 - Üyeleri Listele");
                Console.WriteLine("7 - Ödünç Al");
                Console.WriteLine("8 - İade Et");
                Console.WriteLine("9 - Geciken Kitaplar");
                Console.WriteLine("0 - Çıkış");
                Console.Write("Seçiminiz: ");

                string secim = Console.ReadLine();

                switch (secim)
                {
                    case "1": KitapEkle(); break;
                    case "2": KitaplariListele(); break;
                    case "3": KitapGuncelle(); break;
                    case "4": KitapSil(); break;
                    case "5": UyeEkle(); break;
                    case "6": UyeleriListele(); break;
                    case "7": OduncAl(); break;
                    case "8": IadeEt(); break;
                    case "9": GecikenleriListele(); break;
                    case "0":
                        KitaplariKaydet();
                        UyeleriKaydet();
                        OdunclerKaydet();  // <--- Bu satır eksik, bunu ekle
                        Console.WriteLine("Veriler kaydedildi. Çıkılıyor...");
                        return;
                    default:
                        Console.WriteLine("Geçersiz seçim!");
                        break;
                }
            }
            
        }

        static void OdunclerKaydet()
        {
            string json = JsonConvert.SerializeObject(oduncler, Formatting.Indented);
            File.WriteAllText(oduncDosya, json);
        }

        static void OduncleriYukle()
        {
            if (File.Exists(oduncDosya))
            {
                string json = File.ReadAllText(oduncDosya);
                oduncler = JsonConvert.DeserializeObject<List<Odunc>>(json) ?? new List<Odunc>();
            }
        }

        static void KitapEkle()
        {
            Console.Write("ISBN: ");
            string isbn = Console.ReadLine();
            Console.Write("Başlık: ");
            string baslik = Console.ReadLine();
            Console.Write("Yazar: ");
            string yazar = Console.ReadLine();
            Console.Write("Sayfa Sayısı: ");
            if (!int.TryParse(Console.ReadLine(), out int sayfa))
            {
                Console.WriteLine("Geçersiz sayfa sayısı.");
                return;
            }
            Kitap kitap = new Kitap
            {
                ISBN = isbn,
                Baslik = baslik,
                Yazar = yazar,
                SayfaSayisi = sayfa
            };


            kitaplar.Add(kitap);
            Console.WriteLine("Kitap eklendi.");
        }

        static void KitaplariListele()
        {
            Console.WriteLine("\n--- Kitaplar ---");
            foreach (var kitap in kitaplar)
            {
                Console.WriteLine($"{kitap.ISBN} - {kitap.Baslik} ({kitap.Yazar})");
            }
        }

        static void KitapGuncelle()
        {
            Console.Write("Güncellenecek kitap ISBN: ");
            string isbn = Console.ReadLine();

            var kitap = kitaplar.FirstOrDefault(k => k.ISBN == isbn);
            if (kitap == null)
            {
                Console.WriteLine("Kitap bulunamadı.");
                return;
            }

            Console.Write("Yeni Başlık (boş bırak = değişme): ");
            string yeniBaslik = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(yeniBaslik)) kitap.Baslik = yeniBaslik;

            Console.Write("Yeni Yazar (boş bırak = değişme): ");
            string yeniYazar = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(yeniYazar)) kitap.Yazar = yeniYazar;

            Console.Write("Yeni Sayfa Sayısı (boş bırak = değişme): ");
            string yeniSayfa = Console.ReadLine();
            if (int.TryParse(yeniSayfa, out int sayfa)) kitap.SayfaSayisi = sayfa;

            Console.WriteLine("Kitap güncellendi.");
        }

        static void KitapSil()
        {
            Console.Write("Silinecek kitap ISBN: ");
            string isbn = Console.ReadLine();

            var kitap = kitaplar.FirstOrDefault(k => k.ISBN == isbn);

            if (kitap != null)
            {
                kitaplar.Remove(kitap);
                Console.WriteLine("Kitap silindi.");
            }
            else
            {
                Console.WriteLine("Kitap bulunamadı.");
            }
        }

        static void UyeEkle()
        {
            Console.Write("Ad: ");
            string ad = Console.ReadLine();
            Console.Write("Soyad: ");
            string soyad = Console.ReadLine();

            Uye uye = new Uye { Ad = ad, Soyad = soyad };
            uyeler.Add(uye);
            Console.WriteLine("Üye eklendi.");
        }

        static void UyeleriListele()
        {
            Console.WriteLine("\n--- Üyeler ---");
            int i = 1;
            foreach (var uye in uyeler)
            {
                Console.WriteLine($"{i++} - {uye.Ad} {uye.Soyad}");
            }
        }

        static void OduncAl()
        {
            UyeleriListele();
            Console.Write("Ödünç alacak üye numarası: ");
            if (!int.TryParse(Console.ReadLine(), out int uyeIndex) || uyeIndex < 1 || uyeIndex > uyeler.Count)
            {
                Console.WriteLine("Geçersiz üye numarası.");
                return;
            }

            KitaplariListele();
            Console.Write("Ödünç alınacak kitap ISBN: ");
            string isbn = Console.ReadLine();
            var kitap = kitaplar.FirstOrDefault(k => k.ISBN == isbn);
            if (kitap == null)
            {
                Console.WriteLine("Kitap bulunamadı.");
                return;
            }

            var uye = uyeler[uyeIndex - 1];
            if (oduncler.Any(o => o.KitapISBN == isbn && o.IadeTarihi == null))
            {
                Console.WriteLine("Kitap şu anda ödünçte.");
                return;
            }

            oduncler.Add(new Odunc
            {
                OduncId = oduncler.Count > 0 ? oduncler.Max(o => o.OduncId) + 1 : 1,
                UyeId = uye.UyeId,
                KitapISBN = isbn,
                OduncTarihi = DateTime.Now,
                IadeTarihi = null
            });
            Console.WriteLine("Kitap ödünç alındı.");
        }


        static void IadeEt()
        {
            Console.Write("Kitap ISBN girin: ");
            string isbn = Console.ReadLine();
            var odunc = oduncler.FirstOrDefault(o => o.KitapISBN == isbn && o.IadeTarihi == null);
            if (odunc != null)
            {
                odunc.IadeTarihi = DateTime.Now;
                Console.WriteLine("Kitap iade edildi.");
            }
            else Console.WriteLine("Aktif ödünç kaydı bulunamadı.");
        }

        static void GecikenleriListele()
        {
            Console.WriteLine("\n--- Geciken Kitaplar ---");
            foreach (var odunc in oduncler)
            {
                if (odunc.IadeTarihi == null && odunc.GeciktiMi())
                {
                    var uye = uyeler.FirstOrDefault(u => u.UyeId == odunc.UyeId);
                    var kitap = kitaplar.FirstOrDefault(k => k.ISBN == odunc.KitapISBN);
                    Console.WriteLine($"{uye?.Ad} {uye?.Soyad} - {kitap?.Baslik} ({odunc.OduncTarihi.ToShortDateString()})");
                }
            }
        }

        static void KitaplariKaydet()
        {
            string json = JsonConvert.SerializeObject(kitaplar, Formatting.Indented);
            File.WriteAllText(kitapDosya, json);
        }

        static void UyeleriKaydet()
        {
            string json = JsonConvert.SerializeObject(uyeler, Formatting.Indented);
            File.WriteAllText(uyeDosya, json);
        }

        static void KitaplariYukle()
        {
            if (File.Exists(kitapDosya))
            {
                string json = File.ReadAllText(kitapDosya);
                kitaplar = JsonConvert.DeserializeObject<List<Kitap>>(json);
            }
            else
            {
                kitaplar = new List<Kitap>();
            }
        }

        static void UyeleriYukle()
        {
            if (File.Exists(uyeDosya))
            {
                string json = File.ReadAllText(uyeDosya);
                uyeler = JsonConvert.DeserializeObject<List<Uye>>(json);
            }
            else
            {
                uyeler = new List<Uye>();
            }
        }

    }
}

