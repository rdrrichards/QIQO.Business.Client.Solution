using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using QIQO.Business.Client.Core;
using QIQO.Business.Client.Core.Infrastructure;
using QIQO.Business.Client.Core.UI;
using QIQO.Business.Client.Entities;
using QIQO.Business.Module.General.Models;
using QIQO.Business.Module.Invoices.Services;
using QIQO.Business.Module.Invoices.Views;
using System.Collections.ObjectModel;

namespace QIQO.Business.Module.Invoices.ViewModels
{
    public class WorkingInvoiceViewModel : ViewModelBase, IRegionMemberLifetime
    {
        private readonly IWorkingInvoiceService _workingOrdersService;
        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManager;
        private ObservableCollection<BusinessItem> _workingInvoices = new ObservableCollection<BusinessItem>();
        private object _selected_order;
        private string _header_msg = "Working Invoices";

        public WorkingInvoiceViewModel(IWorkingInvoiceService workingOrdersService, IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            _workingOrdersService = workingOrdersService;
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;

            ChooseItemCommand = new DelegateCommand(OpenInvoice);
            InitWorkingInvoiceList();
            _eventAggregator.GetEvent<OpenInvoiceServiceEvent>().Subscribe(OnOpenInvoiceChangedEvent, ThreadOption.UIThread);
        }

        private void OnOpenInvoiceChangedEvent(int open_order_cnt)
        {
            InitWorkingInvoiceList();
            HeaderMessage = $"Working Invoices ({WorkingInvoices.Count})";
        }
        public bool IsLoading => false;

        private void InitWorkingInvoiceList()
        {
            var workingInvoices = _workingOrdersService.GetWorkingInvoices();
            WorkingInvoices.Clear();
            foreach (var wi in workingInvoices)
            {
                WorkingInvoices.Add(Map(wi.Model));
            }
        }

        public bool KeepAlive => false;
        public DelegateCommand ChooseItemCommand { get; set; }

        public ObservableCollection<BusinessItem> WorkingInvoices
        {
            get { return _workingInvoices; }
            private set { SetProperty(ref _workingInvoices, value); }
        }

        public int SelectedInvoiceIndex { get; set; }
        public object SelectedItem
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
            if (SelectedItem is BusinessItem busItem)
            {
                if (busItem.BusinessObject is Invoice invoiceToEdit)
                {
                    var parameters = new NavigationParameters { { "InvoiceNumber", invoiceToEdit.InvoiceNumber } };
                    _regionManager.RequestNavigate(RegionNames.ContentRegion, typeof(InvoiceViewX).FullName, parameters);
                }
            }
        }

        private BusinessItem Map(Invoice order)
        {
            return new BusinessItem
            {
                ItemId = order.InvoiceNumber,
                ItemCode = order.Account.AccountCode,
                ItemName = order.Account.AccountName,
                ItemEntryDate = order.OrderEntryDate,
                ItemStatus = order.InvoiceStatus.ToString(),
                ItemStatusDate = order.InvoiceStatusDate,
                BusinessObject = order,
                Total = (double)order.InvoiceValueSum,
                Quantity = order.InvoiceItemCount
            };
        }
    }
}
