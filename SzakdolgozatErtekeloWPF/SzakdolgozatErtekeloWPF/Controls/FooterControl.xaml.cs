using System;
using System.Windows;
using System.Windows.Controls;
using SzakdolgozatErtekeloWPF.Models;

namespace SzakdolgozatErtekeloWPF.Controls
{
    /// <summary>
    /// Interaction logic for FooterControl.xaml
    /// </summary>
    public partial class FooterControl : UserControl
    {
        // Események – MainWindow kezeli a tényleges letöltést
        public event Action DownloadPdfClicked;
        public event Action DownloadWordClicked;
        public event Action DownloadLatexClicked;
        public event Action DownloadZipClicked;

        public FooterControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Frissíti a megjelenített összpontszámot és javasolt érdemjegyet.
        /// </summary>
        public void UpdateScore(double total, string grade)
        {
            OsszpontszamValue.Text = total.ToString("0.#");
            JavasoltErdemjegyValue.Text = grade;
        }

        /// <summary>
        /// Frissíti a feliratok szövegét lokalizáció szerint.
        /// </summary>
        public void SetLocalization(LocalizationModel model)
        {
            OsszpontszamLabelRun.Text = model.Osszpontszam + ": ";
            JavasoltErdemjegyLabelRun.Text = model.JavasoltErdemjegy + ": ";
        }

        private void PdfButton_Click(object sender, RoutedEventArgs e)
            => DownloadPdfClicked?.Invoke();

        private void WordButton_Click(object sender, RoutedEventArgs e)
            => DownloadWordClicked?.Invoke();

        private void LatexButton_Click(object sender, RoutedEventArgs e)
            => DownloadLatexClicked?.Invoke();

        private void ZipButton_Click(object sender, RoutedEventArgs e)
            => DownloadZipClicked?.Invoke();
    }
}
