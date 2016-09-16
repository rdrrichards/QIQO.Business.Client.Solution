using System.Windows.Controls;

namespace QIQO.Business.Client.Core.UI
{
    /// <summary>
    /// Interaction logic for UserSettingsView.xaml
    /// </summary>
    public partial class UserSettingsView : UserControl
    {
        public UserSettingsView()
        {
            InitializeComponent();
            DataContext = new UserSettingsViewModel();
        }
    }
}
