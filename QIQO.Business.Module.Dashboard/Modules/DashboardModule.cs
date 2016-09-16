using Microsoft.Practices.Unity;
using Prism.Regions;
using QIQO.Business.Client.Core;
using QIQO.Business.Client.Core.Infrastructure;
using QIQO.Business.Module.Dashboard.Views;

namespace QIQO.Business.Module.Dashboard.Modules
{
    public class DashboardModule : ModuleBase
    {
        public DashboardModule(IUnityContainer container, IRegionManager region_manager) : base(container, region_manager)
        {
        }
        public override void Initialize()
        {
            RegionManager.RegisterViewWithRegion(RegionNames.ToolBarRegion, typeof(DashboardNavigationView));
            //RegionManager.RegisterViewWithRegion(RegionNames.RibbonRegion, typeof(DashboardRibbonView));
            RegionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(DashboardView));

            UnityContainer.RegisterType(typeof(object), typeof(DashboardView), typeof(DashboardView).FullName);
            UnityContainer.RegisterType(typeof(object), typeof(DashboardRibbonView), typeof(DashboardRibbonView).FullName);
            //RegionManager.RegisterViewWithRegion(RegionNames.RibbonRegion, typeof(HelpRibbonView));
            //RegionManager.RegisterViewWithRegion(RegionNames.RibbonRegion, typeof(WindowRibbonView));

            RegionManager.RequestNavigate(RegionNames.RibbonRegion, typeof(DashboardRibbonView).FullName);
        }
    }
}
