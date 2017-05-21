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
using System.Collections.ObjectModel;

namespace QIQO.Business.Module.Orders.ViewModels
{
    public class OpenOrderViewModelX : ViewModelBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IServiceFactory _serviceFactory;
        private readonly IRegionManager _regionManager;
        private ObservableCollection<BusinessItem> _openOrders = new ObservableCollection<BusinessItem>();
        private object _selectedItem;
        private string _headerMsg;
        private bool _isLoading;

        public OpenOrderViewModelX(IEventAggregator eventAggregator, IServiceFactory serviceFactory, IRegionManager regionManager)
        {
            _eventAggregator = eventAggregator;
            _serviceFactory = serviceFactory;
            _regionManager = regionManager;
            
            GetCompanyOpenOrders();
            
            RefreshCommand = new DelegateCommand(GetCompanyOpenOrders);
            ChooseItemCommand = new DelegateCommand(EditOrder, CanEditOrder);
        }

        private bool CanEditOrder()
        {
            return SelectedItem != null;
        }

        private void EditOrder()
        {
            if (SelectedItem is BusinessItem busItem)
            {
                if (busItem.BusinessObject is Order ord_to_edit)
                {
                    var parameters = new NavigationParameters { { "OrderKey", ord_to_edit.OrderKey } };
                    _regionManager.RequestNavigate(RegionNames.ContentRegion, typeof(OrderViewX).FullName, parameters);
                }
            }
        }
        
        public DelegateCommand RefreshCommand { get; set; }
        public bool IsLoading
        {
            get { return _isLoading; }
            set { SetProperty(ref _isLoading, value); }
        }

        public int SelectedItemIndex { get; set; }
        public object SelectedItem
        {
            get { return _selectedItem; }
            set { SetProperty(ref _selectedItem, value); }
        }

        public ObservableCollection<BusinessItem> OpenOrders
        {
            get { return _openOrders; }
            private set { SetProperty(ref _openOrders, value); }
        }

        public string HeaderMessage
        {
            get { return _headerMsg; }
            private set { SetProperty(ref _headerMsg, value); }
        }

        public DelegateCommand ChooseItemCommand { get; set; }

        private async void GetCompanyOpenOrders()
        {
            HeaderMessage = "Open Orders (Loading...)";
            IsLoading = true;

            var proxy = _serviceFactory.CreateClient<IOrderService>();
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

                    SelectedItem = OpenOrders[0];
                    SelectedItemIndex = 0;
                    HeaderMessage = "Open Orders (" + OpenOrders.Count.ToString() + ") X";
                }
                else
                {
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