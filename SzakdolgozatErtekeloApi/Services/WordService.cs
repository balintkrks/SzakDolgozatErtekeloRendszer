using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using SzakdolgozatErtekeloApi.DTO;
using SzakdolgozatErtekeloApi.Templates;

namespace SzakdolgozatErtekeloApi.Services
{
    public class WordService
    {
        public void Generate(string outputPath, AdatokDTO adatok)
        {
            var templatePath = Path.Combine(
                AppContext.BaseDirectory,
                "Templates",
                "BiralatiLapTemplate.docx");


            File.Copy(
                templatePath,
                outputPath,
                true);


            using var document = WordprocessingDocument.Open(outputPath, true);


            ReplaceContentControl(document, "HallgatoNev", adatok.Hallgato.Nev);
            ReplaceContentControl(document, "Neptun", adatok.Hallgato.Netpun);
            ReplaceContentControl(document, "Szak", adatok.Hallgato.Szak.ToString().Replace("_", " "));
            ReplaceContentControl(document, "Cim", adatok.Hallgato.SzakdolgozatCime);

            FillEvaluationTables(document, adatok);

            ReplaceContentControl(document, "OsszesitettErtekeles", adatok.OsszesitettErtekeles.ToString());
            ReplaceContentControl(document, "RovidSzovegesErtekeles", adatok.RovidSzovegesErtekeles ?? "");
            ReplaceContentControl(document, "JavasoltErdemjegy", adatok.JavasoltErdemjegy ?? "");
            ReplaceContentControl(document, "BiroiJavaslat", adatok.BitraloiJavaslat ?? "");
            ReplaceContentControl(document, "Kerdesek", string.Join("\r\n", adatok.Kerdesek.Select((x, i) => $"{i + 1}. {x}")));

            var parts = adatok.JavasoltErdemjegy.Split('(');

            string erdemjegySzoveg = parts[0].Trim();
            string erdemjegySzam = parts[1].Replace(")", "").Trim();

            ReplaceContentControl(document, "JavasoltErdemjegySzoveg", erdemjegySzoveg);
            ReplaceContentControl(document, "JavasoltErdemjegySzam", erdemjegySzam);


            ReplaceContentControl(document, "Datum", $"Eger, {DateTime.Now:yyyy.MM.dd.}");

            ReplaceContentControl(document, "ErtekeloSzerepe", adatok.ErtekeloSzerepe.ToString());

            document.MainDocumentPart.Document.Save();
        }

        private void ReplaceContentControl(WordprocessingDocument document, string tag, string value)
        {
            foreach (var control in document.MainDocumentPart!.Document.Descendants<SdtElement>())
            {
                var tagElement = control.SdtProperties?.GetFirstChild<Tag>();

                if (tagElement == null)
                    continue;

                if (tagElement.Val != tag)
                    continue;

                var text = control.Descendants<Text>().FirstOrDefault();

                if (text != null)
                {
                    text.Text = value;
                }

                break;
            }
        }

        private void FillEvaluationTables(WordprocessingDocument document, AdatokDTO adatok)
        {
            var criteria = EvaulationTemplate.GetDefaultCriteria();

            for (int i = 0; i < adatok.Ertekelesek.Count; i++)
            {
                var ertekeles = adatok.Ertekelesek[i];
                var criterion = criteria.First(x => x.ID == ertekeles.ID);

                ReplaceContentControl(document, $"C{i + 1}Title", criterion.Title);
                ReplaceContentControl(document, $"C{i + 1}Zero", criterion.ZeroPointText);
                ReplaceContentControl(document, $"C{i + 1}OneTwo", criterion.OneTwoPointText);
                ReplaceContentControl(document, $"C{i + 1}ThreeFour", criterion.ThreeFourPointText);
                ReplaceContentControl(document, $"C{i + 1}Five", criterion.FivePointText);

                ReplaceContentControl(document, $"C{i + 1}Jegy", ertekeles.Jegy.ToString());
                ReplaceContentControl(document, $"C{i + 1}Megjegyzes", ertekeles.Megjegyzes ?? "");
            }
        }

    }


}
