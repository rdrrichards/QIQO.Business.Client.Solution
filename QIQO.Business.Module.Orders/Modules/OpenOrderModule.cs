using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using QIQO.Business.Module.Orders.Views;
using QIQO.Business.Client.Core.Infrastructure;

namespace QIQO.Business.Module.Orders.Modules
{
    public class OpenOrderModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion(RegionNames.DashboardRegion, typeof(OpenOrderView));
        }
        public void RegisterTypes(IContainerRegistry containerRegistry) { }
    }
}
