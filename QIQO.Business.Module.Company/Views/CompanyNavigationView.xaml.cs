using QIQO.Business.Module.Company.ViewModels;
using System.Windows.Controls;

namespace QIQO.Business.Module.Company.Views
{
    /// <summary>
    /// Interaction logic for CompanyNavigationView.xaml
    /// </summary>
    public partial class CompanyNavigationView : UserControl
    {
        public CompanyNavigationView()
        {
            InitializeComponent();
            DataContext = new CompanyNavigationViewModel();
        }
    }
}
