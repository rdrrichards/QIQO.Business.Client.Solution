using CommonServiceLocator;
using Prism.Events;
using Prism.Regions;
using QIQO.Business.Client.Core.UI;

namespace QIQO.Business.Module.Orders.ViewModels
{
    public class OrderRibbonViewModel : ViewModelBase, IRegionMemberLifetime
    {
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator event_aggregator;

        public OrderRibbonViewModel()
        {
            event_aggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            _regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
        }

        public bool KeepAlive { get; } = false;
    }
}
