using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using Microsoft.Practices.Unity;
using QIQO.Business.Client.Core.UI;
using QIQO.Business.Client.Core;

namespace QIQO.Business.Module.Orders.ViewModels
{
    public class OrderRibbonViewModel : ViewModelBase, IRegionMemberLifetime
    {
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator event_aggregator;

        public OrderRibbonViewModel()
        {
            event_aggregator = Unity.Container.Resolve<IEventAggregator>();
            _regionManager = Unity.Container.Resolve<IRegionManager>();
        }

        public bool KeepAlive { get; } = false;
    }
}
