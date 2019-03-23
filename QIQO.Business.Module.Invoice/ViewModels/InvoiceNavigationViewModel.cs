using CommonServiceLocator;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using QIQO.Business.Client.Core;
using QIQO.Business.Client.Core.Infrastructure;
using QIQO.Business.Client.Core.UI;
using QIQO.Business.Module.Invoices.Views;
using QIQO.Common.Core.Logging;

namespace QIQO.Business.Module.Invoices.ViewModels
{
    public class InvoiceNavigationViewModel : ViewModelBase, IRegionMemberLifetime
    {
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator event_aggregator;
        private bool _nav_is_checked;

        public InvoiceNavigationViewModel()
        {
            event_aggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            _regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();

            ShowInvoiceModuleCommand = new DelegateCommand(ShowInvoiceModule);
            event_aggregator.GetEvent<InvoiceLoadedEvent>().Subscribe(OnInvoiceLoaded, ThreadOption.BackgroundThread);
        }

        private void OnInvoiceLoaded(string obj)
        {
            IsNavButtonChecked = true;
        }

        public bool KeepAlive { get; } = false;
        public bool IsNavButtonChecked
        {
            get { return _nav_is_checked; }
            set { SetProperty(ref _nav_is_checked, value); }
        }

        private void ShowInvoiceModule()
        {
            //var parameters = new NavigationParameters();
            _regionManager.RequestNavigate(RegionNames.RibbonRegion, typeof(InvoiceRibbonView).FullName);
            _regionManager.RequestNavigate(RegionNames.ContentRegion, typeof(InvoiceShellView).FullName, NavigationComplete);
            _regionManager.RequestNavigate(RegionNames.InvoicesRegion, typeof(InvoiceView).FullName, NavigationComplete);
        }

        private void NavigationComplete(NavigationResult result)
        {
            Log.Debug("From OrderNavigationViewModel " + result.Context.Uri.ToString());
        }

        public DelegateCommand ShowInvoiceModuleCommand { get; set; }
    }
}
