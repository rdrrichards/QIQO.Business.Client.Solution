using QIQO.Business.Module.Account.ViewModels;
using System.Windows.Controls;

namespace QIQO.Business.Module.Account.Views
{
    /// <summary>
    /// Interaction logic for AccountFinderView.xaml
    /// </summary>
    public partial class AccountFinderView : UserControl
    {
        public AccountFinderView()
        {
            InitializeComponent();
            DataContext = new AccountFinderViewModel();
        }
    }
}
