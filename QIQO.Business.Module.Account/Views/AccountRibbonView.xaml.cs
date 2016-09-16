using QIQO.Business.Module.Account.ViewModels;
using System.Windows.Controls.Ribbon;

namespace QIQO.Business.Module.Account.Views
{
    /// <summary>
    /// Interaction logic for AccountRibbonView.xaml
    /// </summary>
    public partial class AccountRibbonView : RibbonTab //, IRegionMemberLifetime
    {
        public AccountRibbonView()
        {
            InitializeComponent();
            DataContext = new AccountRibbonViewModel();
        }

        //public bool KeepAlive { get; } = false;
    }
}
