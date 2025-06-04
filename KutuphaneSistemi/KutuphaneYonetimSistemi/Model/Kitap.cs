using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneYonetimSistemi.Model
{
    public class Kitap
    {
        public string ISBN { get; set; }
        public int KitapId { get; set; }

        public string Baslik { get; set; }
        public string Yazar { get; set; }
        public int SayfaSayisi { get; set; }
    }
}
