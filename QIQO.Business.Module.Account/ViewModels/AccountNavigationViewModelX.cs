using Prism.Events;
using Prism.Regions;
using QIQO.Business.Client.Core;
using QIQO.Business.Client.Core.Infrastructure;
using QIQO.Business.Client.Core.UI;

namespace QIQO.Business.Module.Account.ViewModels
{
    public class AccountNavigationViewModelX : NavigationViewModelBase
    {
        public AccountNavigationViewModelX(IEventAggregator evnt_aggr, IRegionManager rm) : base(evnt_aggr, rm)
        {
            Module = ViewNames.AccountHomeView;
            event_aggregator.GetEvent<OpenAccountServiceEvent>().Subscribe(OnOpenAccountChangedEvent, ThreadOption.BackgroundThread);
        }

        private void OnOpenAccountChangedEvent(int open_order_cnt)
        {
            InstanceCount = open_order_cnt;
        }
    }
}