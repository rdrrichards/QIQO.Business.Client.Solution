using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using QIQO.Business.Client.Core;
using QIQO.Business.Client.Core.Infrastructure;
using QIQO.Business.Client.Core.UI;
using QIQO.Business.Client.Wrappers;
using QIQO.Business.Module.Invoices.Services;
using QIQO.Business.Module.Invoices.Views;
using System.Collections.ObjectModel;

namespace QIQO.Business.Module.Invoices.ViewModels
{
    public class WorkingInvoiceViewModel : ViewModelBase, IRegionMemberLifetime
    {
        private readonly IWorkingInvoiceService working_orders_service;
        private readonly IEventAggregator event_aggregator;
        private readonly IRegionManager _regionManager;
        private ObservableCollection<InvoiceWrapper> _working_orders;
        private object _selected_order;
        private string _header_msg = "Working Invoices";

        public WorkingInvoiceViewModel(IWorkingInvoiceService working_orders_svc, IEventAggregator event_aggtr, IRegionManager regionManager)
        {
            working_orders_service = working_orders_svc;
            event_aggregator = event_aggtr;
            _regionManager = regionManager;

            OpenInvoiceCommand = new DelegateCommand(OpenInvoice);
            InitWorkingInvoiceList();
            event_aggregator.GetEvent<OpenInvoiceServiceEvent>().Subscribe(OnOpenInvoiceChangedEvent, ThreadOption.BackgroundThread);
        }

        private void OnOpenInvoiceChangedEvent(int open_order_cnt)
        {
            InitWorkingInvoiceList();
            HeaderMessage = $"Working Invoices ({WorkingInvoices.Count})";
        }
        public bool IsLoading => false;

        private void InitWorkingInvoiceList()
        {
            WorkingInvoices = working_orders_service.GetWorkingInvoices();
        }

        public bool KeepAlive => false;
        public DelegateCommand OpenInvoiceCommand { get; set; }

        public ObservableCollection<InvoiceWrapper> WorkingInvoices
        {
            get { return _working_orders; }
            private set { SetProperty(ref _working_orders, value); }
        }

        public int SelectedInvoiceIndex { get; set; }
        public object SelectedInvoice
        {
            get { return _selected_order; }
            set { SetProperty(ref _selected_order, value); }
        }

        public string HeaderMessage
        {
            get { return _header_msg; }
            private set { SetProperty(ref _header_msg, value); }
        }
        private void OpenInvoice()
        {
            var selectedInvoice = SelectedInvoice as InvoiceWrapper;
            if (selectedInvoice != null)
            {
                var parameters = new NavigationParameters();
                parameters.Add("InvoiceNumber", selectedInvoice.InvoiceNumber);
                _regionManager.RequestNavigate(RegionNames.ContentRegion, typeof(InvoiceViewX).FullName, parameters);
            }
        }
    }
}
