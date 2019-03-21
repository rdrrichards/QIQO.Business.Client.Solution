using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using QIQO.Business.Client.Core.Infrastructure;
using QIQO.Business.Module.Account.Views;

namespace QIQO.Business.Module.Account.Modules
{
    public class AccountModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion(RegionNames.ToolBarRegion, typeof(AccountNavigationView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register(typeof(object), typeof(AccountFinderView), typeof(AccountFinderView).FullName);
            containerRegistry.Register(typeof(object), typeof(AccountView), typeof(AccountView).FullName);
            containerRegistry.Register(typeof(object), typeof(AccountRibbonView), typeof(AccountRibbonView).FullName);
        }
    }
}
