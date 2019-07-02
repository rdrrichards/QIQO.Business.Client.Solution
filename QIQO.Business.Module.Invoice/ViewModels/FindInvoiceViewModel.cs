using CommonServiceLocator;
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
using System.Collections.ObjectModel;

namespace QIQO.Business.Module.Invoices.ViewModels
{
    public class FindInvoiceViewModel : ViewModelBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IServiceFactory _serviceFactory;
        private readonly IRegionManager _regionManager;

        private ObservableCollection<Invoice> _invoices = new ObservableCollection<Invoice>();
        private readonly string _viewTitle = "Invoice Find";
        private string _searchTerm = "";
        private ItemSelectionNotification notification;
        private string _buttonText = "Find";
        private bool _buttonEnabled = true;
        private object _selectedItem;
        private bool _isSearching;

        public object SelectedInvoice
        {
            get { return _selectedItem; }
            set
            {
                SetProperty(ref _selectedItem, value);
                ChooseInvoiceCommand.RaiseCanExecuteChanged();
            }
        }
        public int SelectedIndex { get; set; }

        public FindInvoiceViewModel()
        {
            _eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            _serviceFactory = ServiceLocator.Current.GetInstance<IServiceFactory>();
            _regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();

            BindCommands();
            _eventAggregator.GetEvent<InvoiceLoadedEvent>().Publish(string.Empty);
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
                    RaisePropertyChanged(nameof(Notification));
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
            get { return _searchTerm; }
            set { SetProperty(ref _searchTerm, value); GetInvoicesCommand.RaiseCanExecuteChanged(); }
        }

        public string ButtonContent
        {
            get { return _buttonText; }
            private set { SetProperty(ref _buttonText, value); }
        }

        public bool IsLoading
        {
            get { return _isSearching; }
            private set { SetProperty(ref _isSearching, value); }
        }

        public bool ButtonEnabled
        {
            get { return _buttonEnabled; }
            private set { SetProperty(ref _buttonEnabled, value); }
        }

        public bool FoundSome => Invoices.Count > 0;
        public bool FoundSomeNo => Invoices.Count == 0;

        public DelegateCommand GetInvoicesCommand { get; set; }
        public DelegateCommand ChooseInvoiceCommand { get; set; }

        private void BindCommands()
        {
            GetInvoicesCommand = new DelegateCommand(GetInvoices, CanGetInvoices);
            ChooseInvoiceCommand = new DelegateCommand(ChooseInvoice, CanChooseInvoice);
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
                var order_service = _serviceFactory.CreateClient<IInvoiceService>();

                using (order_service)
                {
                    try
                    {
                        var orders = order_service.FindInvoicesByCompanyAsync((Company)CurrentCompany, SearchTerm);
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
                RaisePropertyChanged(nameof(FoundSome));
                RaisePropertyChanged(nameof(FoundSomeNo));
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
            if (SelectedInvoice is Invoice sel_acct)
            {
                var parameters = new NavigationParameters();
                parameters.Add("InvoiceKey", sel_acct.InvoiceKey);
                _regionManager.RequestNavigate(RegionNames.ContentRegion, typeof(InvoiceShellView).FullName);
                _regionManager.RequestNavigate(RegionNames.InvoicesRegion, typeof(InvoiceView).FullName, parameters);
                _regionManager.RequestNavigate(RegionNames.RibbonRegion, typeof(InvoiceRibbonView).FullName);
            }
        }
    }
}
