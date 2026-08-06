using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using Microsoft.Win32;
using SzakdolgozatErtekeloWPF.DTO;
using SzakdolgozatErtekeloWPF.Enumok;
using SzakdolgozatErtekeloWPF.Models;
using SzakdolgozatErtekeloWPF.Services;

namespace SzakdolgozatErtekeloWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly LocalizationService localizationService = new();
        private readonly PdfService pdfService = new();
        private readonly WordService wordService = new();
        private readonly LatexService latexService = new();

        private bool english = false;
        private bool scientific = false;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Nyelvváltó és módváltó események
            SwitchPanel.LanguageChanged += SetLanguage;
            SwitchPanel.ScientificChanged += SetScientific;

            // Score frissítés minden slider mozgáskor
            Evaluation.ScoresChanged += UpdateFooterScore;

            // Letöltő gomb események
            Footer.DownloadPdfClicked += DownloadPdf;
            Footer.DownloadWordClicked += DownloadWord;
            Footer.DownloadLatexClicked += DownloadLatex;
            Footer.DownloadZipClicked += DownloadZip;

            RefreshLocalization();
        }

        // ── Lokalizáció ───────────────────────────────────────────────────────

        private void RefreshLocalization()
        {
            localizationService.Load(english, scientific);
            LocalizationModel model = localizationService.Current;

            Header.SetLocalization(model);
            StudentData.SetLocalization(model);
            Evaluation.SetLocalization(model);
            Summary.SetLocalization(model);
            Footer.SetLocalization(model);

            // Footer pontszámot is frissítjük (pl. ha jegy label szövege megváltozott)
            UpdateFooterScore();
        }

        public void SetLanguage(bool isEnglish)
        {
            english = isEnglish;
            RefreshLocalization();
        }

        public void SetScientific(bool isScientific)
        {
            scientific = isScientific;
            RefreshLocalization();
        }

        // ── Pontszámítás (ugyanaz mint a JS-ben) ─────────────────────────────

        private void UpdateFooterScore()
        {
            double[] scores = Evaluation.GetScores();
            bool hasZero = scores.Any(s => s == 0.0);
            double total = hasZero ? 0 : Math.Round(scores.Sum() * 10.0) / 10.0;
            string grade = GetGrade(total, hasZero);
            Footer.UpdateScore(total, grade);
        }

        private string GetGrade(double total, bool hasZero)
        {
            LocalizationModel model = localizationService.Current;

            if (model == null) return "-";

            if (hasZero || total <= 25) return model.ElegtelenLabel;
            if (total <= 32) return model.ElegsegessLabel;
            if (total <= 38) return model.KozepesLabel;
            if (total <= 44) return model.JoLabel;
            return model.JelesLabel;
        }

        // ── Adatgyűjtés + Validáció ───────────────────────────────────────────

        /// <summary>
        /// Összegyűjti az összes mezőt AdatokDTO-ba.
        /// Ha validációs hiba van, null-t ad vissza.
        /// </summary>
        private AdatokDTO? GetAllData()
        {
            StudentData.ClearAllErrors();

            string nev = StudentData.GetNev();
            string neptun = StudentData.GetNeptun();
            string cim = StudentData.GetCim();

            // 1. Név: nem üres + nincs benne szám
            if (string.IsNullOrWhiteSpace(nev))
            {
                StudentData.HighlightError(StudentData.GetNevBox());
                ShowValidationError("A hallgató neve kötelező!");
                return null;
            }
            if (nev.Any(char.IsDigit))
            {
                StudentData.HighlightError(StudentData.GetNevBox());
                ShowValidationError("A hallgató neve nem tartalmazhat számot!");
                return null;
            }

            // 2. Neptun: pontosan 6 alfanumerikus karakter
            if (!Regex.IsMatch(neptun, @"^[A-Za-z0-9]{6}$"))
            {
                StudentData.HighlightError(StudentData.GetNeptunBox());
                ShowValidationError("Pontosan 6 karakter, csak betű és szám!");
                return null;
            }

            // 3. Cím: nem üres
            if (string.IsNullOrWhiteSpace(cim))
            {
                StudentData.HighlightError(StudentData.GetCimBox());
                ShowValidationError("A szakdolgozat címe kötelező!");
                return null;
            }

            // 4. Kérdések: legalább 1 nem üres
            List<string> kerdesek = Summary.GetKerdesek();
            if (kerdesek.Count == 0)
            {
                ShowValidationError("Legalább egy kérdés megadása kötelező!");
                return null;
            }

            // Pontszám és jegy
            double[] scores = Evaluation.GetScores();
            bool hasZero = scores.Any(s => s == 0.0);
            double total = hasZero ? 0 : Math.Round(scores.Sum() * 10.0) / 10.0;
            string grade = GetGrade(total, hasZero);

            // Értékelések listája
            var ertekelesek = scores.Select((s, i) => new ErtekelesDTO
            {
                ID = i + 1,
                Jegy = (float)s,
                Megjegyzes = Evaluation.GetMegjegyzes(i)
            }).ToList();

            return new AdatokDTO
            {
                Hallgato = new HallgatoDTO
                {
                    Nev = nev,
                    Netpun = neptun,   // A "Netpun" typo szándékos – API kompatibilitás
                    Szak = (Szakok)StudentData.GetSzakIndex(),
                    SzakdolgozatCime = cim
                },
                Ertekelesek = ertekelesek,
                OsszesitettErtekeles = (float)total,
                RovidSzovegesErtekeles = Summary.GetRovidErtekeles(),
                JavasoltErdemjegy = grade,
                BitraloiJavaslat = Summary.GetBitraloiJavaslat(),
                Kerdesek = kerdesek,
                ErtekeloSzerepe = (ErtekeloSzerepe)Header.GetSelectedRole(),
                OldalNyelve = english ? Nyelv.En : Nyelv.Hu,
                Laptipusa = scientific ? Laptipus.Tudomanyos : Laptipus.Allatalanos
            };
        }

        private void ShowValidationError(string message)
        {
            MessageBox.Show(message, "Hiányzó adat", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        // ── Letöltő műveletek ─────────────────────────────────────────────────

        private void DownloadPdf()
        {
            AdatokDTO? data = GetAllData();
            if (data == null) return;

            SaveFileDialog dlg = new SaveFileDialog
            {
                Filter = "PDF fájl (*.pdf)|*.pdf",
                FileName = "Biralat.pdf"
            };

            if (dlg.ShowDialog() != true) return;

            try
            {
                pdfService.Generate(dlg.FileName, data);
                ShowSuccess("PDF sikeresen létrehozva!", dlg.FileName);
            }
            catch (Exception ex)
            {
                ShowError("Hiba a PDF generálásakor", ex);
            }
        }

        private void DownloadWord()
        {
            AdatokDTO? data = GetAllData();
            if (data == null) return;

            SaveFileDialog dlg = new SaveFileDialog
            {
                Filter = "Word fájl (*.docx)|*.docx",
                FileName = "Biralat.docx"
            };

            if (dlg.ShowDialog() != true) return;

            try
            {
                wordService.Generate(dlg.FileName, data);
                ShowSuccess("Word fájl sikeresen létrehozva!", dlg.FileName);
            }
            catch (Exception ex)
            {
                ShowError("Hiba a Word generálásakor", ex);
            }
        }

        private void DownloadLatex()
        {
            AdatokDTO? data = GetAllData();
            if (data == null) return;

            SaveFileDialog dlg = new SaveFileDialog
            {
                Filter = "LaTeX fájl (*.tex)|*.tex",
                FileName = "Biralat.tex"
            };

            if (dlg.ShowDialog() != true) return;

            try
            {
                latexService.Generate(dlg.FileName, data);
                ShowSuccess("LaTeX fájl sikeresen létrehozva!", dlg.FileName);
            }
            catch (Exception ex)
            {
                ShowError("Hiba a LaTeX generálásakor", ex);
            }
        }

        private void DownloadZip()
        {
            AdatokDTO? data = GetAllData();
            if (data == null) return;

            SaveFileDialog dlg = new SaveFileDialog
            {
                Filter = "ZIP archívum (*.zip)|*.zip",
                FileName = "Dokumentumok.zip"
            };

            if (dlg.ShowDialog() != true) return;

            string tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());

            try
            {
                Directory.CreateDirectory(tempDir);

                string pdfPath = Path.Combine(tempDir, "Biralat.pdf");
                string wordPath = Path.Combine(tempDir, "Biralat.docx");
                string latexPath = Path.Combine(tempDir, "Biralat.tex");

                pdfService.Generate(pdfPath, data);
                wordService.Generate(wordPath, data);
                latexService.Generate(latexPath, data);

                if (File.Exists(dlg.FileName))
                    File.Delete(dlg.FileName);

                ZipFile.CreateFromDirectory(tempDir, dlg.FileName);

                ShowSuccess("ZIP archívum sikeresen létrehozva! (PDF + Word + LaTeX)", dlg.FileName);
            }
            catch (Exception ex)
            {
                ShowError("Hiba a ZIP generálásakor", ex);
            }
            finally
            {
                // Temp mappa törlése
                if (Directory.Exists(tempDir))
                    Directory.Delete(tempDir, true);
            }
        }

        // ── Segédmetódusok ────────────────────────────────────────────────────

        private void ShowSuccess(string message, string filePath)
        {
            MessageBox.Show(
                $"{message}\n\nFájl helye: {filePath}",
                "Siker",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        private void ShowError(string context, Exception ex)
        {
            MessageBox.Show(
                $"{context}:\n{ex.Message}",
                "Hiba",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }
}