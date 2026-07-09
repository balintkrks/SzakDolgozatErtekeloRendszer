using SzakdolgozatErtekeloApi.Enumok;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Elements;
using SzakdolgozatErtekeloApi.DTO;
using SzakdogaBiralatTeszt;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace SzakdolgozatErtekeloApi.Services
{
    public class PdfService
    {
        public void Generate(string filePath,AdatokDTO adatok)
        {
            Document.Create(document =>
            {
                document.Page(page =>
                {
                    page.Margin(40);

                    page.DefaultTextStyle(x => x.FontFamily("Times New Roman"));

                    page.Content().Column(column =>
                    {
                        CreateHeader(column);
                        CreateStudentInfo(column, adatok);

                        var templates = EvaulationTemplate.GetDefaultCriteria();

                        foreach (var ertekeles in adatok.Ertekelesek)
                        {
                            var template = templates.FirstOrDefault(x => x.ID == ertekeles.ID);

                            CreateEvaluationTable(column, template, ertekeles);
                        }

                        CreateSummary(column, adatok);
                        CreateGradeSection(column, adatok);
                        CreateFinalSection(column, adatok);
                    });
                });
            }).GeneratePdf(filePath);
        }

        private void CreateHeader(ColumnDescriptor column)
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

            column.Item().PaddingTop(30);
        }

        private void CreateStudentInfo(ColumnDescriptor column, AdatokDTO adatok)
        {
            column.Item().Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn();
                    columns.RelativeColumn(2);
                });

                AddStudentRow(table, "A hallgató neve:", adatok.Hallgato.Nev);
                AddStudentRow(table, "A hallgató Neptun kódja:", adatok.Hallgato.Netpun);
                AddStudentRow(table, "A hallgató szakja:", adatok.Hallgato.Szak.ToString());
                AddStudentRow(table, "A szakdolgozat címe:", adatok.Hallgato.SzakdolgozatCime);
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

        private void CreateEvaluationTable(ColumnDescriptor column, EvaluationCriterion criterion,ErtekelesDTO ertekeles)
        {
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
                    .Text("Értékelési\nszempont")
                    .Bold();

                table.Cell()
                    .ColumnSpan(4)
                    .Border(1)
                    .Padding(5)
                    .AlignCenter()
                    .Text("Adható pontszám")
                    .Bold();

                table.Cell()
                    .RowSpan(2)
                    .Border(1)
                    .Padding(5)
                    .AlignCenter()
                    .AlignMiddle()
                    .Text("Elért\npontszám")
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

            column.Item().PaddingTop(10);

            column.Item().Text(text =>
            {
                text.Span("Rövid indoklás (opcionális): ").FontSize(9);
                text.Span(ertekeles.Megjegyzes).FontSize(9);
            });

            column.Item().PaddingBottom(30);
        }

        private void CreateSummary(ColumnDescriptor column, AdatokDTO adatok)
        {
            column.Item().PaddingTop(20);

            column.Item().Text(
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
                                .Text("Összes pontszám")
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
                .Text("A dolgozat rövid szöveges értékelése (opcionális):")
                .Bold()
                .FontSize(12);

            column.Item()
                .MinHeight(80)
                .Padding(8)
                .Text(adatok.RovidSzovegesErtekeles);
        }

        private void CreateGradeSection(ColumnDescriptor column, AdatokDTO adatok)
        {
            column.Item().PaddingTop(20);

            column.Item().Text("Javasolt érdemjegy megállapítása (kötelező): ").Bold();

            column.Item().PaddingTop(5);

            column.Item().Text("A szakdolgozat javasolt érdemjegye az összesített pontszám alapján:");

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

                AddRow("Elégtelen:", "8–25 pont");
                AddRow("Elégséges:", "26–32 pont");
                AddRow("Közepes:", "33–38 pont");
                AddRow("Jó:", "39–44 pont");
                AddRow("Jeles:", "45–50 pont");
            });

            column.Item().PaddingTop(20);

            column.Item()
                .Text("A bíráló javaslatai a védéshez (opcionális):")
                .Bold();

            column.Item()
                .MinHeight(80)
                .Padding(8)
                .Text(adatok.BitraloiJavaslat);
        }

        private void CreateFinalSection(ColumnDescriptor column, AdatokDTO adatok)
        {
            column.Item().PaddingTop(20);

            column.Item()
                .Text("A hallgató által megválaszolandó kérdések (kötelező):")
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
                    
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                });

                table.Cell().Text("A szakdolgozat értékelése:").Bold();
                table.Cell().Text(adatok.JavasoltErdemjegy);
            });

            column.Item().PaddingTop(40);

            column.Item().Row(row =>
            {
                row.RelativeItem()
                    .Text($"Eger, {DateTime.Now:yyyy.MM.dd.}");

                row.RelativeItem()
                    .AlignRight()
                    .Column(col =>
                    {
                        col.Item().AlignCenter().Text($"{adatok.ErtekeloSzerepe} aláírása");
                    });
            });
        }
    }
}
