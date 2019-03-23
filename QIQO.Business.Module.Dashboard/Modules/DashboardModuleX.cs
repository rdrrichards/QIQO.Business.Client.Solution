using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using QIQO.Business.Client.Core.Infrastructure;
using QIQO.Business.Module.Dashboard.Views;

namespace QIQO.Business.Module.Dashboard.Modules
{
    public class DashboardModuleX : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion(RegionNames.ToolBarRegion, typeof(DashboardNavigationViewX));
            regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(DashboardViewX));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry) { }
    }
}
