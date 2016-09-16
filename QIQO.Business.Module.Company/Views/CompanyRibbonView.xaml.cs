using QIQO.Business.Module.Company.ViewModels;
using System.Windows.Controls.Ribbon;

namespace QIQO.Business.Module.Company.Views
{
    /// <summary>
    /// Interaction logic for CompanyRibbonView.xaml
    /// </summary>
    public partial class CompanyRibbonView : RibbonTab
    {
        public CompanyRibbonView()
        {
            InitializeComponent();
            DataContext = new CompanyRibbonViewModel();
        }
    }
}
