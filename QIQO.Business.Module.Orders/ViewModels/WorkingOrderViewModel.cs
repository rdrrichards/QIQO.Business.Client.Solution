using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using QIQO.Business.Client.Core;
using QIQO.Business.Client.Core.Infrastructure;
using QIQO.Business.Client.Core.UI;
using QIQO.Business.Client.Wrappers;
using QIQO.Business.Module.Orders.Services;
using QIQO.Business.Module.Orders.Views;
using System.Collections.ObjectModel;

namespace QIQO.Business.Module.Orders.ViewModels
{
    public class WorkingOrderViewModel : ViewModelBase, IRegionMemberLifetime
    {
        private readonly IWorkingOrderService working_orders_service;
        private readonly IEventAggregator event_aggregator;
        private readonly IRegionManager _regionManager;
        private ObservableCollection<OrderWrapper> _working_orders;
        private object _selected_order;
        private string _header_msg = "Working Orders";

        public WorkingOrderViewModel(IWorkingOrderService working_orders_svc, IEventAggregator event_aggtr, IRegionManager regionManager)
        {
            working_orders_service = working_orders_svc;
            event_aggregator = event_aggtr;
            _regionManager = regionManager;

            OpenOrderCommand = new DelegateCommand(OpenOrder);
            InitWorkingOrderList();
            event_aggregator.GetEvent<OpenOrderServiceEvent>().Subscribe(OnOpenOrderChangedEvent, ThreadOption.BackgroundThread);
        }

        private void OnOpenOrderChangedEvent(int open_order_cnt)
        {
            InitWorkingOrderList();
            HeaderMessage = $"Working Orders ({WorkingOrders.Count})";
        }
        public bool IsLoading => false;

        private void InitWorkingOrderList()
        {
            WorkingOrders = working_orders_service.GetWorkingOrders();
        }

        public bool KeepAlive => false;
        public DelegateCommand OpenOrderCommand { get; set; }

        public ObservableCollection<OrderWrapper> WorkingOrders
        {
            get { return _working_orders; }
            private set { SetProperty(ref _working_orders, value); }
        }

        public int SelectedOrderIndex { get; set; }
        public object SelectedOrder
        {
            get { return _selected_order; }
            set { SetProperty(ref _selected_order, value); }
        }

        public string HeaderMessage
        {
            get { return _header_msg; }
            private set { SetProperty(ref _header_msg, value); }
        }
        private void OpenOrder()
        {
            var selectedOrder = SelectedOrder as OrderWrapper;
            if (selectedOrder != null)
            {
                var parameters = new NavigationParameters();
                parameters.Add("OrderNumber", selectedOrder.OrderNumber);
                _regionManager.RequestNavigate(RegionNames.ContentRegion, typeof(OrderViewX).FullName, parameters);
            }
        }
    }
}
