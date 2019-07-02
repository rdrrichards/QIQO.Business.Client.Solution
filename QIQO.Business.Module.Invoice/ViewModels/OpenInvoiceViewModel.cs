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
using QIQO.Custom.Controls;
//using QIQO.Custom.Controls;
using System;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;

namespace QIQO.Business.Module.Invoices.ViewModels
{
    public class OpenInvoiceViewModel : ViewModelBase
    {
        readonly IEventAggregator event_aggregator;
        readonly IServiceFactory service_factory;
        private ObservableCollection<InvoiceWrapper> _open_orders;
        private object _selected_order;
        private readonly IRegionManager _regionManager;
        private string _header_msg;

        public OpenInvoiceViewModel(IEventAggregator event_aggtr, IServiceFactory service_fctry, IRegionManager regionManager)
        {
            // add code to this contructor to bring in dependencies and assign them to local variable
            event_aggregator = event_aggtr;
            service_factory = service_fctry;
            _regionManager = regionManager;

            //HeaderMessage = "Open Invoices (Loading...)";
            GetCompanyOpenInvoices();

            OpenInvoiceCommand = new DelegateCommand(OpenInvoice);
            RefreshCommand = new DelegateCommand(GetCompanyOpenInvoices);
            ApplicationCommands.DashboardRefreshCommand.RegisterCommand(RefreshCommand);
        }

        private void OpenInvoice()
        {
            var selectedInvoice = SelectedInvoice as InvoiceWrapper;
            //MessageBox.Show("The order that you double-clicked on is: " + selectedInvoice.InvoiceNumber);
            //*** NEEDS WORK!
            if (selectedInvoice != null)
            {
                var parameters = new NavigationParameters();
                parameters.Add("InvoiceKey", selectedInvoice.InvoiceKey);
                _regionManager.RequestNavigate(RegionNames.ContentRegion, typeof(InvoiceShellView).FullName);
                _regionManager.RequestNavigate(RegionNames.InvoicesRegion, typeof(InvoiceView).FullName, parameters);
                _regionManager.RequestNavigate(RegionNames.RibbonRegion, typeof(InvoiceRibbonView).FullName);
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

        private async void GetCompanyOpenInvoices()
        {
            HeaderMessage = "Open Invoices (Loading...)";
            //ExecuteFaultHandledOperation(() =>
            //{
            var proxy = service_factory.CreateClient<IInvoiceService>();
            var company = new Company() { CompanyKey = CurrentCompanyKey };
            var open_order_col = new ObservableCollection<InvoiceWrapper>();

            using (proxy)
            {
                var orders = proxy.GetInvoicesByCompanyAsync(company);
                await orders;

                if (orders.Result.Count > 0)
                {
                    foreach (var order in orders.Result)
                    {
                        var order_wrapper = new InvoiceWrapper(order);
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
        }

        private void SetEventDatesContext()
        {
            var eds = new ObservableCollection<QIQODate>();

            foreach (var invoice in OpenInvoices)
            {
                var id = new QIQODate()
                {
                    Date = invoice.InvoiceEntryDate.AddDays(15),
                    EntityType = "Account",
                    EntityName = invoice.Account.AccountName,
                    BackgroundBrush = Brushes.LightPink,
                    DateDescription = $"Due Date\nInvoice: {invoice.InvoiceNumber}",
                    DateType = QIQODateType.InvoiceDueDate,
                    FontWeight = FontWeights.Bold
                };

                eds.Add(id);
            }

            event_aggregator.GetEvent<CalendarContextChangedEvent>().Publish(eds);
        }

        protected override void DisplayErrorMessage(Exception ex, [CallerMemberName] string methodName = "")
        {
            event_aggregator.GetEvent<GeneralErrorEvent>().Publish(methodName + " - " + ex.Message);
        }
    }
}
