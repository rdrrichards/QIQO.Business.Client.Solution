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
using QIQO.Business.Module.Orders.Views;
using System;
using System.Collections.ObjectModel;

namespace QIQO.Business.Module.Orders.ViewModels
{
    public class FindOrderViewModel : ViewModelBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IServiceFactory _serviceFactory;
        private readonly IRegionManager _regionManager;

        private ObservableCollection<Order> _orders = new ObservableCollection<Order>();
        private string _viewTitle = "Order Find";
        private string _searchTerm = "";
        private ItemSelectionNotification notification;
        private string _buttonText = "Find";
        private bool _buttonEnabled = true;
        private object _selectedItem;
        private bool _isSearching;

        public object SelectedOrder
        {
            get { return _selectedItem; }
            set
            {
                SetProperty(ref _selectedItem, value);
                ChooseOrderCommand.RaiseCanExecuteChanged();
            }
        }
        public int SelectedIndex { get; set; }

        public FindOrderViewModel()
        {
            _eventAggregator = Unity.Container.Resolve<IEventAggregator>();
            _serviceFactory = Unity.Container.Resolve<IServiceFactory>();
            _regionManager = Unity.Container.Resolve<IRegionManager>();

            BindCommands();
            _eventAggregator.GetEvent<OrderLoadedEvent>().Publish(string.Empty);
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

        public ObservableCollection<Order> Orders
        {
            get { return _orders; }
            private set { SetProperty(ref _orders, value); }
        }

        public string SearchTerm
        {
            get { return _searchTerm; }
            set { SetProperty(ref _searchTerm, value); GetOrdersCommand.RaiseCanExecuteChanged(); }
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
        public DelegateCommand GetOrdersCommand { get; set; }
        public DelegateCommand ChooseOrderCommand { get; set; }
        public DelegateCommand ChooseOrderCommandX { get; set; }

        private void BindCommands()
        {
            GetOrdersCommand = new DelegateCommand(GetOrders, CanGetOrders);
            ChooseOrderCommand = new DelegateCommand(ChooseOrder, CanChooseOrder);
        }

        private bool CanGetOrders()
        {
            return SearchTerm.Length > 0 && ButtonEnabled;
        }

        private bool CanChooseOrder()
        {
            return SelectedOrder != null;
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
                var order_service = _serviceFactory.CreateClient<IOrderService>();

                using (order_service)
                {
                    try
                    {
                        var orders = order_service.FindOrdersByCompanyAsync((Company)CurrentCompany, SearchTerm);
                        await orders;
                        Orders = new ObservableCollection<Order>(orders.Result);
                    }
                    catch (Exception ex)
                    {
                        MessageToDisplay = ex.Message;
                        return;
                    }
                }

                MessageToDisplay = Orders.Count.ToString() + " order(s) found";
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

        private void ChooseOrder()
        {
            if (SelectedOrder is Order sel_acct)
            {
                var parameters = new NavigationParameters();
                parameters.Add("OrderKey", sel_acct.OrderKey);
                _regionManager.RequestNavigate(RegionNames.ContentRegion, typeof(OrderShellView).FullName);
                _regionManager.RequestNavigate(RegionNames.OrdersRegion, typeof(OrderView).FullName, parameters);
                _regionManager.RequestNavigate(RegionNames.RibbonRegion, typeof(OrderRibbonView).FullName);
            }
        }
    }
}