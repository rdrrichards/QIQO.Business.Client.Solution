using Microsoft.Practices.Unity;
using Prism.Regions;
using QIQO.Business.Module.Orders.Views;
using QIQO.Business.Client.Core;
using QIQO.Business.Client.Core.Infrastructure;
using Prism.Unity;

namespace QIQO.Business.Module.Orders.Modules
{
    public class OpenOrderModuleX : ModuleBase
    {
        public OpenOrderModuleX(IUnityContainer container, IRegionManager region_mgr) : base (container, region_mgr)
        {
        }

        public override void Initialize()
        {
            RegionManager.RegisterViewWithRegion(RegionNames.DashboardRegion, typeof(OpenOrderViewX));
            //UnityContainer.RegisterTypeForNavigation<OpenOrderViewX>("OpenOrderViewX");
        }
    }
}
