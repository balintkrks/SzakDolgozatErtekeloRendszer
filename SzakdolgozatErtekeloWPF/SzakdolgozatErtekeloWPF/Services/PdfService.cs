using SzakdolgozatErtekeloWPF.DTO;
using SzakdolgozatErtekeloWPF.Enumok;
using SzakdolgozatErtekeloWPF.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Elements;
using SzakdolgozatErtekeloWPF.Templates;


namespace SzakdolgozatErtekeloWPF.Services
{
    public class PdfService
    {
        public void Generate(string filePath, AdatokDTO adatok)
        {
            Document.Create(document =>
            {
                document.Page(page =>
                {
                    page.Margin(40);

                    page.DefaultTextStyle(x => x.FontFamily("Times New Roman"));

                    page.Content().Column(column =>
                    {
                        CreateHeader(column, adatok);
                        CreateStudentInfo(column, adatok);

                        List<EvaluationCriterion> templates;

                        if (adatok.OldalNyelve == Nyelv.En)
                        {
                            templates = EvaluationTemplate.GetEnglishCriteria();
                        }
                        else if (adatok.Laptipusa == Laptipus.Tudomanyos)
                        {
                            templates = EvaluationTemplate.GetScientificCriteria();
                        }
                        else
                        {
                            templates = EvaluationTemplate.GetDefaultCriteria();
                        }

                        foreach (ErtekelesDTO ertekeles in adatok.Ertekelesek)
                        {
                            EvaluationCriterion template = templates.FirstOrDefault(x => x.ID == ertekeles.ID);

                            CreateEvaluationTable(column, template, ertekeles, adatok);
                        }

                        CreateSummary(column, adatok);
                        CreateGradeSection(column, adatok);
                        CreateFinalSection(column, adatok);
                    });
                });
            }).GeneratePdf(filePath);
        }

        private void CreateHeader(ColumnDescriptor column, AdatokDTO adatok)
        {
            if (adatok.OldalNyelve == Nyelv.En)
            {
                column.Item()
                    .AlignCenter()
                    .Text("ESZTERHÁZY KÁROLY CATHOLIC UNIVERSITY")
                    .Bold()
                    .FontSize(14);

                column.Item()
                    .PaddingTop(10)
                    .AlignCenter()
                    .Text("THESIS WORK REVIEW SHEET")
                    .Bold()
                    .FontSize(18);
            }
            else
            {
                column.Item()
                    .AlignCenter()
                    .Text("ESZTERHÁZY KÁROLY KATOLIKUS EGYETEM")
                    .Bold()
                    .FontSize(14);

                column.Item()
                    .PaddingTop(10)
                    .AlignCenter()
                    .Text("SZAKDOLGOZAT BÍRÁLATI LAP")
                    .Bold()
                    .FontSize(18);
            }

            column.Item().PaddingTop(30);
        }

        private void CreateStudentInfo(ColumnDescriptor column, AdatokDTO adatok)
        {
            bool en = adatok.OldalNyelve == Nyelv.En;

            column.Item().Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn();
                    columns.RelativeColumn(2);
                });

                AddStudentRow(table,
                    en ? "Name of the student:" : "A hallgató neve:",
                    adatok.Hallgato.Nev);

                AddStudentRow(table,
                    en ? "The student's Neptun code:" : "A hallgató Neptun kódja:",
                    adatok.Hallgato.Netpun);

                AddStudentRow(table,
                    en ? "The student's major:" : "A hallgató szakja:",
                    adatok.Hallgato.Szak.ToString().Replace("_", " "));

                AddStudentRow(table,
                    en ? "Title of the thesis:" : "A szakdolgozat címe:",
                    adatok.Hallgato.SzakdolgozatCime);
            });

            column.Item().PaddingTop(20);
        }

        private void AddStudentRow(TableDescriptor table, string label, string value)
        {
            table.Cell()
                .PaddingVertical(6)
                .Text(label)
                .Bold();

            table.Cell()
                .PaddingVertical(6)
                .Text(value);
        }

        private void CreateEvaluationTable(ColumnDescriptor column, EvaluationCriterion criterion, ErtekelesDTO ertekeles, AdatokDTO adatok)
        {
            bool en = adatok.OldalNyelve == Nyelv.En;

            column.Item().PaddingTop(15);

            column.Item().Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn(4);
                    columns.RelativeColumn(2.5f);
                    columns.RelativeColumn(2.5f);
                    columns.RelativeColumn(2.5f);
                    columns.RelativeColumn(2.5f);
                    columns.ConstantColumn(45);
                });

                table.Cell()
                    .RowSpan(2)
                    .Border(1)
                    .Padding(5)
                    .AlignCenter()
                    .AlignMiddle()
                    .Text(en ? "Evaluation\ncriteria" : "Értékelési\nszempont")
                    .Bold();

                table.Cell()
                    .ColumnSpan(4)
                    .Border(1)
                    .Padding(5)
                    .AlignCenter()
                    .Text(en ? "Awardable score" : "Adható pontszám")
                    .Bold();

                table.Cell()
                    .RowSpan(2)
                    .Border(1)
                    .Padding(5)
                    .AlignCenter()
                    .AlignMiddle()
                    .Text(en ? "Score\nachieved" : "Elért\npontszám")
                    .Bold();

                table.Cell().Border(1).Padding(5).AlignCenter().Text("0").Bold();
                table.Cell().Border(1).Padding(5).AlignCenter().Text("1-2").Bold();
                table.Cell().Border(1).Padding(5).AlignCenter().Text("3-4").Bold();
                table.Cell().Border(1).Padding(5).AlignCenter().Text("5").Bold();



                table.Cell().Border(1).Padding(5).Text(criterion.Title).FontSize(9);
                table.Cell().Border(1).Padding(5).Text(criterion.ZeroPointText).FontSize(9); ;
                table.Cell().Border(1).Padding(5).Text(criterion.OneTwoPointText).FontSize(9); ;
                table.Cell().Border(1).Padding(5).Text(criterion.ThreeFourPointText).FontSize(9); ;
                table.Cell().Border(1).Padding(5).Text(criterion.FivePointText).FontSize(9); ;

                table.Cell()
                    .Border(1)
                    .Padding(5)
                    .AlignCenter()
                    .AlignMiddle()
                    .Text(ertekeles.Jegy.ToString())
                    .Bold();

            });

            if (en)
            {
                column.Item().PaddingTop(10);
            }
            else
            {
                column.Item().PaddingTop(adatok.Laptipusa == Laptipus.Tudomanyos ? 25 : 15);
            }



            column.Item().Text(text =>
            {
                text.Span(en ? "Short justification (optional): " : "Rövid indoklás (opcionális): ").FontSize(9);
                text.Span(ertekeles.Megjegyzes).FontSize(9);
            });

            column.Item().PaddingBottom(adatok.Laptipusa == Laptipus.Tudomanyos ? 25 : 20);
        }

        private void CreateSummary(ColumnDescriptor column, AdatokDTO adatok)
        {
            bool en = adatok.OldalNyelve == Nyelv.En;

            column.Item().PaddingTop(20);

            column.Item().Text(
                en ?
                "If one of the evaluation criteria is zero, then all scores are zero. Half points can also be given for evaluation." :
                "Amennyiben az értékelési szempontok közül valamelyik nulla pont, akkor az összes pontszám is nulla. Értékelésnél fél pontok is adhatók.")
                .FontSize(11);

            column.Item().PaddingTop(10);


            column.Item()
                .AlignCenter()
                .Element(container =>
                {
                    container
                       .Width(250)
                       .Table(table =>
                       {
                           table.ColumnsDefinition(columns =>
                           {
                               columns.RelativeColumn(2);
                               columns.ConstantColumn(70);
                           });

                           table.Cell()
                                .Border(1)
                                .Padding(5)
                                .Text(en ? "Total score:" : "Összes pontszám:")
                                .Bold();

                           table.Cell()
                                .Border(1)
                                .Padding(5)
                                .AlignCenter()
                                .AlignMiddle()
                                .Text(adatok.OsszesitettErtekeles.ToString())
                                .Bold()
                                .FontSize(11);
                       });
                });

            column.Item().PaddingTop(20);

            column.Item()
                .Text(en ? "Short text evaluation of the thesis (optional):" : "A dolgozat rövid szöveges értékelése (opcionális):")
                .Bold()
                .FontSize(12);

            column.Item()
                .MinHeight(80)
                .Padding(8)
                .Text(adatok.RovidSzovegesErtekeles);
        }

        private void CreateGradeSection(ColumnDescriptor column, AdatokDTO adatok)
        {
            bool en = adatok.OldalNyelve == Nyelv.En;

            column.Item().PaddingTop(20);


            column.Item().PaddingTop(5);

            column.Item().Text(text =>
            {
                text.Span(en ? "Determination of recommended grade (required): " : "Javasolt érdemjegy megállapítása (kötelező): ")
                    .Bold();

                text.Span(adatok.JavasoltErdemjegy);
            });

            column.Item().PaddingTop(10);

            column.Item().Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(120);
                    columns.ConstantColumn(100);
                });

                void AddRow(string grade, string points)
                {
                    table.Cell()
                        .PaddingVertical(2)
                        .Text(grade).FontSize(8).Bold();

                    table.Cell()
                        .PaddingVertical(2)
                        .Text(points).FontSize(8);
                }

                AddRow(en ? "Insufficient:" : "Elégtelen:", en ? "8–25 points" : "8–25 pont");
                AddRow(en ? "Sufficient:" : "Elégséges:", en ? "26–32 points" : "26–32 pont");
                AddRow(en ? "Medium:" : "Közepes:", en ? "33–38 points" : "33–38 pont");
                AddRow(en ? "Good:" : "Jó:", en ? "39–44 points" : "39–44 pont");
                AddRow(en ? "Marked" : "Jeles:", en ? "45–50 points" : "45–50 pont");
            });

            column.Item().PaddingTop(20);

            column.Item()
                .Text(en ? "Reviewer's suggestions for defense (optional):" : "A bíráló javaslatai a védéshez (opcionális):")
                .Bold();

            column.Item()
                .MinHeight(80)
                .Padding(8)
                .Text(adatok.BitraloiJavaslat);
        }

        private void CreateFinalSection(ColumnDescriptor column, AdatokDTO adatok)
        {
            bool en = adatok.OldalNyelve == Nyelv.En;

            column.Item().PaddingTop(20);

            column.Item()
                .Text(en ? "Questions to be answered by the student (mandatory):" : "A hallgató által megválaszolandó kérdések (kötelező):")
                .Bold();

            column.Item().PaddingTop(5);

            for (int i = 0; i < adatok.Kerdesek.Count; i++)
            {
                column.Item().Text($"{i + 1}. {adatok.Kerdesek[i]}");
            }

            column.Item().PaddingTop(20);

            column.Item().Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn(2);
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                });

                string[] parts = adatok.JavasoltErdemjegy.Split('(');

                string erdemjegySzoveg = parts[0].Trim();
                string erdemjegySzam = parts[1].Replace(")", "").Trim();



                table.Cell().Text(en ? "of the thesis: " : "A szakdolgozat értékelése:").Bold();
                table.Cell().Text(en ? $"by letter: {erdemjegySzoveg}" : $"betűvel: {erdemjegySzoveg}");
                table.Cell().Text(en ? $"by number: {erdemjegySzam}" : $"számmal: {erdemjegySzam}");
            });

            column.Item().PaddingTop(90);

            column.Item().Row(row =>
            {
                row.RelativeItem()
                    .Text($"Eger, {DateTime.Now:yyyy.MM.dd.}");

                row.RelativeItem()
                    .AlignRight()
                    .Column(col =>
                    {
                        col.Item().AlignCenter().Text(en ? $"Signature of {adatok.ErtekeloSzerepe}" : $"{adatok.ErtekeloSzerepe} aláírása");
                    });
            });
        }
    }
}
