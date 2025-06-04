using KutuphaneYonetimSistemi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneYonetimSistemi.Model
{
    internal class Uye : IKullanıcı, IOduncAlabilir
    {
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public int UyeId { get; set; }

        private List<Odunc> aktifOduncler = new List<Odunc>();

        public void BilgileriGoster()
        {
            Console.WriteLine($"Ad: {Ad} {Soyad}, Ödünç Kitap Sayısı: {aktifOduncler.Count}");
        }

        public void OduncAl(Kitap kitap)
        {
            aktifOduncler.Add(new Odunc
            {
                KitapId = kitap.KitapId,
                UyeId = this.UyeId,
                OduncTarihi = DateTime.Now,
                IadeTarihi = null
            });

            Console.WriteLine($"{kitap.Baslik} ödünç alındı.");
        }

        public void IadeEt(Kitap kitap)
        {
            var odunc = aktifOduncler.FirstOrDefault(o => o.KitapId == kitap.KitapId && o.IadeTarihi == null);
            if (odunc != null)
            {
                odunc.IadeTarihi = DateTime.Now;
                Console.WriteLine($"{kitap.Baslik} iade edildi.");
                // İade edilince listeden kaldırmak isteğe bağlı
                aktifOduncler.Remove(odunc);
            }
            else
            {
                Console.WriteLine($"{kitap.Baslik} için aktif ödünç kaydı bulunamadı.");
            }
        }

        public List<Odunc> GetOduncler()
        {
            return aktifOduncler;
        }
    }
}
