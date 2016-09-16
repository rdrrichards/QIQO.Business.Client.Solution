using Prism.Commands;
using Prism.Regions;
using QIQO.Business.Client.Core.Infrastructure;
using QIQO.Business.Client.Core.UI;
using QIQO.Business.Module.Account.Views;


namespace QIQO.Business.Module.Account.ViewModels
{
    public class AccountHomeViewModel : ViewModelBase
    {
        private readonly IRegionManager _regionManager;

        public AccountHomeViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            NewAccountCommand = new DelegateCommand(NewAccount);
        }
        public DelegateCommand NewAccountCommand { get; set; }

        private void NewAccount()
        {
            _regionManager.RequestNavigate(RegionNames.ContentRegion, typeof(AccountViewX).FullName);
        }
    }
}


