using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using QIQO.Business.Module.General.Views;
using QIQO.Business.Client.Core.Infrastructure;

namespace QIQO.Business.Module.General.Modules
{
    public class QuickLinkModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion(RegionNames.HomeShortcutRegion, typeof(QuickLinkView));
        }
        public void RegisterTypes(IContainerRegistry containerRegistry) { }
    }
}
