using System.Windows;
using System.Windows.Controls;
using SzakdolgozatErtekeloWPF.Models;

namespace SzakdolgozatErtekeloWPF.Controls
{
    /// <summary>
    /// Interaction logic for HeaderControl.xaml
    /// </summary>
    public partial class HeaderControl : UserControl
    {
        public HeaderControl()
        {
            InitializeComponent();
        }

        public void SetLocalization(LocalizationModel model)
        {
            TitleText.Text = model.HeaderCim;

            int selectedIndex = RoleComboBox.SelectedIndex;

            RoleComboBox.Items.Clear();
            RoleComboBox.Items.Add(model.SzerepkorKonzulens);
            RoleComboBox.Items.Add(model.SzerepkorOpponens);

            RoleComboBox.SelectedIndex = selectedIndex < 0 ? 0 : selectedIndex;
        }

        /// <summary>Visszaadja a kiválasztott szerepkör indexét (0=Konzulens, 1=Opponens).</summary>
        public int GetSelectedRole() => RoleComboBox.SelectedIndex < 0 ? 0 : RoleComboBox.SelectedIndex;
    }
}
