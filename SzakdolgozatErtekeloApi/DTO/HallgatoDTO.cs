using SzakdolgozatErtekeloApi.Enumok;

namespace SzakdolgozatErtekeloApi.DTO
{
    public class HallgatoDTO
    {
        public string Nev { get; set; }
        public string Netpun { get; set; }
        public Szakok Szak { get; set; }
        public string SzakdolgozatCime { get; set; }

    }
}
