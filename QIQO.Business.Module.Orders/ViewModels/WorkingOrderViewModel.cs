using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using QIQO.Business.Client.Core;
using QIQO.Business.Client.Core.Infrastructure;
using QIQO.Business.Client.Core.UI;
using QIQO.Business.Client.Entities;
using QIQO.Business.Module.General.Models;
using QIQO.Business.Module.Orders.Services;
using QIQO.Business.Module.Orders.Views;
using System.Collections.ObjectModel;

namespace QIQO.Business.Module.Orders.ViewModels
{
    public class WorkingOrderViewModel : ViewModelBase, IRegionMemberLifetime
    {
        private readonly IWorkingOrderService _workingOrdersService;
        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManager;
        private ObservableCollection<BusinessItem> _workingOrders = new ObservableCollection<BusinessItem>();
        private object _selectedOrder;
        private string _headerMsg = "Working Orders";

        public WorkingOrderViewModel(IWorkingOrderService workingOrdersService, IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            _workingOrdersService = workingOrdersService;
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;

            ChooseItemCommand = new DelegateCommand(OpenOrder);
            InitWorkingOrderList();
            _eventAggregator.GetEvent<OpenOrderServiceEvent>().Subscribe(OnOpenOrderChangedEvent, ThreadOption.UIThread);
        }

        private void OnOpenOrderChangedEvent(int open_order_cnt)
        {
            InitWorkingOrderList();
            HeaderMessage = $"Working Orders ({WorkingOrders.Count})";
        }
        public bool IsLoading => false;

        private void InitWorkingOrderList()
        {
            var workingOrders = _workingOrdersService.GetWorkingOrders();
            WorkingOrders.Clear();
            foreach (var wo in workingOrders)
                WorkingOrders.Add(Map(wo.Model));
        }

        public bool KeepAlive => false;
        public DelegateCommand ChooseItemCommand { get; set; }

        public ObservableCollection<BusinessItem> WorkingOrders
        {
            get { return _workingOrders; }
            private set { SetProperty(ref _workingOrders, value); }
        }

        public int SelectedItemIndex { get; set; }
        public object SelectedItem
        {
            get { return _selectedOrder; }
            set { SetProperty(ref _selectedOrder, value); }
        }

        public string HeaderMessage
        {
            get { return _headerMsg; }
            private set { SetProperty(ref _headerMsg, value); }
        }

        private void OpenOrder()
        {
            if (SelectedItem is BusinessItem busItem)
            {
                if (busItem.BusinessObject is Order orderToEdit)
                {
                    var parameters = new NavigationParameters { { "OrderNumber", orderToEdit.OrderNumber } };
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
