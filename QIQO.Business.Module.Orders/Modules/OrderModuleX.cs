using Microsoft.Practices.Unity;
using Prism.Regions;
using Prism.Unity;
using QIQO.Business.Client.Core;
using QIQO.Business.Client.Core.Infrastructure;
using QIQO.Business.Module.Orders.Services;
using QIQO.Business.Module.Orders.Views;

namespace QIQO.Business.Module.Orders.Modules
{
    public class OrderModuleX : ModuleBase
    {
        public OrderModuleX(IUnityContainer container, IRegionManager region_manager) : base(container, region_manager)
        {
        }
        public override void Initialize()
        {
            UnityContainer.RegisterType<IWorkingOrderService, WorkingOrderService>(new ContainerControlledLifetimeManager());

            //RegionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(OrderHomeView));
            RegionManager.RegisterViewWithRegion(RegionNames.ToolBarRegion, typeof(OrderNavigationViewX));
            //RegionManager.RegisterViewWithRegion(RegionNames.ToolBarRegion, typeof(OrderNavigationViewX));


            RegionManager.RegisterViewWithRegion(RegionNames.OrdersHomeOpenOrderRegion, typeof(OpenOrderViewX));
            RegionManager.RegisterViewWithRegion(RegionNames.OrdersHomeRecentOrderRegion, typeof(WorkingOrderView));
            RegionManager.RegisterViewWithRegion(RegionNames.OrdersHomeSearchOrderRegion, typeof(FindOrderViewX));

            UnityContainer.RegisterTypeForNavigation<OrderHomeView>(typeof(OrderHomeView).FullName);
            UnityContainer.RegisterTypeForNavigation<OrderViewX>(typeof(OrderViewX).FullName);

            //UnityContainer.RegisterType(typeof(object), typeof(OrderShellView), typeof(OrderShellView).FullName);
            //UnityContainer.RegisterType(typeof(object), typeof(OrderView), typeof(OrderView).FullName);
            //UnityContainer.RegisterType(typeof(object), typeof(FindOrderView), typeof(FindOrderView).FullName);
            //UnityContainer.RegisterType(typeof(object), typeof(OrderRibbonView), typeof(OrderRibbonView).FullName);
        }
    }
}
