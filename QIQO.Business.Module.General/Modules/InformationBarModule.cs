using Microsoft.Practices.Unity;
using Prism.Regions;
using QIQO.Business.Module.General.Views;
using QIQO.Business.Client.Core;
using QIQO.Business.Client.Core.Infrastructure;

namespace QIQO.Business.Module.General.Modules
{
    public class InformationBarModule : ModuleBase
    {
        public InformationBarModule(IUnityContainer container, IRegionManager region_manager) : base(container, region_manager)
        {
        }
        public override void Initialize()
        {
            RegionManager.RegisterViewWithRegion(RegionNames.InformationBarRegion, typeof(InformationBarView));
        }
    }
}
