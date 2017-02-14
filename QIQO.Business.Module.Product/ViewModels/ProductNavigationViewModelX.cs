using Prism.Events;
using Prism.Regions;
using QIQO.Business.Client.Core.Infrastructure;
using QIQO.Business.Client.Core.UI;

namespace QIQO.Business.Module.Product.ViewModels
{
    public class ProductNavigationViewModelX : NavigationViewModelBase
    {
        public ProductNavigationViewModelX(IEventAggregator evnt_aggr, IRegionManager rm) : base (evnt_aggr, rm)
        {
            Module = ViewNames.ProductViewX;
        }
    }
}