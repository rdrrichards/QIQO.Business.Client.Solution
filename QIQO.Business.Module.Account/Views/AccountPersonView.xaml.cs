using QIQO.Business.Module.Account.ViewModels;
using System.Windows.Controls;

namespace QIQO.Business.Module.Account.Views
{
    /// <summary>
    /// Interaction logic for AccountPersonView.xaml
    /// </summary>
    public partial class AccountPersonView : UserControl
    {
        public AccountPersonView()
        {
            InitializeComponent();
            DataContext = new AccountPersonViewModel();
        }
    }
}
