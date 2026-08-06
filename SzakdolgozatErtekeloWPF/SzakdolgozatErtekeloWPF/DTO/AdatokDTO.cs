using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SzakdolgozatErtekeloWPF.Enumok;

namespace SzakdolgozatErtekeloWPF.DTO
{
    public class AdatokDTO
    {
        public HallgatoDTO Hallgato { get; set; }
        public List<ErtekelesDTO> Ertekelesek { get; set; }
        public float OsszesitettErtekeles { get; set; }
        public string RovidSzovegesErtekeles { get; set; }
        public string JavasoltErdemjegy { get; set; }
        public string BitraloiJavaslat { get; set; }
        public List<string> Kerdesek { get; set; }
        public ErtekeloSzerepe ErtekeloSzerepe { get; set; }
        public Nyelv OldalNyelve { get; set; }
        public Laptipus Laptipusa { get; set; }
    }
}
