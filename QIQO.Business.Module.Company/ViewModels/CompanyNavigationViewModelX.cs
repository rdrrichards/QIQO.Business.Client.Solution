using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using QIQO.Business.Client.Core;
using QIQO.Business.Client.Core.Infrastructure;
using QIQO.Business.Client.Core.UI;
using QIQO.Business.Module.Company.Views;
using QIQO.Common.Core.Logging;
using System.Windows.Media;

namespace QIQO.Business.Module.Company.ViewModels
{
    class CompanyNavigationViewModelX : ViewModelBase, IRegionMemberLifetime
    {
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator event_aggregator;
        private bool _nav_is_checked;
        private string module = ViewNames.CompanyHomeView;

        public CompanyNavigationViewModelX()
        {
            event_aggregator = Unity.Container.Resolve<IEventAggregator>();
            _regionManager = Unity.Container.Resolve<IRegionManager>();

            ShowCompanyModuleCommand = new DelegateCommand(ShowCompanyModule);
            event_aggregator.GetEvent<CompanyLoadedEvent>().Subscribe(OnFormLoaded, ThreadOption.BackgroundThread);
            event_aggregator.GetEvent<NavigationEvent>().Subscribe(OnNavigationOccured, ThreadOption.BackgroundThread);
        }

        private void OnNavigationOccured(string module_name)
        {
            if (module_name != module) IsNavButtonChecked = false;
            OnPropertyChanged(nameof(DropShadowColor));
        }

        public DelegateCommand ShowCompanyModuleCommand { get; set; }

        public bool KeepAlive { get; } = true;
        private void OnFormLoaded(string obj)
        {
            IsNavButtonChecked = true;
        }
        public bool IsNavButtonChecked
        {
            get { return _nav_is_checked; }
            set { SetProperty(ref _nav_is_checked, value); }
        }

        public int InstanceCount => 0;
        public Color DropShadowColor => IsNavButtonChecked ? Colors.AntiqueWhite : Colors.Black;

        private void ShowCompanyModule()
        {
            _regionManager.RequestNavigate(RegionNames.ContentRegion, typeof(CompanyView).FullName, NavigationComplete);
        }

        private void NavigationComplete(NavigationResult result)
        {
            IsNavButtonChecked = true;
            OnPropertyChanged(nameof(DropShadowColor));
            Log.Debug("From CompanyNavigationViewModelX " + result.Context.Uri.ToString());
            event_aggregator.GetEvent<NavigationEvent>().Publish(module);
        }
    }
}
