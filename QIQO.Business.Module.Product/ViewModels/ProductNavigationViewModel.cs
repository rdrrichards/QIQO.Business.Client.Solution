using CommonServiceLocator;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using QIQO.Business.Client.Core;
using QIQO.Business.Client.Core.Infrastructure;
using QIQO.Business.Client.Core.UI;
using QIQO.Business.Module.Product.Views;
using QIQO.Common.Core.Logging;

namespace QIQO.Business.Module.Product.ViewModels
{
    class ProductNavigationViewModel : ViewModelBase
    {
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator event_aggregator;
        private bool _nav_is_checked;

        public ProductNavigationViewModel()
        {
            event_aggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            _regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();

            ShowProductModuleCommand = new DelegateCommand(ShowProductModule);
            event_aggregator.GetEvent<ProductLoadedEvent>().Subscribe(OnFormLoaded, ThreadOption.BackgroundThread);
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

        private void ShowProductModule()
        {
            var parameters = new NavigationParameters();
            _regionManager.RequestNavigate(RegionNames.RibbonRegion, typeof(ProductRibbonView).FullName);
            _regionManager.RequestNavigate(RegionNames.ContentRegion, typeof(ProductView).FullName, NavigationComplete, parameters);
        }

        private void NavigationComplete(NavigationResult result)
        {
            Log.Debug(result.Context.Uri.ToString());
            //Log.Error(result.Error.Message);
            Log.Debug(result.Context.NavigationService.ToString());
        }

        public DelegateCommand ShowProductModuleCommand { get; set; }
    }
}
