using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using QIQO.Business.Client.Core.Infrastructure;
using QIQO.Business.Module.Orders.Services;
using QIQO.Business.Module.Orders.Views;

namespace QIQO.Business.Module.Orders.Modules
{
    public class OrderModuleX : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion(RegionNames.ToolBarRegion, typeof(OrderNavigationViewX));
            regionManager.RegisterViewWithRegion(RegionNames.OrdersHomeOpenOrderRegion, typeof(OpenOrderViewX));
            regionManager.RegisterViewWithRegion(RegionNames.OrdersHomeRecentOrderRegion, typeof(WorkingOrderView));
            regionManager.RegisterViewWithRegion(RegionNames.OrdersHomeSearchOrderRegion, typeof(FindOrderViewX));
        }
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IWorkingOrderService, WorkingOrderService>();

            containerRegistry.RegisterForNavigation<OrderHomeView>(typeof(OrderHomeView).FullName);
            containerRegistry.RegisterForNavigation<OrderViewX>(typeof(OrderViewX).FullName);
        }
    }
}
