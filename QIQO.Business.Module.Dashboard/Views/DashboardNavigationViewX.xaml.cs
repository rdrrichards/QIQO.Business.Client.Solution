using QIQO.Business.Module.Dashboard.ViewModels;
using System.Windows.Controls;

namespace QIQO.Business.Module.Dashboard.Views
{
    /// <summary>
    /// Interaction logic for DashboardNavigationView.xaml
    /// </summary>
    public partial class DashboardNavigationViewX : UserControl
    {
        public DashboardNavigationViewX()
        {
            InitializeComponent();
            DataContext = new DashboardNavigationViewModelX();
        }
    }
}
