using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SzakdolgozatErtekeloApi.DTO;
using SzakdolgozatErtekeloApi.Templates;

namespace SzakdogaBiralatTeszt
{
    public class LatexService
    {
        public void Generate(string outputPath, AdatokDTO adatok)
        {
            var templatePath = Path.Combine(AppContext.BaseDirectory,"Templates","BiralatiLapTemplate.tex");

            var latex = File.ReadAllText(templatePath);

            latex = Replace(latex, "HallgatoNev", adatok.Hallgato.Nev);
            latex = Replace(latex, "Neptun", adatok.Hallgato.Netpun);
            latex = Replace(latex, "Szak", adatok.Hallgato.Szak.ToString());
            latex = Replace(latex, "Cim", adatok.Hallgato.SzakdolgozatCime);

            FillEvaluationTables(ref latex, adatok);

            latex = Replace(latex, "OsszesitettErtekeles", adatok.OsszesitettErtekeles.ToString());
            latex = Replace(latex, "RovidSzovegesErtekeles", EscapeLatex(adatok.RovidSzovegesErtekeles));
            latex = Replace(latex, "JavasoltErdemjegy", adatok.JavasoltErdemjegy);
            latex = Replace(latex, "BiroiJavaslat", EscapeLatex(adatok.BitraloiJavaslat));
            latex = Replace(latex, "Kerdesek", FormatQuestions(adatok.Kerdesek));
            latex = Replace(latex, "ErtekeloSzerepe", adatok.ErtekeloSzerepe.ToString());

            File.WriteAllText(outputPath, latex);
        }

        private string Replace(string text, string key, string value)
        {
            return text.Replace(
                $"{{{{{key}}}}}",
                EscapeLatex(value ?? ""));
        }

        private void FillEvaluationTables(ref string latex, AdatokDTO adatok)
        {
            var criteria = EvaulationTemplate.GetDefaultCriteria();

            foreach (var ertekeles in adatok.Ertekelesek)
            {
                var criterion = criteria.FirstOrDefault(x => x.ID == ertekeles.ID);

                if (criterion == null)
                    continue;

                string prefix = $"C{ertekeles.ID}";

                latex = Replace(latex, $"{prefix}Title", criterion.Title);
                latex = Replace(latex, $"{prefix}Zero", criterion.ZeroPointText);
                latex = Replace(latex, $"{prefix}OneTwo", criterion.OneTwoPointText);
                latex = Replace(latex, $"{prefix}ThreeFour", criterion.ThreeFourPointText);
                latex = Replace(latex, $"{prefix}Five", criterion.FivePointText);

                latex = Replace(latex, $"{prefix}Jegy", ertekeles.Jegy.ToString());
                latex = Replace(latex, $"{prefix}Megjegyzes", ertekeles.Megjegyzes ?? "");
            }
        }

        private string FormatQuestions(List<string>? kerdesek)
        {
            if (kerdesek == null || kerdesek.Count == 0)
                return string.Empty;

            return string.Join(
                Environment.NewLine + Environment.NewLine,
                kerdesek.Select((k, i) => $"{i + 1}. {EscapeLatex(k)}"));
        }

        private string EscapeLatex(string? text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            return text
                .Replace(@"\", @"\textbackslash{}")
                .Replace("&", @"\&")
                .Replace("%", @"\%")
                .Replace("$", @"\$")
                .Replace("#", @"\#")
                .Replace("_", @"\_")
                .Replace("{", @"\{")
                .Replace("}", @"\}")
                .Replace("~", @"\textasciitilde{}")
                .Replace("^", @"\textasciicircum{}");
        }
    }
}
