using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using QIQO.Business.Client.Core.Infrastructure;
using QIQO.Business.Module.Dashboard.Views;

namespace QIQO.Business.Module.Dashboard.Modules
{
    public class DashboardModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion(RegionNames.ToolBarRegion, typeof(DashboardNavigationView));
            regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(DashboardView));
            regionManager.RequestNavigate(RegionNames.RibbonRegion, typeof(DashboardRibbonView).FullName);
        }
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register(typeof(object), typeof(DashboardView), typeof(DashboardView).FullName);
            containerRegistry.Register(typeof(object), typeof(DashboardRibbonView), typeof(DashboardRibbonView).FullName);
        }
    }
}
