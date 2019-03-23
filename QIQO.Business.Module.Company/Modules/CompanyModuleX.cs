using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using QIQO.Business.Client.Core;
using QIQO.Business.Client.Core.Infrastructure;
using QIQO.Business.Module.Company.Views;

namespace QIQO.Business.Module.Company.Modules
{
    [Roles(Security.QIQOCompanyAdminRole)]
    public class CompanyModuleX : IModule
    {
        //public override void Initialize()
        //{
        //    //RegionManager.RegisterViewWithRegion(RegionNames.CompanyRegion, typeof(CompanyView));
        //    RegionManager.RegisterViewWithRegion(RegionNames.ToolBarRegion, typeof(CompanyNavigationViewX));
        //    //RegionManager.RegisterViewWithRegion(RegionNames.RibbonRegion, typeof(CompanyRibbonView));

        //    UnityContainer.RegisterType(typeof(object), typeof(CompanyView), typeof(CompanyView).FullName);
        //    UnityContainer.RegisterType(typeof(object), typeof(ChartOfAccountsView), typeof(ChartOfAccountsView).FullName);
        //    UnityContainer.RegisterType(typeof(object), typeof(CompanyRibbonView), typeof(CompanyRibbonView).FullName);
        //}
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion(RegionNames.ToolBarRegion, typeof(CompanyNavigationViewX));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register(typeof(object), typeof(CompanyView), typeof(CompanyView).FullName);
            containerRegistry.Register(typeof(object), typeof(ChartOfAccountsView), typeof(ChartOfAccountsView).FullName);
            containerRegistry.Register(typeof(object), typeof(CompanyRibbonView), typeof(CompanyRibbonView).FullName);
        }
    }
}