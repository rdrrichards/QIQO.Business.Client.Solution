using Prism.Events;
using Prism.Regions;
using QIQO.Business.Client.Core;
using QIQO.Business.Client.Core.Infrastructure;
using QIQO.Business.Client.Core.UI;

namespace QIQO.Business.Module.Product.ViewModels
{
    public class ProductNavigationViewModelX : NavigationViewModelBase
    {
        public ProductNavigationViewModelX(IEventAggregator evnt_aggr, IRegionManager rm) : base(evnt_aggr, rm)
        {
            Module = ViewNames.ProductViewX;
            event_aggregator.GetEvent<ProductNewProductAddEvent>().Subscribe(NewProductAdded, ThreadOption.BackgroundThread);
            event_aggregator.GetEvent<ProductNewProductCancelEvent>().Subscribe(NewProductCanceled, ThreadOption.BackgroundThread);
            event_aggregator.GetEvent<ProductNewProductCompleteEvent>().Subscribe(NewProductCanceled, ThreadOption.BackgroundThread);
        }

        private void NewProductCanceled(string module_name)
        {
            if (module_name == Module)
            {
                InstanceCount--;
            }
        }

        private void NewProductAdded(string module_name)
        {
            if (module_name == Module)
            {
                InstanceCount++;
            }
        }
    }
}