using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using SzakdolgozatErtekeloWPF.Models;

namespace SzakdolgozatErtekeloWPF.Controls
{
    /// <summary>
    /// Interaction logic for EvaluationCard.xaml
    /// </summary>
    public partial class EvaluationCard : UserControl
    {
        /// <summary>
        /// Akkor tüzel, amikor a slider értéke megváltozik.
        /// </summary>
        public event Action ValueChanged;

        public EvaluationCard()
        {
            InitializeComponent();
            PointSlider.ValueChanged += PointSlider_ValueChanged;
        }

        public void SetLocalization(EvaluationCriterion criterion, LocalizationModel localization)
        {
            NumberText.Text = criterion.ID + ".";
            TitleText.Text = criterion.Title;
            ZeroText.Text = "• " + criterion.ZeroPointText;
            OneTwoText.Text = "• " + criterion.OneTwoPointText;
            ThreeFourText.Text = "• " + criterion.ThreeFourPointText;
            FiveText.Text = "• " + criterion.FivePointText;
            PointLabelRun.Text = localization.AdottPont + ": ";
            CommentLabelText.Text = localization.MegjegyzesPlaceholder;
        }

        private void PointSlider_ValueChanged(
            object sender,
            RoutedPropertyChangedEventArgs<double> e)
        {
            if (PointText != null)
            {
                PointText.Text = PointSlider.Value.ToString("0.0");

                // Ha nulla: piros, egyébként kék
                PointText.Foreground = PointSlider.Value == 0
                    ? new SolidColorBrush(Color.FromRgb(0xE0, 0x3F, 0x3F))
                    : new SolidColorBrush(Color.FromRgb(0x3F, 0x5F, 0xE0));
            }

            ValueChanged?.Invoke();
        }

        /// <summary>Visszaadja a slider aktuális értékét.</summary>
        public double GetValue() => PointSlider.Value;

        /// <summary>Visszaadja a megjegyzés szövegét.</summary>
        public string GetMegjegyzes() => CommentTextBox.Text.Trim();
    }
}
