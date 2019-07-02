using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using QIQO.Business.Client.Core.Infrastructure;
using QIQO.Common.Core.Logging;
using System.Windows.Media;

namespace QIQO.Business.Client.Core.UI
{
    public class NavigationViewModelBase : ViewModelBase, IRegionMemberLifetime
    {
        protected readonly IRegionManager _regionManager;
        protected readonly IEventAggregator event_aggregator;
        protected bool _nav_is_checked = false;
        protected int _instance_cnt = 0;

        public NavigationViewModelBase(IEventAggregator evnt_aggr, IRegionManager rm)
        {
            event_aggregator = evnt_aggr;
            _regionManager = rm;
            event_aggregator.GetEvent<NavigationEvent>().Subscribe(OnNavigationOccured, ThreadOption.BackgroundThread);
            ShowModuleCommand = new DelegateCommand(ShowModule);
        }
        protected void OnNavigationOccured(string module_name)
        {
            if (module_name != Module)
            {
                IsNavButtonChecked = false;
            }

            RaisePropertyChanged(nameof(DropShadowColor));
        }

        protected virtual void ShowModule()
        {
            _regionManager.RequestNavigate(RegionNames.ContentRegion, Module, NavigationComplete);
        }
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

        public string Module { get; set; }

        public Color DropShadowColor => IsNavButtonChecked ? Colors.AntiqueWhite : Colors.Black;
        public DelegateCommand ShowModuleCommand { get; set; }

        protected virtual void NavigationComplete(NavigationResult result)
        {
            IsNavButtonChecked = true;
            RaisePropertyChanged(nameof(DropShadowColor));
            Log.Debug($"Navigating to {result.Context.Uri.ToString()}");
            event_aggregator.GetEvent<NavigationEvent>().Publish(Module);
        }
        public bool KeepAlive => true;
    }
}