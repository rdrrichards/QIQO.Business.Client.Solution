using QIQO.Business.Module.Dashboard.ViewModels;
using System.Windows.Controls.Ribbon;

namespace QIQO.Business.Module.Dashboard.Views
{
    /// <summary>
    /// Interaction logic for DashboardRibbonView.xaml
    /// </summary>
    public partial class DashboardRibbonView : RibbonTab
    {
        public DashboardRibbonView()
        {
            InitializeComponent();
            DataContext = new DashboardRibbonViewModel();
        }
    }
}
