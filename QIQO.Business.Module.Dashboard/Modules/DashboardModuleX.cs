using Microsoft.Practices.Unity;
using Prism.Regions;
using QIQO.Business.Client.Core;
using QIQO.Business.Client.Core.Infrastructure;
using QIQO.Business.Module.Dashboard.Views;

namespace QIQO.Business.Module.Dashboard.Modules
{
    public class DashboardModuleX : ModuleBase
    {
        public DashboardModuleX(IUnityContainer container, IRegionManager region_manager) : base(container, region_manager)
        {
        }
        public override void Initialize()
        {
            RegionManager.RegisterViewWithRegion(RegionNames.ToolBarRegion, typeof(DashboardNavigationViewX));
            RegionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(DashboardViewX));
        }
    }
}
