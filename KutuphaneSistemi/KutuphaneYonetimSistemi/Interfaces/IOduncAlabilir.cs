using KutuphaneYonetimSistemi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneYonetimSistemi.Interfaces
{
    internal interface IOduncAlabilir
    {
        void OduncAl(Kitap kitap);
        void IadeEt(Kitap kitap);
    }
}
