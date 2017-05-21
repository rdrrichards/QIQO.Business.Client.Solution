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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace QIQO.Business.Module.Orders.ViewModels
{
    public class FindOrderViewModel : ViewModelBase
    {
        IEventAggregator event_aggregator;
        IServiceFactory service_factory;
        private ObservableCollection<Order> _orders = new ObservableCollection<Order>();
        private string _viewTitle = "Order Find";
        private string _search_term = "";
        private ItemSelectionNotification notification;
        private IRegionManager _regionManager;
        private string _button_text = "Find";
        private bool _button_enabled = true;
        private object _selected_order;
        private bool _is_searching;

        public object SelectedOrder
        {
            get { return _selected_order; }
            set
            {
                SetProperty(ref _selected_order, value);
                ChooseOrderCommand.RaiseCanExecuteChanged();
            }
        }
        public int SelectedIndex { get; set; }

        public FindOrderViewModel()
        {
            event_aggregator = Unity.Container.Resolve<IEventAggregator>();
            service_factory = Unity.Container.Resolve<IServiceFactory>();
            _regionManager = Unity.Container.Resolve<IRegionManager>(); 

            BindCommands();
            event_aggregator.GetEvent<OrderLoadedEvent>().Publish(string.Empty);
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
            get { return _search_term; }
            set { SetProperty(ref _search_term, value); GetOrdersCommand.RaiseCanExecuteChanged(); }
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
        public DelegateCommand GetOrdersCommand { get; set; }
        public DelegateCommand ChooseOrderCommand { get; set; }
        public DelegateCommand ChooseOrderCommandX { get; set; }

        private void BindCommands()
        {
            //CloseWindowCommand = new DelegateCommand(DoCancel);
            GetOrdersCommand = new DelegateCommand(GetOrders, CanGetOrders);
            ChooseOrderCommand = new DelegateCommand(ChooseOrder, CanChooseOrder);
            ChooseOrderCommandX = new DelegateCommand(ChooseOrderX, CanChooseOrder);
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
                IOrderService order_service = service_factory.CreateClient<IOrderService>();

                using (order_service)
                {
                    try
                    {
                        Task<List<Order>> orders = order_service.FindOrdersByCompanyAsync((Company)CurrentCompany, SearchTerm);
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
            Order sel_acct = SelectedOrder as Order;
            if (sel_acct != null)
            {
                //if (notification != null)
                //{
                //    notification.SelectedItem = sel_acct;
                //    notification.Confirmed = true;
                //}

                //FinishInteraction();
                var parameters = new NavigationParameters();
                parameters.Add("OrderKey", sel_acct.OrderKey);
                _regionManager.RequestNavigate(RegionNames.ContentRegion, typeof(OrderShellView).FullName);
                _regionManager.RequestNavigate(RegionNames.OrdersRegion, typeof(OrderView).FullName, parameters);
                _regionManager.RequestNavigate(RegionNames.RibbonRegion, typeof(OrderRibbonView).FullName);
            }
        }

        private void ChooseOrderX()
        {
            Order sel_acct = SelectedOrder as Order;
            if (sel_acct != null)
            {
                var parameters = new NavigationParameters();
                parameters.Add("OrderKey", sel_acct.OrderKey);
                _regionManager.RequestNavigate(RegionNames.ContentRegion, typeof(OrderViewX).FullName, parameters);
            }
        }
    }
}
