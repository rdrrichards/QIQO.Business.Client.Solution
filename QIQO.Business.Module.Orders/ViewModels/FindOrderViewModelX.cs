using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using QIQO.Business.Client.Contracts;
using QIQO.Business.Client.Core;
using QIQO.Business.Client.Core.Infrastructure;
using QIQO.Business.Client.Core.UI;
using QIQO.Business.Client.Entities;
using QIQO.Business.Module.General.Models;
using QIQO.Business.Module.Orders.Views;
using System.Collections.ObjectModel;

namespace QIQO.Business.Module.Orders.ViewModels
{
    public class FindOrderViewModelX : ViewModelBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IServiceFactory _serviceFactory;
        private readonly IRegionManager _regionManager;

        private ObservableCollection<BusinessItem> _orders = new ObservableCollection<BusinessItem>();
        private string _viewTitle = "Invoice Find";
        private string _searchTerm = "";
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
            }
        }
        public int SelectedItemIndex { get; set; }

        public FindOrderViewModelX(IEventAggregator eventAggregator, IServiceFactory serviceFactory, IRegionManager regionManager)
        {
            _eventAggregator = eventAggregator;
            _serviceFactory = serviceFactory;
            _regionManager = regionManager;

            BindCommands();
            _eventAggregator.GetEvent<OrderLoadedEvent>().Publish(string.Empty);
        }
        public override string ViewTitle { get { return _viewTitle; } }

        public ObservableCollection<BusinessItem> FoundItems
        {
            get { return _orders; }
            private set { SetProperty(ref _orders, value); }
        }

        public string SearchTerm
        {
            get { return _searchTerm; }
            set { SetProperty(ref _searchTerm, value); GetOrdersCommand.RaiseCanExecuteChanged(); }
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
        public DelegateCommand GetOrdersCommand { get; set; }
        public DelegateCommand SearchCommand { get; set; }
        public DelegateCommand ChooseItemCommand { get; set; }

        private void BindCommands()
        {
            SearchCommand = new DelegateCommand(GetOrders, CanGetOrders);
            GetOrdersCommand = new DelegateCommand(GetOrders, CanGetOrders);
            ChooseItemCommand = new DelegateCommand(ChooseOrderX, CanChooseOrder);
        }

        private bool CanGetOrders()
        {
            return SearchTerm.Length > 0 && ButtonEnabled;
        }

        private bool CanChooseOrder()
        {
            return SelectedItem != null;
        }

        private async void GetOrders()
        {
            if (!string.IsNullOrWhiteSpace(SearchTerm))
            {
                MessageToDisplay = "Searching...";
                IsBusy = true;
                ButtonEnabled = false;
                IsLoading = true;
                GetOrdersCommand.RaiseCanExecuteChanged();
                var proxy = _serviceFactory.CreateClient<IOrderService>();

                using (proxy)
                {
                    var orders = proxy.FindOrdersByCompanyAsync((Company)CurrentCompany, SearchTerm);
                    await orders;

                    if (orders.Result.Count > 0)
                    {
                        foreach (var order in orders.Result)
                            FoundItems.Add(Map(order));

                        SelectedItem = FoundItems[0];
                        SelectedItemIndex = 0;
                    }
                }

                MessageToDisplay = FoundItems.Count.ToString() + " order(s) found";
                ButtonEnabled = true;
                GetOrdersCommand.RaiseCanExecuteChanged();
            }
            else
            {
                MessageToDisplay = "You must enter a search term in order to find an order";
            }
            IsLoading = false;
            IsBusy = false;
        }

        private void ChooseOrderX()
        {
            if (SelectedItem is BusinessItem busItem)
            {
                if (busItem.BusinessObject is Order order)
                {
                    var parameters = new NavigationParameters { { "OrderKey", order.OrderKey } };
                    _regionManager.RequestNavigate(RegionNames.ContentRegion, typeof(OrderViewX).FullName, parameters);
                }
            }
        }

        private BusinessItem Map(Order order)
        {
            return new BusinessItem
            {
                ItemId = order.OrderNumber,
                ItemCode = order.Account.AccountCode,
                ItemName = order.Account.AccountName,
                ItemEntryDate = order.OrderEntryDate,
                ItemStatus = order.OrderStatus.ToString(),
                ItemStatusDate = order.OrderStatusDate,
                BusinessObject = order,
                Total = (double)order.OrderValueSum,
                Quantity = order.OrderItemCount
            };
        }
    }
}
