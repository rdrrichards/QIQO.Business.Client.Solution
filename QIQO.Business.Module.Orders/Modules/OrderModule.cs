using Microsoft.Practices.Unity;
using Prism.Regions;
using QIQO.Business.Client.Core;
using QIQO.Business.Client.Core.Infrastructure;
using QIQO.Business.Module.Orders.Views;

namespace QIQO.Business.Module.Orders.Modules
{
    public class OrderModule : ModuleBase
    {
        public OrderModule(IUnityContainer container, IRegionManager region_manager) : base(container, region_manager)
        {
        }
        public override void Initialize()
        {
            //RegionManager.RegisterViewWithRegion(RegionNames.OrdersRegion, typeof(OrderView));

            RegionManager.RegisterViewWithRegion(RegionNames.ToolBarRegion, typeof(OrderNavigationView));
            //RegionManager.RegisterViewWithRegion(RegionNames.RibbonRegion, typeof(OrderRibbonView));
            //RegionManager.RegisterViewWithRegion(RegionNames.OrdersRegion, typeof(OrderView));

            UnityContainer.RegisterType(typeof(object), typeof(OrderShellView), typeof(OrderShellView).FullName);
            UnityContainer.RegisterType(typeof(object), typeof(OrderView), typeof(OrderView).FullName);
            UnityContainer.RegisterType(typeof(object), typeof(FindOrderView), typeof(FindOrderView).FullName);
            UnityContainer.RegisterType(typeof(object), typeof(OrderRibbonView), typeof(OrderRibbonView).FullName);

            //IRegion region = RegionManager.Regions[RegionNames.ContentRegion];

            // Scoped Regions code
            //var view = UnityContainer.Resolve<OrderView>();
            //region.Add(view, typeof(OrderView).FullName);
            //region.Deactivate(view);
        }
    }
}
