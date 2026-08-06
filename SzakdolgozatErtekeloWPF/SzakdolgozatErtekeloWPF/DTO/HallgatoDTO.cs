using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SzakdolgozatErtekeloWPF.Enumok;

namespace SzakdolgozatErtekeloWPF.DTO
{
    public class HallgatoDTO
    {
        public string Nev { get; set; }
        public string Netpun { get; set; }
        public Szakok Szak { get; set; }
        public string SzakdolgozatCime { get; set; }
    }
}
