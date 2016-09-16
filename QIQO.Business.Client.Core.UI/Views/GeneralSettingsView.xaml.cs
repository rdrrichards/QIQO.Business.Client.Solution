using System.Windows.Controls;

namespace QIQO.Business.Client.Core.UI
{
    /// <summary>
    /// Interaction logic for GeneralSettingsView.xaml
    /// </summary>
    public partial class GeneralSettingsView : UserControl
    {
        public GeneralSettingsView()
        {
            InitializeComponent();
            DataContext = new GeneralSettingsViewModel();
        }
    }
}
