using System.Windows;
using QuestPDF.Infrastructure;

namespace SzakdolgozatErtekeloWPF
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // QuestPDF Community license (ingyenes, nem kereskedelmi)
            QuestPDF.Settings.License = LicenseType.Community;
        }
    }
}
