using Prism.Commands;
using Prism.Events;
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
    public class OpenOrderViewModel : ViewModelBase
    {
        IEventAggregator event_aggregator;
        IServiceFactory service_factory;
        private ObservableCollection<OrderWrapper> _open_orders;
        private object _selected_order;
        private IRegionManager _regionManager;
        private string _header_msg;

        public OpenOrderViewModel(IEventAggregator event_aggtr, IServiceFactory service_fctry, IRegionManager regionManager)
        {
            // add code to this contructor to bring in dependencies and assign them to local variable
            event_aggregator = event_aggtr;
            service_factory = service_fctry;
            _regionManager = regionManager;

            //HeaderMessage = "Open Orders (Loading...)";
            GetCompanyOpenOrders();

            OpenOrderCommand = new DelegateCommand(OpenOrder);
            RefreshCommand = new DelegateCommand(GetCompanyOpenOrders);
            ApplicationCommands.DashboardRefreshCommand.RegisterCommand(RefreshCommand);
        }

        private void OpenOrder()
        {
            var selectedOrder = SelectedOrder as OrderWrapper;
            //MessageBox.Show("The order that you double-clicked on is: " + selectedOrder.OrderNumber);
            //*** NEEDS WORK!
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

        private async void GetCompanyOpenOrders()
        {
            HeaderMessage = "Open Orders (Loading...)";
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
                    HeaderMessage = "Open Orders (" + OpenOrders.Count.ToString() + ")";
                }
                else
                {
                    OpenOrders = open_order_col;
                    HeaderMessage = "Open Orders (0)";
                }

            }
            //});
            //SetEventDatesContext();
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