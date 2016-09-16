using Microsoft.Practices.Unity;
using Prism.Regions;
using Prism.Unity;
using QIQO.Business.Client.Core;
using QIQO.Business.Client.Core.Infrastructure;
using QIQO.Business.Module.Account.Interfaces;
using QIQO.Business.Module.Account.Services;
using QIQO.Business.Module.Account.Views;

namespace QIQO.Business.Module.Account.Modules
{
    public class AccountModuleX : ModuleBase
    {
        public AccountModuleX(IUnityContainer container, IRegionManager region_manager) : base(container, region_manager)
        {
        }
        public override void Initialize()
        {
            UnityContainer.RegisterType<IRecentAccountsService, RecentAccountsService>(new ContainerControlledLifetimeManager());
            UnityContainer.RegisterType<IWorkingAccountService, WorkingAccountService>(new ContainerControlledLifetimeManager());

            RegionManager.RegisterViewWithRegion(RegionNames.ToolBarRegion, typeof(AccountNavigationViewX));
            //RegionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(AccountHomeView));
            RegionManager.RegisterViewWithRegion(RegionNames.AccountsHomeRecentAccountRegion, typeof(RecentAccountView));
            RegionManager.RegisterViewWithRegion(RegionNames.AccountsHomeSearchAccountRegion, typeof(AccountFinderViewX));

            RegionManager.RegisterViewWithRegion(RegionNames.AccountsHomeOpenAccountRegion, typeof(WorkingAccountView));

            UnityContainer.RegisterTypeForNavigation<AccountHomeView>(typeof(AccountHomeView).FullName);
            UnityContainer.RegisterTypeForNavigation<AccountViewX>(typeof(AccountViewX).FullName);

            //UnityContainer.RegisterType(typeof(object), typeof(AccountFinderViewX), typeof(AccountFinderViewX).FullName);
            //UnityContainer.RegisterType(typeof(object), typeof(AccountView), typeof(AccountView).FullName);
        }
    }
}
