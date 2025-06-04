using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace KutuphaneYonetimSistemi.Model
{
    internal class Odunc
    {
        public int OduncId { get; set; }
        public int UyeId { get; set; }
        public int KitapId { get; set; }
        public string KitapISBN { get; set; }
        public DateTime OduncTarihi { get; set; }
        public DateTime? IadeTarihi { get; set; }
        public bool GeciktiMi()
        {
            return IadeTarihi == null && (DateTime.Now - OduncTarihi).TotalDays > 15;
        }


        static List<Odunc> oduncler = new List<Odunc>();
        static string oduncDosya = "oduncler.json";


        public static void OduncAl(List<Uye> uyeler, List<Kitap> kitaplar)
        {
            Console.Write("Üye ID girin: ");
            int uyeId = int.Parse(Console.ReadLine());

            Console.Write("Kitap ID girin: ");
            int kitapId = int.Parse(Console.ReadLine());

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
                Console.WriteLine("Bu kitap zaten ödünçte.");
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
            OduncleriKaydet();
            Console.WriteLine("Kitap başarıyla ödünç alındı.");
        }
        // Ödünç kayıtlarını JSON'a kaydetme fonksiyonu
        static void OduncleriKaydet()
        {
            string json = JsonConvert.SerializeObject(oduncler, Formatting.Indented);
            File.WriteAllText(oduncDosya, json);
        }

        // Ödünç kayıtlarını JSON'dan yükleme fonksiyonu
        static void OduncleriYukle()
        {
            if (File.Exists(oduncDosya))
            {
                string json = File.ReadAllText(oduncDosya);
                oduncler = JsonConvert.DeserializeObject<List<Odunc>>(json);
            }
            else
            {
                oduncler = new List<Odunc>();
            }
        }
    }
}
