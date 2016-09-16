using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using Microsoft.Practices.Unity;
using QIQO.Business.Client.Core.UI;
using QIQO.Business.Client.Core;
using QIQO.Business.Client.Core.Infrastructure;
using QIQO.Common.Core.Logging;
using QIQO.Business.Module.Dashboard.Views;
using System.Windows.Media;

namespace QIQO.Business.Module.Dashboard.ViewModels
{
    class DashboardNavigationViewModelX : ViewModelBase, IRegionMemberLifetime
    {
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator event_aggregator;
        private bool _nav_is_checked = true;
        private int _instance_cnt = 0;
        private string module = ViewNames.DashboardHomeView;

        public DashboardNavigationViewModelX()
        {
            event_aggregator = Unity.Container.Resolve<IEventAggregator>();
            _regionManager = Unity.Container.Resolve<IRegionManager>();

            ShowDashboardModuleCommand = new DelegateCommand(DoNavigate);
            event_aggregator.GetEvent<NavigationEvent>().Subscribe(OnNavigationOccured, ThreadOption.BackgroundThread);
        }

        private void OnNavigationOccured(string module_name)
        {
            if (module_name != module) IsNavButtonChecked = false;
            OnPropertyChanged(nameof(DropShadowColor));
        }

        public DelegateCommand ShowDashboardModuleCommand { get; set; } // ShowDashboardModuleCommand

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

        private void DoNavigate()
        {
            _regionManager.RequestNavigate(RegionNames.ContentRegion, typeof(DashboardViewX).FullName, NavigationComplete);
        }

        private void NavigationComplete(NavigationResult result)
        {
            IsNavButtonChecked = true;
            OnPropertyChanged(nameof(DropShadowColor));
            Log.Debug("From DashboardNavigationViewModelX " + result.Context.Uri.ToString());
            event_aggregator.GetEvent<NavigationEvent>().Publish(module);
        }
    }
}
