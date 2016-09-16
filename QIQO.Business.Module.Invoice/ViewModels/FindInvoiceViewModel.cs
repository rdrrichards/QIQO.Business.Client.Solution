using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using Prism.Regions;
using QIQO.Business.Client.Contracts;
using QIQO.Business.Client.Core;
using QIQO.Business.Client.Core.Infrastructure;
using QIQO.Business.Client.Core.UI;
using QIQO.Business.Client.Entities;
using QIQO.Business.Module.Invoices.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace QIQO.Business.Module.Invoices.ViewModels
{
    public class FindInvoiceViewModel : ViewModelBase
    {
        IEventAggregator event_aggregator;
        IServiceFactory service_factory;
        private ObservableCollection<Invoice> _invoices = new ObservableCollection<Invoice>();
        private string _viewTitle = "Invoice Find";
        private string _search_term = "";
        private ItemSelectionNotification notification;
        private IRegionManager _regionManager;
        private string _button_text = "Find";
        private bool _button_enabled = true;
        private object _selected_order;
        private bool _is_searching;

        public object SelectedInvoice
        {
            get { return _selected_order; }
            set
            {
                SetProperty(ref _selected_order, value);
                ChooseInvoiceCommand.RaiseCanExecuteChanged();
                ChooseInvoiceCommandX.RaiseCanExecuteChanged();
            }
        }
        public int SelectedIndex { get; set; }

        public FindInvoiceViewModel()
        {
            event_aggregator = Unity.Container.Resolve<IEventAggregator>();
            service_factory = Unity.Container.Resolve<IServiceFactory>();
            _regionManager = Unity.Container.Resolve<IRegionManager>();

            BindCommands();
            event_aggregator.GetEvent<InvoiceLoadedEvent>().Publish(string.Empty);
        }
        public override string ViewTitle { get { return _viewTitle; } }
        public Action FinishInteraction { get; set; }
        public INotification Notification
        {
            get
            {
                return notification;
            }
            set
            {
                if (value is ItemSelectionNotification)
                {
                    notification = value as ItemSelectionNotification;
                    OnPropertyChanged(() => Notification);
                }
            }
        }

        public ObservableCollection<Invoice> Invoices
        {
            get { return _invoices; }
            private set { SetProperty(ref _invoices, value); }
        }

        public string SearchTerm
        {
            get { return _search_term; }
            set { SetProperty(ref _search_term, value); GetInvoicesCommand.RaiseCanExecuteChanged(); }
        }

        public string ButtonContent
        {
            get { return _button_text; }
            private set { SetProperty(ref _button_text, value); }
        }

        public bool IsLoading
        {
            get { return _is_searching; }
            private set { SetProperty(ref _is_searching, value); }
        }

        public bool ButtonEnabled
        {
            get { return _button_enabled; }
            private set { SetProperty(ref _button_enabled, value); }
        }

        public bool FoundSome => Invoices.Count > 0;
        public bool FoundSomeNo => Invoices.Count == 0;

        public DelegateCommand GetInvoicesCommand { get; set; }
        public DelegateCommand ChooseInvoiceCommand { get; set; }
        public DelegateCommand ChooseInvoiceCommandX { get; set; }

        private void BindCommands()
        {
            //CloseWindowCommand = new DelegateCommand(DoCancel);
            GetInvoicesCommand = new DelegateCommand(GetInvoices, CanGetInvoices);
            ChooseInvoiceCommand = new DelegateCommand(ChooseInvoice, CanChooseInvoice);
            ChooseInvoiceCommandX = new DelegateCommand(ChooseInvoiceX, CanChooseInvoice);
        }

        private bool CanGetInvoices()
        {
            return SearchTerm.Length > 0 && ButtonEnabled;
        }

        private bool CanChooseInvoice()
        {
            return SelectedInvoice != null;
        }

        private async void GetInvoices()
        {
            if (!string.IsNullOrWhiteSpace(SearchTerm))
            {
                MessageToDisplay = "Searching...";
                IsBusy = true;
                ButtonEnabled = false;
                IsLoading = true;
                GetInvoicesCommand.RaiseCanExecuteChanged();
                IInvoiceService order_service = service_factory.CreateClient<IInvoiceService>();

                using (order_service)
                {
                    try
                    {
                        Task<List<Invoice>> orders = order_service.FindInvoicesByCompanyAsync((Company)CurrentCompany, SearchTerm);
                        await orders;
                        Invoices = new ObservableCollection<Invoice>(orders.Result);
                    }
                    catch (Exception ex)
                    {
                        MessageToDisplay = ex.Message;
                        return;
                    }
                }

                MessageToDisplay = Invoices.Count.ToString() + " invoice(s) found";
                ButtonEnabled = true;
                GetInvoicesCommand.RaiseCanExecuteChanged();
                OnPropertyChanged(nameof(FoundSome));
                OnPropertyChanged(nameof(FoundSomeNo));
            }
            else
            {
                MessageToDisplay = "You must enter a search term in order to find an invoice";
            }
            IsLoading = false;
            IsBusy = false;
        }

        private void ChooseInvoice()
        {
            Invoice sel_acct = SelectedInvoice as Invoice;
            if (sel_acct != null)
            {
                //if (notification != null)
                //{
                //    notification.SelectedItem = sel_acct;
                //    notification.Confirmed = true;
                //}

                //FinishInteraction();
                var parameters = new NavigationParameters();
                parameters.Add("InvoiceKey", sel_acct.InvoiceKey);
                _regionManager.RequestNavigate(RegionNames.ContentRegion, typeof(InvoiceShellView).FullName);
                _regionManager.RequestNavigate(RegionNames.InvoicesRegion, typeof(InvoiceView).FullName, parameters);
                _regionManager.RequestNavigate(RegionNames.RibbonRegion, typeof(InvoiceRibbonView).FullName);
            }
        }

        private void ChooseInvoiceX()
        {
            Invoice sel_acct = SelectedInvoice as Invoice;
            if (sel_acct != null)
            {
                var parameters = new NavigationParameters();
                parameters.Add("InvoiceKey", sel_acct.InvoiceKey);
                _regionManager.RequestNavigate(RegionNames.ContentRegion, typeof(InvoiceViewX).FullName, parameters);
            }
        }
    }
}
