using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using CommonServiceLocator;
using QIQO.Business.Client.Core.UI;
using QIQO.Business.Client.Core;
using QIQO.Business.Client.Core.Infrastructure;
using QIQO.Business.Module.Account.Views;
using QIQO.Common.Core.Logging;
using QIQO.Business.Client.Entities;

namespace QIQO.Business.Module.Account.ViewModels
{
    class AccountRibbonViewModel : ViewModelBase, IRegionMemberLifetime
    {
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator event_aggregator;

        public AccountRibbonViewModel()
        {
            event_aggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            _regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();

            AccountNavigateCommand = new DelegateCommand<object>(DoNavigation);
        }

        public bool KeepAlive { get; } = false;

        public DelegateCommand<object> AccountNavigateCommand { get; set; }

        private void DoNavigation(object view)
        {
            var acct_type = (QIQOAccountType)view;
            var parameters = new NavigationParameters();
            parameters.Add("AccountType", acct_type);
            _regionManager.RequestNavigate(RegionNames.ContentRegion, typeof(AccountView).FullName, NavigationComplete, parameters);
            _regionManager.RequestNavigate(RegionNames.RibbonRegion, typeof(AccountRibbonView).FullName);
        }

        private void NavigationComplete(NavigationResult result)
        {
            Log.Debug(result.Context.Uri.ToString());
        }
    }
}
