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
using QIQO.Business.Module.Orders.Views;
//using QIQO.Custom.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace QIQO.Business.Module.Orders.ViewModels
{
    public class OpenOrderViewModelX : ViewModelBase
    {
        private readonly IEventAggregator event_aggregator;
        private readonly IServiceFactory service_factory;
        private ObservableCollection<OrderWrapper> _open_orders;
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

            OpenOrderCommand = new DelegateCommand(OpenOrder);
            RefreshCommand = new DelegateCommand(GetCompanyOpenOrders);
            ApplicationCommands.DashboardRefreshCommand.RegisterCommand(RefreshCommand);
            EditOrderRequest = new InteractionRequest<ItemEditNotification>();
            EditOrderCommand = new DelegateCommand(EditOrder, CanEditOrder);
        }

        private bool CanEditOrder()
        {
            return SelectedOrder != null;
        }

        private void EditOrder()
        {
            var ord_to_edit = SelectedOrder as OrderWrapper;
            if (ord_to_edit != null)
            {
                var parameters = new NavigationParameters();
                parameters.Add("OrderKey", ord_to_edit.OrderKey);
                _regionManager.RequestNavigate(RegionNames.ContentRegion, typeof(OrderViewX).FullName, parameters);
                //ItemEditNotification notification = new ItemEditNotification(ord_to_edit);
                //notification.Title = "Edit Order";
                //EditOrderRequest.Raise(notification,
                //    r =>
                //    {
                //        if (r != null && r.Confirmed && r.EditibleObject != null) // 
                //        {
                //            //EntityAttribute att = r.EditibleObject as EntityAttribute;
                //            //if (att != null)
                //            //{
                //            //    var att_to_change = AttSelectedItem as EntityAttributeWrapper;
                //            //    if (att_to_change != null)
                //            //    {
                //            //        att_to_change.AttributeValue = att.AttributeValue;
                //            //    }
                //            //}
                //        }
                //    });
            }
        }

        private void OpenOrder()
        {
            var selectedOrder = SelectedOrder as OrderWrapper;
            if (selectedOrder != null)
            {
                var parameters = new NavigationParameters();
                parameters.Add("OrderKey", selectedOrder.OrderKey);
                _regionManager.RequestNavigate(RegionNames.ContentRegion, typeof(OrderShellView).FullName);
                _regionManager.RequestNavigate(RegionNames.OrdersRegion, typeof(OrderView).FullName, parameters);
                _regionManager.RequestNavigate(RegionNames.RibbonRegion, typeof(OrderRibbonView).FullName);
            }
        }

        public DelegateCommand OpenOrderCommand { get; set; }
        public DelegateCommand RefreshCommand { get; set; }
        public bool IsLoading
        {
            get { return _is_loading; }
            set { SetProperty(ref _is_loading, value); }
        }

        public int SelectedOrderIndex { get; set; }
        public object SelectedOrder
        {
            get { return _selected_order; }
            set { SetProperty(ref _selected_order, value); }
        }

        public ObservableCollection<OrderWrapper> OpenOrders
        {
            get { return _open_orders; }
            private set { SetProperty(ref _open_orders, value); }
        }

        public string HeaderMessage
        {
            get { return _header_msg; }
            private set { SetProperty(ref _header_msg, value); }
        }
        public InteractionRequest<ItemEditNotification> EditOrderRequest { get; set; }
        public DelegateCommand EditOrderCommand { get; set; }

        private async void GetCompanyOpenOrders()
        {
            HeaderMessage = "Open Orders (Loading...)";
            IsLoading = true;
            //ExecuteFaultHandledOperation(() =>
            //{
            IOrderService proxy = service_factory.CreateClient<IOrderService>();
            Company company = new Company() { CompanyKey = CurrentCompanyKey };
            ObservableCollection<OrderWrapper> open_order_col = new ObservableCollection<OrderWrapper>();

            using (proxy)
            {
                Task<List<Order>> orders = proxy.GetOrdersByCompanyAsync(company);
                await orders;

                if (orders.Result.Count > 0)
                {
                    foreach (Order order in orders.Result)
                    {
                        OrderWrapper order_wrapper = new OrderWrapper(order);
                        open_order_col.Add(order_wrapper);
                    }
                    OpenOrders = open_order_col;
                    SelectedOrder = OpenOrders[0];
                    SelectedOrderIndex = 0;
                    HeaderMessage = "Open Orders (" + OpenOrders.Count.ToString() + ") X";
                }
                else
                {
                    OpenOrders = open_order_col;
                    HeaderMessage = "Open Orders (0)";
                }

            }
            //});
            //SetEventDatesContext();
            IsLoading = false;
        }

        //private void SetEventDatesContext()
        //{
        //    ObservableCollection<QIQODate> eds = new ObservableCollection<QIQODate>();

        //    foreach (OrderWrapper order in OpenOrders)
        //    {
        //        QIQODate id = new QIQODate()
        //        {
        //            Date = (DateTime)order.DeliverByDate,
        //            EntityType = "Account",
        //            EntityName = order.Account.AccountName,
        //            BackgroundBrush = Brushes.LightGreen,
        //            DateDescription = $"Due Date\nOrder: {order.OrderNumber}",
        //            DateType = QIQODateType.OrderDeliverByDate,
        //            FontWeight = FontWeights.Bold
        //        };

        //        eds.Add(id);
        //    }

        //    for (int i=1;i<=90;i++)
        //    {
        //        DateTime dt = DateTime.Today.AddDays(i);
        //        if (dt.DayOfWeek == DayOfWeek.Monday)
        //            eds.Add(new QIQODate()
        //            {
        //                Date = dt,
        //                EntityType = "Company",
        //                EntityName = CurrentCompanyName,
        //                BackgroundBrush = Brushes.Cyan,
        //                DateDescription = "Standard Delivery Date",
        //                DateType = QIQODateType.OrderDeliverByDate,
        //                FontWeight = FontWeights.Bold
        //            });
        //    }

        //    event_aggregator.GetEvent<CalendarContextChangedEvent>().Publish(eds);
        //}

        protected override void DisplayErrorMessage(Exception ex, [CallerMemberName] string methodName = "")
        {
            event_aggregator.GetEvent<GeneralErrorEvent>().Publish(methodName + " - " + ex.Message);
        }
    }
}