using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using SzakdolgozatErtekeloWPF.Models;

namespace SzakdolgozatErtekeloWPF.Controls
{
    /// <summary>
    /// Interaction logic for StudentDataControl.xaml
    /// </summary>
    public partial class StudentDataControl : UserControl
    {
        public StudentDataControl()
        {
            InitializeComponent();

            // Neptun élő szűrő – csak alfanumerikus, max 6 karakter
            NeptunTextBox.TextChanged += NeptunTextBox_TextChanged;

            // Gépeléskor törlődik a hibakiemelés
            NevTextBox.TextChanged += (s, e) => ClearFieldError(NevTextBox);
            NeptunTextBox.TextChanged += (s, e) => ClearFieldError(NeptunTextBox);
            CimTextBox.TextChanged += (s, e) => ClearFieldError(CimTextBox);
        }

        public void SetLocalization(LocalizationModel model)
        {
            TitleText.Text = model.HallgatoAdatai;
            NameLabel.Text = model.HallgatoNeve;
            NeptunLabel.Text = model.HallgatoNeptun;
            SzakLabel.Text = model.HallgatoSzak;
            CimLabel.Text = model.HallgatoCim;

            int selectedIndex = SzakComboBox.SelectedIndex;

            SzakComboBox.Items.Clear();
            SzakComboBox.Items.Add(model.SzakProgramtervezo);
            SzakComboBox.Items.Add(model.SzakGazdasagi);

            SzakComboBox.SelectedIndex = selectedIndex < 0 ? 0 : selectedIndex;
        }

        // ── Neptun élő szűrő ──────────────────────────────────────────────────
        private void NeptunTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filtered = Regex.Replace(NeptunTextBox.Text, @"[^A-Za-z0-9]", "");

            if (filtered.Length > 6)
                filtered = filtered.Substring(0, 6);

            if (filtered != NeptunTextBox.Text)
            {
                int caret = NeptunTextBox.CaretIndex;
                NeptunTextBox.Text = filtered;
                NeptunTextBox.CaretIndex = Math.Min(caret, filtered.Length);
            }
        }

        // ── Hibakiemelés ──────────────────────────────────────────────────────

        public void HighlightError(TextBox textBox)
        {
            textBox.BorderBrush = new SolidColorBrush(Color.FromRgb(0xE0, 0x3F, 0x3F));
            textBox.BorderThickness = new Thickness(2);
            textBox.Background = new SolidColorBrush(Color.FromRgb(0xFF, 0xF5, 0xF5));
        }

        public void ClearFieldError(TextBox textBox)
        {
            textBox.BorderBrush = new SolidColorBrush(Color.FromRgb(0xDD, 0xDD, 0xDD));
            textBox.BorderThickness = new Thickness(1);
            textBox.Background = Brushes.White;
        }

        public void ClearAllErrors()
        {
            ClearFieldError(NevTextBox);
            ClearFieldError(NeptunTextBox);
            ClearFieldError(CimTextBox);
        }

        // ── Adatgyűjtő metódusok ──────────────────────────────────────────────

        public string GetNev() => NevTextBox.Text.Trim();
        public string GetNeptun() => NeptunTextBox.Text.Trim();
        public int GetSzakIndex() => SzakComboBox.SelectedIndex;
        public string GetCim() => CimTextBox.Text.Trim();

        // TextBox referenciák a hibakiemeléshez (MainWindow-ból hívva)
        public TextBox GetNevBox() => NevTextBox;
        public TextBox GetNeptunBox() => NeptunTextBox;
        public TextBox GetCimBox() => CimTextBox;
    }
}
