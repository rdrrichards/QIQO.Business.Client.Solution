using QIQO.Business.Module.Company.ViewModels;
using System.Windows.Controls;

namespace QIQO.Business.Module.Company.Views
{
    /// <summary>
    /// Interaction logic for ChartOfAccountsView.xaml
    /// </summary>
    public partial class ChartOfAccountsView : UserControl
    {
        public ChartOfAccountsView()
        {
            InitializeComponent();
            DataContext = new ChartOfAccountsViewModel();
        }
    }
}
