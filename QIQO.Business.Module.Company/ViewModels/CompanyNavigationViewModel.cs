using CommonServiceLocator;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using QIQO.Business.Client.Core;
using QIQO.Business.Client.Core.Infrastructure;
using QIQO.Business.Client.Core.UI;
using QIQO.Business.Module.Company.Views;
using QIQO.Common.Core.Logging;


namespace QIQO.Business.Module.Company.ViewModels
{
    class CompanyNavigationViewModel : ViewModelBase
    {
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator event_aggregator;
        private bool _nav_is_checked;

        public CompanyNavigationViewModel()
        {
            event_aggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            _regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();

            ShowCompanyModuleCommand = new DelegateCommand(ShowCompanyModule);
            event_aggregator.GetEvent<CompanyLoadedEvent>().Subscribe(OnFormLoaded, ThreadOption.BackgroundThread);
        }

        private void OnFormLoaded(string obj)
        {
            IsNavButtonChecked = true;
        }
        public bool IsNavButtonChecked
        {
            get { return _nav_is_checked; }
            set { SetProperty(ref _nav_is_checked, value); }
        }

        private void ShowCompanyModule()
        {
            var parameters = new NavigationParameters();
            _regionManager.RequestNavigate(RegionNames.RibbonRegion, typeof(CompanyRibbonView).FullName);
            _regionManager.RequestNavigate(RegionNames.ContentRegion, typeof(CompanyView).FullName, NavigationComplete, parameters);
        }

        private void NavigationComplete(NavigationResult result)
        {
            Log.Debug(result.Context.Uri.ToString());
            //Log.Error(result.Error.Message);
            Log.Debug(result.Context.NavigationService.ToString());
        }

        public DelegateCommand ShowCompanyModuleCommand { get; set; }
    }
}
