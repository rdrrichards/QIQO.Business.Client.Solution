using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using QIQO.Business.Client.Core;
using QIQO.Business.Client.Core.Infrastructure;
using QIQO.Business.Module.Company.Views;

namespace QIQO.Business.Module.Company.Modules
{
    [Roles(Security.QIQOCompanyAdminRole)]
    public class CompanyModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion(RegionNames.ToolBarRegion, typeof(CompanyNavigationView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register(typeof(object), typeof(CompanyView), typeof(CompanyView).FullName);
            containerRegistry.Register(typeof(object), typeof(ChartOfAccountsView), typeof(ChartOfAccountsView).FullName);
            containerRegistry.Register(typeof(object), typeof(CompanyRibbonView), typeof(CompanyRibbonView).FullName);
        }
    }
}