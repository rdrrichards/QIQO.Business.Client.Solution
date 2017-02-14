using Prism.Events;
using Prism.Regions;
using QIQO.Business.Client.Core.Infrastructure;
using QIQO.Business.Client.Core.UI;

namespace QIQO.Business.Module.Orders.ViewModels
{
    public class OrderNavigationViewModelX : NavigationViewModelBase
    {
        public OrderNavigationViewModelX(IEventAggregator evnt_aggr, IRegionManager rm) : base (evnt_aggr, rm)
        {
            Module = ViewNames.OrderHomeView;
        }
    }
}
