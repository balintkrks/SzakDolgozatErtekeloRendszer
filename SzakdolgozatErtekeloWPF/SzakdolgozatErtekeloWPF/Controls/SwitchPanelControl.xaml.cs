using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SzakdolgozatErtekeloWPF.Controls
{
    /// <summary>
    /// Interaction logic for SwitchPanelControl.xaml
    /// </summary>
    public partial class SwitchPanelControl : UserControl
    {
        public event Action<bool> LanguageChanged;
        public event Action<bool> ScientificChanged;


        public SwitchPanelControl()
        {
            InitializeComponent();

            MagyarRadio.Checked += MagyarRadio_Checked;
            AngolRadio.Checked += AngolRadio_Checked;

            AltalanosRadio.Checked += AltalanosRadio_Checked;
            TudomanyosRadio.Checked += TudomanyosRadio_Checked;
        }


        private void AngolRadio_Checked(object sender, RoutedEventArgs e)
        {
            LanguageChanged.Invoke(true);
        }


        private void MagyarRadio_Checked(object sender, RoutedEventArgs e)
        {
            LanguageChanged.Invoke(false);
        }


        private void TudomanyosRadio_Checked(object sender, RoutedEventArgs e)
        {
            ScientificChanged.Invoke(true);
        }


        private void AltalanosRadio_Checked(object sender, RoutedEventArgs e)
        {
            ScientificChanged.Invoke(false);
        }
    }
}
