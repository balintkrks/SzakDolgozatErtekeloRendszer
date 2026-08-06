using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using SzakdolgozatErtekeloWPF.Models;

namespace SzakdolgozatErtekeloWPF.Controls
{
    /// <summary>
    /// Szöveges értékelés, bírálói javaslat és kérdések szekció
    /// </summary>
    public partial class SummaryControl : UserControl
    {
        public SummaryControl()
        {
            InitializeComponent();

            // Kezdeti 2 kérdés sor
            AddKerdes();
            AddKerdes();
        }

        public void SetLocalization(LocalizationModel model)
        {
            SzovegesErtekelesCimText.Text = model.SzovegesErtekelesCim;
            BiraloiJavaslatkCimText.Text = model.BiraloiJavaslatkCim;
            KerdesekCimText.Text = model.KerdesekCim;
            UjKerdesGomb.Content = model.UjKerdesGomb;
        }

        private void AddKerdes()
        {
            DockPanel row = new DockPanel
            {
                Margin = new Thickness(0, 0, 0, 8),
                LastChildFill = true
            };

            // Törlő gomb (jobb oldal)
            Button deleteBtn = new Button
            {
                Content = "×",
                Style = Application.Current.Resources["DeleteButton"] as Style,
                Margin = new Thickness(8, 0, 0, 0),
                VerticalAlignment = VerticalAlignment.Center
            };

            // Kérdés szövegmező
            TextBox input = new TextBox
            {
                Height = 35,
                Padding = new Thickness(10, 5, 10, 5),
                VerticalContentAlignment = VerticalAlignment.Center,
                BorderBrush = new SolidColorBrush(Color.FromRgb(0xDD, 0xDD, 0xDD)),
                BorderThickness = new Thickness(1)
            };

            deleteBtn.Click += (s, e) =>
            {
                // Legalább 1 sor marad
                if (KerdesekLista.Children.Count > 1)
                    KerdesekLista.Children.Remove(row);
            };

            DockPanel.SetDock(deleteBtn, Dock.Right);
            row.Children.Add(deleteBtn);
            row.Children.Add(input);

            KerdesekLista.Children.Add(row);
        }

        private void UjKerdesGomb_Click(object sender, RoutedEventArgs e)
        {
            AddKerdes();
        }

        /// <summary>
        /// Visszaadja a nem üres kérdéseket (mint a JS getKerdesek())
        /// </summary>
        public List<string> GetKerdesek()
        {
            var result = new List<string>();

            foreach (DockPanel row in KerdesekLista.Children.OfType<DockPanel>())
            {
                TextBox tb = row.Children.OfType<TextBox>().FirstOrDefault();

                if (tb != null && !string.IsNullOrWhiteSpace(tb.Text))
                    result.Add(tb.Text.Trim());
            }

            return result;
        }

        public string GetRovidErtekeles() => RovidErtekelesBox.Text.Trim();

        public string GetBitraloiJavaslat() => BitraloiJavaslat.Text.Trim();
    }
}
