using Microsoft.Practices.Unity;
using Prism.Regions;
using QIQO.Business.Module.Orders.Views;
using QIQO.Business.Client.Core;
using QIQO.Business.Client.Core.Infrastructure;

namespace QIQO.Business.Module.Orders.Modules
{
    public class OpenOrderModule : ModuleBase
    {
        public OpenOrderModule(IUnityContainer container, IRegionManager region_mgr) : base (container, region_mgr)
        {
        }

        public override void Initialize()
        {
            //RegionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(OpenOrderView));
            RegionManager.RegisterViewWithRegion(RegionNames.DashboardRegion, typeof(OpenOrderView));
            //UnityContainer.RegisterType(typeof(object), typeof(Module), "OpenOrderModule");

            //IRegion region = RegionManager.Regions[RegionNames.ContentRegion];

            //// Scoped Regions code
            //var view = UnityContainer.Resolve<OpenOrderView>();
            //region.Add(view, typeof(OpenOrderView).FullName);
            //region.Activate(view);
        }
    }
}
