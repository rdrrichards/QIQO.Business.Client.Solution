using Microsoft.Practices.Unity;
using Prism.Regions;
using QIQO.Business.Client.Core;
using QIQO.Business.Client.Core.Infrastructure;
using QIQO.Business.Module.Account.Views;

namespace QIQO.Business.Module.Account.Modules
{
    public class AccountModule : ModuleBase
    {
        public AccountModule(IUnityContainer container, IRegionManager region_manager) : base(container, region_manager)
        {
        }
        public override void Initialize()
        {
            //RegionManager.RegisterViewWithRegion(RegionNames.AccountsRegion, typeof(AccountView));
            RegionManager.RegisterViewWithRegion(RegionNames.ToolBarRegion, typeof(AccountNavigationView));
            //RegionManager.RegisterViewWithRegion(RegionNames.RibbonRegion, typeof(AccountRibbonView));

            UnityContainer.RegisterType(typeof(object), typeof(AccountFinderView), typeof(AccountFinderView).FullName);
            UnityContainer.RegisterType(typeof(object), typeof(AccountView), typeof(AccountView).FullName);
            UnityContainer.RegisterType(typeof(object), typeof(AccountRibbonView), typeof(AccountRibbonView).FullName);
        }
    }
}
