using SzakdolgozatErtekeloApi.Enumok;

namespace SzakdolgozatErtekeloApi.DTO
{
    public class HallgatoDTO
    {
        public string Nev { get; set; }
        public string NetpunKod { get; set; }
        public Szakok szak { get; set; }
        public string SzakdolgozatCime { get; set; }

    }
}
