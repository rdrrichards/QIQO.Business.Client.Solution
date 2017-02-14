using System;
using Prism.Events;
using Prism.Regions;
using QIQO.Business.Client.Core;
using QIQO.Business.Client.Core.Infrastructure;
using QIQO.Business.Client.Core.UI;

namespace QIQO.Business.Module.Orders.ViewModels
{
    public class OrderNavigationViewModelX : NavigationViewModelBase
    {
        public OrderNavigationViewModelX(IEventAggregator evnt_aggr, IRegionManager rm) : base (evnt_aggr, rm)
        {
            Module = ViewNames.OrderHomeView;
            event_aggregator.GetEvent<OpenOrderServiceEvent>().Subscribe(OnOpenOrderChangedEvent, ThreadOption.BackgroundThread);
        }

        private void OnOpenOrderChangedEvent(int open_order_cnt)
        {
            InstanceCount = open_order_cnt;
        }
    }
}
