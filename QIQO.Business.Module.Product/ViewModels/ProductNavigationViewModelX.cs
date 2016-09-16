using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using QIQO.Business.Client.Core;
using QIQO.Business.Client.Core.Infrastructure;
using QIQO.Business.Client.Core.UI;
using QIQO.Business.Module.Product.Views;
using QIQO.Common.Core.Logging;
using System.Windows.Media;
using System;

namespace QIQO.Business.Module.Product.ViewModels
{
    class ProductNavigationViewModelX : ViewModelBase, IRegionMemberLifetime
    {
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator event_aggregator;
        private bool _nav_is_checked = false;
        private int _instance_cnt = 0;
        private string module = ViewNames.ProductHomeView;

        public ProductNavigationViewModelX()
        {
            event_aggregator = Unity.Container.Resolve<IEventAggregator>();
            _regionManager = Unity.Container.Resolve<IRegionManager>();

            ShowProductModuleCommand = new DelegateCommand(ShowProductModule);

            event_aggregator.GetEvent<ProductLoadedEvent>().Subscribe(OnFormLoaded, ThreadOption.BackgroundThread);
            event_aggregator.GetEvent<NavigationEvent>().Subscribe(OnNavigationOccured, ThreadOption.BackgroundThread);
            event_aggregator.GetEvent<ProductNewProductAddEvent>().Subscribe(NewProductAdded, ThreadOption.BackgroundThread);
            event_aggregator.GetEvent<ProductNewProductCancelEvent>().Subscribe(NewProductCanceled, ThreadOption.BackgroundThread);
            event_aggregator.GetEvent<ProductNewProductCompleteEvent>().Subscribe(NewProductCanceled, ThreadOption.BackgroundThread);
        }

        private void NewProductCanceled(string module_name)
        {
            if (module_name == module) InstanceCount--;
        }

        private void NewProductAdded(string module_name)
        {
            if (module_name == module) InstanceCount++;
        }

        private void OnNavigationOccured(string module_name)
        {
            if (module_name != module) IsNavButtonChecked = false;
            OnPropertyChanged(nameof(DropShadowColor));
        }

        public DelegateCommand ShowProductModuleCommand { get; set; }
        
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

        public int InstanceCount
        {
            get { return _instance_cnt; }
            set { SetProperty(ref _instance_cnt, value); }
        }

        public Color DropShadowColor => IsNavButtonChecked ? Colors.AntiqueWhite : Colors.Black;

        private void ShowProductModule()
        {
            _regionManager.RequestNavigate(RegionNames.ContentRegion, typeof(ProductViewX).FullName, NavigationComplete);
        }

        private void NavigationComplete(NavigationResult result)
        {
            IsNavButtonChecked = true;
            OnPropertyChanged(nameof(DropShadowColor));
            Log.Debug("From ProductNavigationViewModelX " + result.Context.Uri.ToString());
            event_aggregator.GetEvent<NavigationEvent>().Publish(module);
        }
    }
}
