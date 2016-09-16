using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using QIQO.Business.Client.Core;
using QIQO.Business.Client.Core.Infrastructure;
using QIQO.Business.Client.Core.UI;
using QIQO.Business.Module.Invoices.Views;
using QIQO.Common.Core.Logging;
using System.Windows.Media;
using System;

namespace QIQO.Business.Module.Invoices.ViewModels
{
    public class InvoiceNavigationViewModelX : ViewModelBase, IRegionMemberLifetime
    {
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator event_aggregator;
        private bool _nav_is_checked = false;
        private int _instance_cnt = 0;
        private string module = ViewNames.InvoiceHomeView;

        public InvoiceNavigationViewModelX()
        {
            event_aggregator = Unity.Container.Resolve<IEventAggregator>();
            _regionManager = Unity.Container.Resolve<IRegionManager>();

            ShowInvoiceModuleCommand = new DelegateCommand(ShowInvoiceModule);
            event_aggregator.GetEvent<InvoiceLoadedEvent>().Subscribe(OnInvoiceLoaded, ThreadOption.BackgroundThread);
            event_aggregator.GetEvent<NavigationEvent>().Subscribe(OnNavigationOccured, ThreadOption.BackgroundThread);
            event_aggregator.GetEvent<OpenInvoiceServiceEvent>().Subscribe(OnOpenInvoiceChangedEvent, ThreadOption.BackgroundThread);
        }

        private void OnOpenInvoiceChangedEvent(int open_invoice_cnt)
        {
            InstanceCount = open_invoice_cnt;
        }

        private void OnNavigationOccured(string module_name)
        {
            if (module_name != module) IsNavButtonChecked = false;
            OnPropertyChanged(nameof(DropShadowColor));
        }

        public DelegateCommand ShowInvoiceModuleCommand { get; set; }

        private void OnInvoiceLoaded(string obj)
        {
            IsNavButtonChecked = true;
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

        private void ShowInvoiceModule()
        {
            _regionManager.RequestNavigate(RegionNames.ContentRegion, typeof(InvoiceHomeView).FullName, NavigationComplete);
        }

        private void NavigationComplete(NavigationResult result)
        {
            IsNavButtonChecked = true;
            OnPropertyChanged(nameof(DropShadowColor));
            Log.Debug("From InvoiceNavigationViewModelX " + result.Context.Uri.ToString());
            event_aggregator.GetEvent<NavigationEvent>().Publish(module);
        }
    }
}
