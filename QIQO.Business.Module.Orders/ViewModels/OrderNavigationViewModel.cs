using CommonServiceLocator;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using QIQO.Business.Client.Core;
using QIQO.Business.Client.Core.Infrastructure;
using QIQO.Business.Client.Core.UI;
using QIQO.Business.Module.Orders.Views;
using QIQO.Common.Core.Logging;

namespace QIQO.Business.Module.Orders.ViewModels
{
    class OrderNavigationViewModel : ViewModelBase, IRegionMemberLifetime
    {
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator event_aggregator;
        private bool _nav_is_checked;

        public OrderNavigationViewModel()
        {
            event_aggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            _regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();

            ShowOrderModuleCommand = new DelegateCommand(ShowOrderModule);
            event_aggregator.GetEvent<OrderLoadedEvent>().Subscribe(OnOrderLoaded, ThreadOption.BackgroundThread);
        }

        private void OnOrderLoaded(string obj)
        {
            IsNavButtonChecked = true;
        }

        public bool KeepAlive { get; } = false;
        public bool IsNavButtonChecked
        {
            get { return _nav_is_checked; }
            set { SetProperty(ref _nav_is_checked, value); }
        }

        private void ShowOrderModule()
        {
            //var parameters = new NavigationParameters();
            _regionManager.RequestNavigate(RegionNames.RibbonRegion, typeof(OrderRibbonView).FullName);
            _regionManager.RequestNavigate(RegionNames.ContentRegion, typeof(OrderShellView).FullName, NavigationComplete);
            _regionManager.RequestNavigate(RegionNames.OrdersRegion, typeof(OrderView).FullName, NavigationComplete);
        }

        private void NavigationComplete(NavigationResult result)
        {
            Log.Debug("From OrderNavigationViewModel " + result.Context.Uri.ToString());
        }

        public DelegateCommand ShowOrderModuleCommand { get; set; }
    }
}
