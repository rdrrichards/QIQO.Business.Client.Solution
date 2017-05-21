using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using Prism.Regions;
using QIQO.Business.Client.Contracts;
using QIQO.Business.Client.Core;
using QIQO.Business.Client.Core.Infrastructure;
using QIQO.Business.Client.Core.UI;
using QIQO.Business.Client.Entities;
using QIQO.Business.Client.Wrappers;
using QIQO.Business.Module.General.Models;
using QIQO.Business.Module.Orders.Views;
using System;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace QIQO.Business.Module.Orders.ViewModels
{
    public class OpenOrderViewModelX : ViewModelBase
    {
        private readonly IEventAggregator event_aggregator;
        private readonly IServiceFactory service_factory;
        private ObservableCollection<OrderWrapper> _open_orders;
        private ObservableCollection<BusinessItem> _open_orders_ = new ObservableCollection<BusinessItem>();
        private object _selected_order;
        private readonly IRegionManager _regionManager;
        private string _header_msg;
        private bool _is_loading;

        public OpenOrderViewModelX(IEventAggregator event_aggtr, IServiceFactory service_fctry, IRegionManager regionManager)
        {
            event_aggregator = event_aggtr;
            service_factory = service_fctry;
            _regionManager = regionManager;
            
            GetCompanyOpenOrders();
            
            RefreshCommand = new DelegateCommand(GetCompanyOpenOrders);
            //EditOrderCommand = new DelegateCommand(EditOrder, CanEditOrder);
            ChooseItemCommand = new DelegateCommand(EditOrder, CanEditOrder);
        }

        private bool CanEditOrder()
        {
            return SelectedOrder != null;
        }

        private void EditOrder()
        {
            if (SelectedOrder is BusinessItem busItem)
            {
                if (busItem.BusinessObject is Order ord_to_edit)
                {
                    var parameters = new NavigationParameters { { "OrderKey", ord_to_edit.OrderKey } };
                    _regionManager.RequestNavigate(RegionNames.ContentRegion, typeof(OrderViewX).FullName, parameters);
                }
            }
        }

        public DelegateCommand OpenOrderCommand { get; set; }
        public DelegateCommand RefreshCommand { get; set; }
        public bool IsLoading
        {
            get { return _is_loading; }
            set { SetProperty(ref _is_loading, value); }
        }

        public int SelectedItemIndex { get; set; }
        public int SelectedOrderIndex { get; set; }
        public object SelectedOrder
        {
            get { return _selected_order; }
            set { SetProperty(ref _selected_order, value); }
        }
        public object SelectedItem
        {
            get { return _selected_order; }
            set { SetProperty(ref _selected_order, value); }
        }

        public ObservableCollection<BusinessItem> OpenOrders
        {
            get { return _open_orders_; }
            private set { SetProperty(ref _open_orders_, value); }
        }

        public string HeaderMessage
        {
            get { return _header_msg; }
            private set { SetProperty(ref _header_msg, value); }
        }
        public InteractionRequest<ItemEditNotification> EditOrderRequest { get; set; }
        public DelegateCommand EditOrderCommand { get; set; }
        public DelegateCommand ChooseItemCommand { get; set; }

        private async void GetCompanyOpenOrders()
        {
            HeaderMessage = "Open Orders (Loading...)";
            IsLoading = true;

            var proxy = service_factory.CreateClient<IOrderService>();
            var company = new Company() { CompanyKey = CurrentCompanyKey };
            var open_order_col = new ObservableCollection<OrderWrapper>();

            using (proxy)
            {
                var orders = proxy.GetOrdersByCompanyAsync(company);
                await orders;

                if (orders.Result.Count > 0)
                {
                    foreach (Order order in orders.Result)
                        OpenOrders.Add(Map(order));

                    SelectedOrder = OpenOrders[0];
                    SelectedItemIndex = 0;
                    HeaderMessage = "Open Orders (" + OpenOrders.Count.ToString() + ") X";
                }
                else
                {
                    OpenOrders = null; // open_order_col_;
                    HeaderMessage = "Open Orders (0)";
                }

            }
            IsLoading = false;
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