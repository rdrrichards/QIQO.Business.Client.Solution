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
using QIQO.Business.Module.General.Models;
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

        private ObservableCollection<BusinessItem> _invoices = new ObservableCollection<BusinessItem>();
        private string _viewTitle = "Invoice Find";
        private string _searchTerm = "";
        private ItemSelectionNotification notification;
        private string _buttonText = "Find";
        private bool _buttonEnabled = true;
        private object _selectedItem;
        private bool _isSearching;

        public object SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                SetProperty(ref _selectedItem, value);
                ChooseItemCommand.RaiseCanExecuteChanged();
                ChooseItemCommandX.RaiseCanExecuteChanged();
            }
        }
        public int SelectedItemIndex { get; set; }

        public FindInvoiceViewModel()
        {
            _eventAggregator = Unity.Container.Resolve<IEventAggregator>();
            _serviceFactory = Unity.Container.Resolve<IServiceFactory>();
            _regionManager = Unity.Container.Resolve<IRegionManager>();

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

        public ObservableCollection<BusinessItem> FoundItems
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

        public bool FoundSome => FoundItems.Count > 0;
        public bool FoundSomeNo => FoundItems.Count == 0;

        public DelegateCommand GetInvoicesCommand { get; set; }
        public DelegateCommand SearchCommand { get; set; }
        public DelegateCommand ChooseItemCommand { get; set; }
        public DelegateCommand ChooseItemCommandX { get; set; }

        private void BindCommands()
        {
            SearchCommand = new DelegateCommand(GetInvoices, CanGetInvoices);
            GetInvoicesCommand = new DelegateCommand(GetInvoices, CanGetInvoices);
            ChooseItemCommand = new DelegateCommand(ChooseInvoiceX, CanChooseInvoice);
            ChooseItemCommandX = new DelegateCommand(ChooseInvoiceX, CanChooseInvoice);
        }

        private bool CanGetInvoices()
        {
            return SearchTerm.Length > 0 && ButtonEnabled;
        }

        private bool CanChooseInvoice()
        {
            return SelectedItem != null;
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
                var proxy = _serviceFactory.CreateClient<IInvoiceService>();

                using (proxy)
                {
                    var invoices = proxy.FindInvoicesByCompanyAsync((Company)CurrentCompany, SearchTerm);
                    await invoices;
                    // FoundItems = new ObservableCollection<Invoice>(orders.Result);

                    if (invoices.Result.Count > 0)
                    {
                        foreach (var invoice in invoices.Result)
                            FoundItems.Add(Map(invoice));

                        SelectedItem = FoundItems[0];
                        SelectedItemIndex = 0;
                    }
                }

                MessageToDisplay = FoundItems.Count.ToString() + " invoice(s) found";
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
            if (SelectedItem is Invoice sel_acct)
            {
                var parameters = new NavigationParameters();
                parameters.Add("InvoiceKey", sel_acct.InvoiceKey);
                _regionManager.RequestNavigate(RegionNames.ContentRegion, typeof(InvoiceShellView).FullName);
                _regionManager.RequestNavigate(RegionNames.InvoicesRegion, typeof(InvoiceView).FullName, parameters);
                _regionManager.RequestNavigate(RegionNames.RibbonRegion, typeof(InvoiceRibbonView).FullName);
            }
        }

        private void ChooseInvoiceX()
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
