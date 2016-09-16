using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using QIQO.Business.Client.Contracts;
using QIQO.Business.Client.Core;
using QIQO.Business.Client.Core.Infrastructure;
using QIQO.Business.Client.Core.UI;
using QIQO.Business.Client.Entities;
using QIQO.Business.Client.Wrappers;
using QIQO.Business.Module.Invoices.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace QIQO.Business.Module.Invoices.ViewModels
{
    public class OpenInvoiceViewModelX : ViewModelBase
    {
        IEventAggregator event_aggregator;
        IServiceFactory service_factory;
        private ObservableCollection<InvoiceWrapper> _open_orders;
        private object _selected_order;
        private IRegionManager _regionManager;
        private string _header_msg;
        private bool _is_loading;

        public OpenInvoiceViewModelX(IEventAggregator event_aggtr, IServiceFactory service_fctry, IRegionManager regionManager)
        {
            // add code to this contructor to bring in dependencies and assign them to local variable
            event_aggregator = event_aggtr;
            service_factory = service_fctry;
            _regionManager = regionManager;

            //HeaderMessage = "Open Invoices (Loading...)";
            GetCompanyOpenInvoices();

            OpenInvoiceCommand = new DelegateCommand(OpenInvoice);
            RefreshCommand = new DelegateCommand(GetCompanyOpenInvoices);
            //ApplicationCommands.DashboardRefreshCommand.RegisterCommand(RefreshCommand);
        }

        private void OpenInvoice()
        {
            var selectedInvoice = SelectedInvoice  as InvoiceWrapper;
            if (selectedInvoice != null)
            {
                var parameters = new NavigationParameters();
                parameters.Add("InvoiceKey", selectedInvoice.InvoiceKey);
                _regionManager.RequestNavigate(RegionNames.ContentRegion, typeof(InvoiceViewX).FullName, parameters);
            }
        }

        public DelegateCommand OpenInvoiceCommand { get; set; }
        public DelegateCommand RefreshCommand { get; set; }

        public int SelectedInvoiceIndex { get; set; }
        public object SelectedInvoice
        {
            get { return _selected_order; }
            set { SetProperty(ref _selected_order, value); }
        }

        public ObservableCollection<InvoiceWrapper> OpenInvoices
        {
            get { return _open_orders; }
            private set { SetProperty(ref _open_orders, value); }
        }

        public string HeaderMessage
        {
            get { return _header_msg; }
            private set { SetProperty(ref _header_msg, value); }
        }

        public bool IsLoading
        {
            get { return _is_loading; }
            set { SetProperty(ref _is_loading, value); }
        }

        private async void GetCompanyOpenInvoices()
        {
            HeaderMessage = "Open Invoices (Loading...)";
            IsLoading = true;
            IsBusy = true;
            //ExecuteFaultHandledOperation(() =>
            //{
            IInvoiceService proxy = service_factory.CreateClient<IInvoiceService>();
            Company company = new Company() { CompanyKey = CurrentCompanyKey };
            ObservableCollection<InvoiceWrapper> open_order_col = new ObservableCollection<InvoiceWrapper>();

            using (proxy)
            {
                Task<List<Invoice>> orders = proxy.GetInvoicesByCompanyAsync(company);
                await orders;

                if (orders.Result.Count > 0)
                {
                    foreach (Invoice order in orders.Result)
                    {
                        InvoiceWrapper order_wrapper = new InvoiceWrapper(order);
                        open_order_col.Add(order_wrapper);
                    }
                    OpenInvoices = open_order_col;
                    SelectedInvoice = OpenInvoices[0];
                    SelectedInvoiceIndex = 0;
                    HeaderMessage = "Open Invoices (" + OpenInvoices.Count.ToString() + ")";
                }
                else
                {
                    OpenInvoices = new ObservableCollection<InvoiceWrapper>();
                    HeaderMessage = "Open Invoices (0)";
                }
            }
            //});
            //SetEventDatesContext();
            IsLoading = false;
            IsBusy = false;
        }

        //private void SetEventDatesContext()
        //{
        //    ObservableCollection<QIQODate> eds = new ObservableCollection<QIQODate>();

        //    foreach (InvoiceWrapper invoice in OpenInvoices)
        //    {
        //        QIQODate id = new QIQODate()
        //        {
        //            Date = invoice.InvoiceEntryDate.AddDays(15),
        //            EntityType = "Account",
        //            EntityName = invoice.Account.AccountName,
        //            BackgroundBrush = Brushes.LightPink,
        //            DateDescription = $"Due Date\nInvoice: {invoice.InvoiceNumber}",
        //            DateType = QIQODateType.InvoiceDueDate,
        //            FontWeight = FontWeights.Bold
        //        };

        //        eds.Add(id);
        //    }

        //    event_aggregator.GetEvent<CalendarContextChangedEvent>().Publish(eds);
        //}

        protected override void DisplayErrorMessage(Exception ex, [CallerMemberName] string methodName = "")
        {
            event_aggregator.GetEvent<GeneralErrorEvent>().Publish(methodName + " - " + ex.Message);
        }
    }
}
