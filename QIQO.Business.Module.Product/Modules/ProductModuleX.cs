using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using QIQO.Business.Client.Core.Infrastructure;
using QIQO.Business.Module.Product.Views;

namespace QIQO.Business.Module.Product.Modules
{
    public class ProductModuleX : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion(RegionNames.ToolBarRegion, typeof(ProductNavigationViewX));
        }
        public void RegisterTypes(IContainerRegistry containerRegistry) {
            containerRegistry.Register(typeof(object), typeof(ProductViewX), typeof(ProductViewX).FullName);
        }
    }
}
