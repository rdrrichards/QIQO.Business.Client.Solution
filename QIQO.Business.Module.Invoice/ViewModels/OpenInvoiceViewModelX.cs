using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using QIQO.Business.Client.Contracts;
using QIQO.Business.Client.Core;
using QIQO.Business.Client.Core.Infrastructure;
using QIQO.Business.Client.Core.UI;
using QIQO.Business.Client.Entities;
using QIQO.Business.Client.Wrappers;
using QIQO.Business.Module.General.Models;
using QIQO.Business.Module.Invoices.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace QIQO.Business.Module.Invoices.ViewModels
{
    public class OpenInvoiceViewModelX : ViewModelBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IServiceFactory _serviceFactory;
        private readonly IRegionManager _regionManager;

        private ObservableCollection<BusinessItem> _openInvoices = new ObservableCollection<BusinessItem>();
        private object _selectedItem;
        private string _headerMsg;
        private bool _isLoading;

        public OpenInvoiceViewModelX(IEventAggregator eventAggregator, IServiceFactory serviceFactory, IRegionManager regionManager)
        {
            _eventAggregator = eventAggregator;
            _serviceFactory = serviceFactory;
            _regionManager = regionManager;
            
            GetCompanyOpenInvoices();

            ChooseItemCommand = new DelegateCommand(OpenInvoice);
            RefreshCommand = new DelegateCommand(GetCompanyOpenInvoices);
        }

        private void OpenInvoice()
        {
            if (SelectedItem is BusinessItem busItem)
            {
                if (busItem.BusinessObject is Invoice invoice)
                {
                    var parameters = new NavigationParameters { { "InvoiceKey", invoice.InvoiceKey } };
                    _regionManager.RequestNavigate(RegionNames.ContentRegion, typeof(InvoiceViewX).FullName, parameters);
                }
            }
        }

        public DelegateCommand ChooseItemCommand { get; set; }
        public DelegateCommand RefreshCommand { get; set; }

        public int SelectedItemIndex { get; set; }
        public object SelectedItem
        {
            get { return _selectedItem; }
            set { SetProperty(ref _selectedItem, value); }
        }

        public ObservableCollection<BusinessItem> OpenInvoices
        {
            get { return _openInvoices; }
            private set { SetProperty(ref _openInvoices, value); }
        }

        public string HeaderMessage
        {
            get { return _headerMsg; }
            private set { SetProperty(ref _headerMsg, value); }
        }

        public bool IsLoading
        {
            get { return _isLoading; }
            set { SetProperty(ref _isLoading, value); }
        }

        private async void GetCompanyOpenInvoices()
        {
            HeaderMessage = "Open Invoices (Loading...)";
            IsLoading = true;
            IsBusy = true;

            var proxy = _serviceFactory.CreateClient<IInvoiceService>();
            var company = new Company() { CompanyKey = CurrentCompanyKey };

            using (proxy)
            {
                var invoices = proxy.GetInvoicesByCompanyAsync(company);
                await invoices;

                if (invoices.Result.Count > 0)
                {
                    foreach (var invoice in invoices.Result)
                        OpenInvoices.Add(Map(invoice));

                    SelectedItem = OpenInvoices[0];
                    SelectedItemIndex = 0;
                    HeaderMessage = "Open Invoices (" + OpenInvoices.Count.ToString() + ") X";
                }
                else
                {
                    HeaderMessage = "Open Invoices (0)";
                }
            }

            IsLoading = false;
            IsBusy = false;
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
