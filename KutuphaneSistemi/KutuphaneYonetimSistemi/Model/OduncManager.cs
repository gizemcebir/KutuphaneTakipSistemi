using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using KutuphaneYonetimSistemi.Model;

namespace KutuphaneYonetimSistemi.Model
{
    internal class OduncManager
    {
        public static List<Odunc> oduncler = new List<Odunc>();
        public static string oduncDosya = "oduncler.json";


        public static void OduncAl(List<Uye> uyeler, List<Kitap> kitaplar)
        {
            Console.Write("Üye ID girin: ");
            if (!int.TryParse(Console.ReadLine(), out int uyeId))
            {
                Console.WriteLine("Geçersiz Üye ID.");
                return;
            }

            Console.Write("Kitap ID girin: ");
            if (!int.TryParse(Console.ReadLine(), out int kitapId))
            {
                Console.WriteLine("Geçersiz Kitap ID.");
                return;
            }

            var uye = uyeler.FirstOrDefault(u => u.UyeId == uyeId);
            if (uye == null)
            {
                Console.WriteLine("Üye bulunamadı.");
                return;
            }

            var kitap = kitaplar.FirstOrDefault(k => k.KitapId == kitapId);
            if (kitap == null)
            {
                Console.WriteLine("Kitap bulunamadı.");
                return;
            }

            bool kitapOduncte = oduncler.Any(o => o.KitapId == kitapId && o.IadeTarihi == null);
            if (kitapOduncte)
            {
                Console.WriteLine("Bu kitap şu anda ödünçte.");
                return;
            }

            Odunc yeniOdunc = new Odunc
            {
                OduncId = oduncler.Count > 0 ? oduncler.Max(o => o.OduncId) + 1 : 1,
                UyeId = uyeId,
                KitapId = kitapId,
                OduncTarihi = DateTime.Now,
                IadeTarihi = null
            };

            oduncler.Add(yeniOdunc);
            Console.WriteLine("Kitap başarıyla ödünç alındı.");
            OduncleriKaydet();
        }

        // JSON dosyasına kaydet
        public static void OduncleriKaydet()
        {
            try
            {
                string json = JsonConvert.SerializeObject(oduncler, Formatting.Indented);
                File.WriteAllText(oduncDosya, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Kayıt sırasında hata oluştu: " + ex.Message);
            }
        }

        // JSON dosyasından yükle
        public static void OduncleriYukle()
        {
            try
            {
                if (File.Exists(oduncDosya))
                {
                    string json = File.ReadAllText(oduncDosya);
                    oduncler = JsonConvert.DeserializeObject<List<Odunc>>(json) ?? new List<Odunc>();
                }
                else
                {
                    oduncler = new List<Odunc>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Yükleme sırasında hata oluştu: " + ex.Message);
                oduncler = new List<Odunc>();
            }
        }
        // Ödünç alma fonksiyonu
    }
}
