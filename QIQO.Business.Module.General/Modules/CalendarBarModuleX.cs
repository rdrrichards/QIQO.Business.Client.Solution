using Microsoft.Practices.Unity;
using Prism.Regions;
using QIQO.Business.Client.Core;
using QIQO.Business.Client.Core.Infrastructure;
using QIQO.Business.Module.General.Views;

namespace QIQO.Business.Module.General.Modules
{
    public class CalendarBarModuleX : ModuleBase
    {
        public CalendarBarModuleX(IUnityContainer container, IRegionManager region_manager) : base(container, region_manager)
        {
        }
        public override void Initialize()
        {
            // RegionManager.RegisterViewWithRegion(RegionNames.HomeCalendarRegion, typeof(CalendarBarViewX));
        }
    }
}
