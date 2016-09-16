using QIQO.Business.Module.Account.ViewModels;
using System.Windows.Controls;

namespace QIQO.Business.Module.Account.Views
{
    /// <summary>
    /// Interaction logic for AccountView.xaml
    /// </summary>
    public partial class AccountView : UserControl
    {
        public AccountView(AccountViewModel view_model)
        {
            InitializeComponent();
            DataContext = view_model;
        }
    }
}
