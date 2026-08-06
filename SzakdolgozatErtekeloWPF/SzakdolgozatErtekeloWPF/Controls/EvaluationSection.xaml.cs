using System;
using System.Windows;
using System.Windows.Controls;
using SzakdolgozatErtekeloWPF.Models;

namespace SzakdolgozatErtekeloWPF.Controls
{
    /// <summary>
    /// Interaction logic for EvaluationSection.xaml
    /// </summary>
    public partial class EvaluationSection : UserControl
    {
        /// <summary>
        /// Akkor tüzel, amikor bármelyik értékelési kártya slider értéke megváltozik.
        /// </summary>
        public event Action ScoresChanged;

        public EvaluationSection()
        {
            InitializeComponent();

            // Feliratkozás minden kártya ValueChanged eseményére
            foreach (EvaluationCard card in GetAllCards())
                card.ValueChanged += () => ScoresChanged?.Invoke();
        }

        public void SetLocalization(LocalizationModel model)
        {
            EvaluationTitleText.Text = model.ErtekelesekCim;

            if (model.Criteria == null)
                return;

            Card1.SetLocalization(model.Criteria[0], model);
            Card2.SetLocalization(model.Criteria[1], model);
            Card3.SetLocalization(model.Criteria[2], model);
            Card4.SetLocalization(model.Criteria[3], model);
            Card5.SetLocalization(model.Criteria[4], model);
            Card6.SetLocalization(model.Criteria[5], model);
            Card7.SetLocalization(model.Criteria[6], model);
            Card8.SetLocalization(model.Criteria[7], model);
            Card9.SetLocalization(model.Criteria[8], model);
            Card10.SetLocalization(model.Criteria[9], model);
        }

        /// <summary>
        /// Visszaadja mind a 10 kártya slider értékét tömbként (0. index = 1. kártya).
        /// </summary>
        public double[] GetScores()
        {
            var cards = GetAllCards();
            double[] scores = new double[cards.Length];

            for (int i = 0; i < cards.Length; i++)
                scores[i] = cards[i].GetValue();

            return scores;
        }

        /// <summary>
        /// Visszaadja az adott indexű kártya megjegyzés szövegét (0-alapú index).
        /// </summary>
        public string GetMegjegyzes(int index)
        {
            return GetAllCards()[index].GetMegjegyzes();
        }

        private EvaluationCard[] GetAllCards()
        {
            return new[]
            {
                Card1, Card2, Card3, Card4, Card5,
                Card6, Card7, Card8, Card9, Card10
            };
        }
    }
}
