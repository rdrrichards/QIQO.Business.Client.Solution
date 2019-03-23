using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using QIQO.Business.Module.General.Views;
using QIQO.Business.Client.Core.Infrastructure;

namespace QIQO.Business.Module.General.Modules
{
    public class StatusBarModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion(RegionNames.StatusBarRegion, typeof(StatusBarView));
        }
        public void RegisterTypes(IContainerRegistry containerRegistry) { }
    }
}
