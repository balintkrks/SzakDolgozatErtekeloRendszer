namespace SzakdolgozatErtekeloApi.DTO
{
    public class AdatokDTO
    {
        public HallgatoDTO Hallgato {get; set;}
        public List<ErtekelesDTO> Ertekelesek { get; set; }
        public int OsszesitettErtekeles { get; set; }
        public string RovidSzovegesErtekeles { get; set; }
        public string JavasoltErdemjegy { get; set; }
        public string BitraloiJavaslat { get; set; }
        public List<string> Kerdesek { get; set; }
    }
}
