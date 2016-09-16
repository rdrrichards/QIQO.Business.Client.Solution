using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using QIQO.Business.Client.Core;
using QIQO.Business.Client.Core.Infrastructure;
using QIQO.Business.Client.Core.UI;
using QIQO.Business.Module.Orders.Views;
using QIQO.Common.Core.Logging;
using System.Windows.Media;

namespace QIQO.Business.Module.Orders.ViewModels
{
    public class OrderNavigationViewModelX : ViewModelBase, IRegionMemberLifetime
    {
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator event_aggregator;
        private bool _nav_is_checked = false;
        private int _instance_cnt = 0;
        private string module = ViewNames.OrderHomeView;

        public OrderNavigationViewModelX()
        {
            event_aggregator = Unity.Container.Resolve<IEventAggregator>();
            _regionManager = Unity.Container.Resolve<IRegionManager>();

            ShowOrderModuleCommand = new DelegateCommand(ShowOrderModule);
            event_aggregator.GetEvent<OrderLoadedEvent>().Subscribe(OnOrderHomeLoaded, ThreadOption.BackgroundThread);
            event_aggregator.GetEvent<NavigationEvent>().Subscribe(OnNavigationOccured, ThreadOption.BackgroundThread);
            event_aggregator.GetEvent<OpenOrderServiceEvent>().Subscribe(OnOpenOrderChangedEvent, ThreadOption.BackgroundThread);
        }

        private void OnOpenOrderChangedEvent(int open_order_cnt)
        {
            InstanceCount = open_order_cnt;
        }

        private void OnNavigationOccured(string module_name)
        {
            if (module_name != module) IsNavButtonChecked = false;
            OnPropertyChanged(nameof(DropShadowColor));
        }
        public DelegateCommand ShowOrderModuleCommand { get; set; }

        private void OnOrderHomeLoaded(string obj)
        {
            IsNavButtonChecked = true;
            OnPropertyChanged(nameof(DropShadowColor));
        }

        public bool KeepAlive { get; } = true;
        public bool IsNavButtonChecked
        {
            get { return _nav_is_checked; }
            set { SetProperty(ref _nav_is_checked, value); }
        }

        public int InstanceCount
        {
            get { return _instance_cnt; }
            set { SetProperty(ref _instance_cnt, value); }
        }

        public Color DropShadowColor => IsNavButtonChecked ? Colors.AntiqueWhite : Colors.Black;

        private void ShowOrderModule()
        {
            _regionManager.RequestNavigate(RegionNames.ContentRegion, typeof(OrderHomeView).FullName, NavigationComplete);
        }

        private void NavigationComplete(NavigationResult result)
        {
            IsNavButtonChecked = true;
            OnPropertyChanged(nameof(DropShadowColor));
            Log.Debug("From OrderNavigationViewModelX " + result.Context.Uri.ToString());
            event_aggregator.GetEvent<NavigationEvent>().Publish(module);
        }

    }
}
