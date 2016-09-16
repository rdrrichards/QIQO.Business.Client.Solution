using QIQO.Business.Module.Dashboard.ViewModels;
using System.Windows.Controls;

namespace QIQO.Business.Module.Dashboard.Views
{
    /// <summary>
    /// Interaction logic for DashboardView.xaml
    /// </summary>
    public partial class DashboardView : UserControl
    {
        public DashboardView()
        {
            InitializeComponent();
            DataContext = new DashboardViewModel();
        }
    }
}
