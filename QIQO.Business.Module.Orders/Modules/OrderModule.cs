using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using QIQO.Business.Client.Core.Infrastructure;
using QIQO.Business.Module.Orders.Views;

namespace QIQO.Business.Module.Orders.Modules
{
    public class OrderModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion(RegionNames.ToolBarRegion, typeof(OrderNavigationView));
        }
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register(typeof(object), typeof(OrderShellView), typeof(OrderShellView).FullName);
            containerRegistry.Register(typeof(object), typeof(OrderView), typeof(OrderView).FullName);
            containerRegistry.Register(typeof(object), typeof(FindOrderView), typeof(FindOrderView).FullName);
            containerRegistry.Register(typeof(object), typeof(OrderRibbonView), typeof(OrderRibbonView).FullName);
        }
    }
}
