using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzakdolgozatErtekeloWPF.Models
{
    public class LocalizationModel
    {
        public string PageTitle { get; set; }

        public string HeaderCim { get; set; }

        public string SzerepkorKonzulens { get; set; }

        public string SzerepkorOpponens { get; set; }

        public string HallgatoAdatai { get; set; }

        public string HallgatoNeve { get; set; }

        public string HallgatoNeptun { get; set; }

        public string HallgatoSzak { get; set; }

        public string HallgatoCim { get; set; }

        public string SzakProgramtervezo { get; set; }

        public string SzakGazdasagi { get; set; }

        public string ErtekelesekCim { get; set; }

        public string SzovegesErtekelesCim { get; set; }

        public string SzovegesErtekelesPlaceholder { get; set; }

        public string BiraloiJavaslatkCim { get; set; }

        public string BiraloiJavaslatkPlaceholder { get; set; }

        public string KerdesekCim { get; set; }

        public string UjKerdesGomb { get; set; }

        public string Osszpontszam { get; set; }

        public string JavasoltErdemjegy { get; set; }

        public string MegjegyzesPlaceholder { get; set; }

        public string AdottPont { get; set; }

        public string ElegtelenLabel { get; set; }

        public string ElegsegessLabel { get; set; }

        public string KozepesLabel { get; set; }

        public string JoLabel { get; set; }

        public string JelesLabel { get; set; }

        public List<EvaluationCriterion> Criteria { get; set; }
    }
}
