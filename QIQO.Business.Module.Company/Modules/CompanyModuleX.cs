using Microsoft.Practices.Unity;
using Prism.Regions;
using QIQO.Business.Client.Core;
using QIQO.Business.Client.Core.Infrastructure;
using QIQO.Business.Module.Company.Views;

namespace QIQO.Business.Module.Company.Modules
{
    [Roles(Security.QIQOCompanyAdminRole)]
    public class CompanyModuleX : ModuleBase
    {
        public CompanyModuleX(IUnityContainer container, IRegionManager region_manager) : base(container, region_manager)
        {
        }
        public override void Initialize()
        {
            //RegionManager.RegisterViewWithRegion(RegionNames.CompanyRegion, typeof(CompanyView));
            RegionManager.RegisterViewWithRegion(RegionNames.ToolBarRegion, typeof(CompanyNavigationViewX));
            //RegionManager.RegisterViewWithRegion(RegionNames.RibbonRegion, typeof(CompanyRibbonView));

            UnityContainer.RegisterType(typeof(object), typeof(CompanyView), typeof(CompanyView).FullName);
            UnityContainer.RegisterType(typeof(object), typeof(ChartOfAccountsView), typeof(ChartOfAccountsView).FullName);
            UnityContainer.RegisterType(typeof(object), typeof(CompanyRibbonView), typeof(CompanyRibbonView).FullName);
        }
    }
}