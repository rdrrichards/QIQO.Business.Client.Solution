using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using QIQO.Business.Client.Core.Infrastructure;
using QIQO.Business.Module.Account.Interfaces;
using QIQO.Business.Module.Account.Services;
using QIQO.Business.Module.Account.Views;

namespace QIQO.Business.Module.Account.Modules
{
    public class AccountModuleX : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();

            regionManager.RegisterViewWithRegion(RegionNames.ToolBarRegion, typeof(AccountNavigationViewX));
            regionManager.RegisterViewWithRegion(RegionNames.AccountsHomeRecentAccountRegion, typeof(RecentAccountView));
            regionManager.RegisterViewWithRegion(RegionNames.AccountsHomeSearchAccountRegion, typeof(AccountFinderViewX));
            regionManager.RegisterViewWithRegion(RegionNames.AccountsHomeOpenAccountRegion, typeof(WorkingAccountView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IRecentAccountsService, RecentAccountsService>();
            containerRegistry.RegisterSingleton<IWorkingAccountService, WorkingAccountService>();
            containerRegistry.RegisterForNavigation<AccountHomeView>(typeof(AccountHomeView).FullName);
            containerRegistry.RegisterForNavigation<AccountViewX>(typeof(AccountViewX).FullName);
        }
    }
}
