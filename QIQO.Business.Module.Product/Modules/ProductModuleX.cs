using Microsoft.Practices.Unity;
using Prism.Regions;
using QIQO.Business.Client.Core;
using QIQO.Business.Client.Core.Infrastructure;
using QIQO.Business.Module.Product.Views;

namespace QIQO.Business.Module.Product.Modules
{
    public class ProductModuleX : ModuleBase
    {
        public ProductModuleX(IUnityContainer container, IRegionManager region_manager) : base(container, region_manager)
        {
        }
        public override void Initialize()
        {
            RegionManager.RegisterViewWithRegion(RegionNames.ToolBarRegion, typeof(ProductNavigationViewX));
            //RegionManager.RegisterViewWithRegion(RegionNames.RibbonRegion, typeof(ProductRibbonView));

            UnityContainer.RegisterType(typeof(object), typeof(ProductViewX), typeof(ProductViewX).FullName);
            //UnityContainer.RegisterType(typeof(object), typeof(ProductRibbonView), typeof(ProductRibbonView).FullName);
        }
    }
}
