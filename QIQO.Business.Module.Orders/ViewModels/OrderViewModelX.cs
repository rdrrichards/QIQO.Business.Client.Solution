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
using QIQO.Business.Module.Orders.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Transactions;
using Unity.Interception.Utilities;

namespace QIQO.Business.Module.Orders.ViewModels
{
    public class OrderViewModelX : ViewModelBase, IRegionMemberLifetime, IConfirmNavigationRequest //, IInteractionRequestAware
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IServiceFactory _serviceFactory;
        private readonly IProductListService _productService;
        private readonly IRegionManager _regionManager;
        private readonly IWorkingOrderService _workingOrderService;
        private readonly IReportService _reportService;
        private OrderWrapper _order;
        private Client.Entities.Account _currentAccount;

        private object _selectedOrderItem;
        private AddressWrapper _shipAddress;
        private AddressWrapper _billAddress;
        private ObservableCollection<Product> _productList;
        private ObservableCollection<Representative> _accountReps;
        private ObservableCollection<Representative> _salesReps;
        private ObservableCollection<FeeSchedule> _feeScheduleList;
        private ObservableCollection<AccountPerson> _accountContacts;
        private string _viewTitle = ApplicationStrings.TabTitleNewOrder;
        private bool _gridEnabled = true;

        public OrderViewModelX(IEventAggregator eventAggregator, IServiceFactory serviceFactory, 
            IProductListService productService, IRegionManager regionManager,
            IReportService reportService, IWorkingOrderService workingOrderService) //
        {
            _eventAggregator = eventAggregator;
            _serviceFactory = serviceFactory;
            _productService = productService;
            _regionManager = regionManager;
            _reportService = reportService;
            _workingOrderService = workingOrderService;

            GetProductList();
            BindCommands();
            GetCompanyRepLists();
            
            IsActive = true;
            IsActiveChanged += OrderViewModel_IsActiveChanged;
            _eventAggregator.GetEvent<OrderLoadedEvent>().Publish(string.Empty);
        }

        private void OrderViewModel_IsActiveChanged(object sender, EventArgs e)
        {
            IsActive = true;
        }

        public bool KeepAlive {
            get {
                if (Order.IsChanged && Order.Account.AccountCode != null) return true;
                return false;
            }
        }

        public void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            //********** Do some sort fo confirmation with the end user here
            if (Order.IsChanged && !string.IsNullOrWhiteSpace(Order.Account.AccountCode))
            {
                Confirmation confirm = new Confirmation();
                confirm.Title = ApplicationStrings.SaveChangesTitle;
                confirm.Content = ApplicationStrings.SaveChangesPrompt;
                SaveChangesConfirmationRequest.Raise(confirm,
                    r => {
                        if (r != null && r.Confirmed)
                        {
                            if (Order.IsValid)
                            {
                                DoSave();
                                continuationCallback(false);
                            }
                            continuationCallback(true);
                        }
                        else
                        {
                            continuationCallback(true);
                        }
                    });
            }
            else
                continuationCallback(true);
        }
        
        public OrderWrapper Order
        {
            get { return _order; }
            private set { SetProperty(ref _order, value); }
        }

        public AddressWrapper DefaultBillingAddress
        {
            get { return _billAddress; }
            private set { SetProperty(ref _billAddress, value); }
        }
        public AddressWrapper DefaultShippingAddress
        {
            get { return _shipAddress; }
            private set { SetProperty(ref _shipAddress, value); }
        }

        public ObservableCollection<AccountPerson> AccountContacts
        {
            get { return _accountContacts; }
            private set { SetProperty(ref _accountContacts, value); }
        }

        public ObservableCollection<Product> ProductList
        {
            get { return _productList; }
            private set { SetProperty(ref _productList, value); }
        }
        public ObservableCollection<Representative> AccountRepList
        {
            get { return _accountReps; }
            private set { SetProperty(ref _accountReps, value); }
        }
        public ObservableCollection<Representative> SalesRepList
        {
            get { return _salesReps; }
            private set { SetProperty(ref _salesReps, value); }
        }
        public ObservableCollection<FeeSchedule> FeeScheduleList
        {
            get { return _feeScheduleList; }
            private set { SetProperty(ref _feeScheduleList, value); }
        }

        public object SelectedOrderItem
        {
            get { return _selectedOrderItem; }
            set
            {
                SetProperty(ref _selectedOrderItem, value);
                InvalidateCommands();
            }
        }

        public bool GridIsEnabled
        {
            get { return _gridEnabled; }
            private set { SetProperty(ref _gridEnabled, value); }
        }

        public override string ViewTitle { get { return _viewTitle; }
            protected set { SetProperty(ref _viewTitle, value); }
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            var paramAccountCode = navigationContext.Parameters.Where(item => item.Key == "AccountCode").FirstOrDefault();
            var paramOrderKey = navigationContext.Parameters.Where(item => item.Key == "OrderKey").FirstOrDefault();
            var paramOrderNumber = navigationContext.Parameters.Where(item => item.Key == "OrderNumber").FirstOrDefault();

            if (paramAccountCode.Value != null)
            {
                Order.Account.AccountCode = (string)navigationContext.Parameters["AccountCode"];
                GetAccount(Order.Account.AccountCode);
                return;
            }

            if (paramOrderKey.Value != null)
            {
                GetOrder((int)paramOrderKey.Value);
                return;
            }

            if (paramOrderNumber.Value != null)
            {
                Order = _workingOrderService.GetOrder((string)navigationContext.Parameters["OrderNumber"]);
                GetAccount(Order.Account.AccountCode);
                _eventAggregator.GetEvent<OrderLoadedEvent>().Publish(Order.OrderNumber);
                return;
            }

            InitNewOrder();
        }

        public override bool IsNavigationTarget(NavigationContext navigationContext)
        {
            var paramAccountCode = navigationContext.Parameters.Where(item => item.Key == "AccountCode").FirstOrDefault();
            var paramOrderNumber = navigationContext.Parameters.Where(item => item.Key == "OrderKey").FirstOrDefault();

            return (paramAccountCode.Value != null || paramOrderNumber.Value != null) ? true : false;
        }

        public int SelectedOrderItemIndex { get; set; }
        public DelegateCommand CancelCommand { get; set; }
        public DelegateCommand CloseCommand { get; set; }
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand DeleteCommand { get; set; }
        public DelegateCommand PrintCommand { get; set; }
        public DelegateCommand<string> GetAccountCommand { get; set; }
        public DelegateCommand<object> UpdateProdInfoCommand { get; set; }
        public DelegateCommand UpdateItemTotalCommand { get; set; }
        public DelegateCommand UpdateHeaderFromDetailCommand { get; set; }
        public DelegateCommand NewOrderItemCommand { get; set; }
        public DelegateCommand EditOrderItemCommand { get; set; }
        public DelegateCommand DeleteOrderItemCommand { get; set; }
        public DelegateCommand FindAccountCommand { get; set; }
        public DelegateCommand NewAccountCommand { get; set; }

        public InteractionRequest<ItemEditNotification> EditOrderItemRequest { get; set; }
        public InteractionRequest<ItemSelectionNotification> FindAccountRequest { get; set; }

        public InteractionRequest<IConfirmation> SaveChangesConfirmationRequest { get; set; }
        public InteractionRequest<IConfirmation> DeleteConfirmationRequest { get; set; }

        protected override void DisplayErrorMessage(Exception ex, [CallerMemberName] string methodName = "")
        {
            _eventAggregator.GetEvent<GeneralErrorEvent>().Publish(methodName + " - " + ex.Message);
        }
        protected void DisplayErrorMessage(string msg)
        {
            _eventAggregator.GetEvent<GeneralErrorEvent>().Publish(msg);
        }

        private void InitNewOrder()
        {
            var new_order = new Order() //*** GET this initializatoin stuff into the objects themselves!! (complete)
            {
                OrderEntryDate = DateTime.Now,
                OrderStatusDate = DateTime.Now,
                DeliverByDate = DateTime.Now.AddDays(7), // think about a defaul lead time for each account
                SalesRep = SalesRepList[0],
                AccountRep = AccountRepList[0]
            };

            new_order.OrderItems.Add(InitNewOrderItem(1));
            SelectedOrderItemIndex = 0;            

            Order = new OrderWrapper(new_order);
            DefaultBillingAddress = new AddressWrapper(new Address());
            DefaultShippingAddress = new AddressWrapper(new Address());
            Order.PropertyChanged += Context_PropertyChanged;
            Order.AcceptChanges();
            // Add order to the service here!
            _workingOrderService.OpenOrder(Order);

            GridIsEnabled = false;
        }

        private OrderItem InitNewOrderItem(int orderItemSeq)
        {
            return new OrderItem()
            {
                SalesRep = SalesRepList[0],
                AccountRep = AccountRepList[0]
            };
        }

        private void GetOrder(int orderKey)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var orderService = _serviceFactory.CreateClient<IOrderService>();
                using (orderService)
                {
                    Order = new OrderWrapper(orderService.GetOrder(orderKey));
                    AccountContacts = new ObservableCollection<AccountPerson>(Order.Account.Model.Employees.Where(item =>
                                                                                item.CompanyRoleType == QIQOPersonType.AccountContact).ToList());
                    Order.PropertyChanged += Context_PropertyChanged;
                    Order.AcceptChanges();
                    _currentAccount = Order.Account.Model;
                }

                DefaultBillingAddress = Order.OrderItems[0].OrderItemBillToAddress;
                DefaultShippingAddress = Order.OrderItems[0].OrderItemShipToAddress;
                ViewTitle = Order.OrderNumber;
                GridIsEnabled = Order.OrderStatus != QIQOOrderStatus.Complete ? true : false;
                _eventAggregator.GetEvent<GeneralMessageEvent>().Publish($"Order {Order.OrderNumber} loaded successfully");
                _eventAggregator.GetEvent<OrderLoadedEvent>().Publish(Order.OrderNumber);
                _eventAggregator.GetEvent<NavigationEvent>().Publish(ViewNames.OrderHomeView);
            });
        }

        private void GetCompanyRepLists()
        {
            var employeeService = _serviceFactory.CreateClient<IEmployeeService>();
            using (employeeService)
            {
                AccountRepList = new ObservableCollection<Representative>(employeeService.GetAccountRepsByCompany(CurrentCompanyKey));
                SalesRepList = new ObservableCollection<Representative>(employeeService.GetSalesRepsByCompany(CurrentCompanyKey));
            }

        }

        private void GetProductList()
        {
            ProductList = new ObservableCollection<Product>(_productService.ProductList);
        }

        private void BindCommands()
        {
            CancelCommand = new DelegateCommand(DoCancel, CanDoCancel);
            CancelCommand.IsActive = true;
            SaveCommand = new DelegateCommand(DoSave, CanDoSave);
            SaveCommand.IsActive = true;
            DeleteCommand = new DelegateCommand(DoDelete, CanDoDelete);
            DeleteCommand.IsActive = true;
            PrintCommand = new DelegateCommand(DoPrint, CanDoDelete);
            PrintCommand.IsActive = true;
            CloseCommand = new DelegateCommand(DoCancel);
            GetAccountCommand = new DelegateCommand<string>(GetAccount, CanGetAccount);
            UpdateProdInfoCommand = new DelegateCommand<object>(PopulateProductInfo);
            UpdateItemTotalCommand = new DelegateCommand(UpdateItemTotals);
            UpdateHeaderFromDetailCommand = new DelegateCommand(UpdateHeaderFromDetail);
            NewOrderItemCommand = new DelegateCommand(AddOrderItem, CanAddOrderItem);
            EditOrderItemCommand = new DelegateCommand(EditOrderItem, CanEditOrderItem);
            DeleteOrderItemCommand = new DelegateCommand(DeleteOrderItem, CanDeleteOrderItem);
            FindAccountCommand = new DelegateCommand(FindAccount);
            NewAccountCommand = new DelegateCommand(NewAccount);

            EditOrderItemRequest = new InteractionRequest<ItemEditNotification>();
            FindAccountRequest = new InteractionRequest<ItemSelectionNotification>();
            SaveChangesConfirmationRequest = new InteractionRequest<IConfirmation>();
            DeleteConfirmationRequest = new InteractionRequest<IConfirmation>();
        }

        private void DoPrint()
        {
            _reportService.ExecuteReport(Reports.Order, $"order_key={Order.OrderKey.ToString()}");
        }

        private void NewAccount()
        {
            _regionManager.RequestNavigate(RegionNames.ContentRegion, ViewNames.AccountView);
        }

        private bool CanDoCancel()
        {
            return Order.IsChanged;
        }

        private void DoCancel()
        {
            _workingOrderService.CloseOrder(Order);
            _regionManager.RequestNavigate(RegionNames.ContentRegion, ViewNames.OrderHomeView);
        }

        private bool CanDoDelete()
        {
            return Order.OrderKey > 0;
        }

        private void DoDelete()
        {
            var confirm = new Confirmation();
            confirm.Title = ApplicationStrings.DeleteOrderTitle;
            confirm.Content = $"Are you sure you want to delete order {Order.OrderNumber}?\n\nClick OK to delete. Click Cancel to return to the form.";
            DeleteConfirmationRequest.Raise(confirm,
                r => {
                    if (r != null && r.Confirmed)
                    {
                        DeleteOrder(Order.OrderNumber);
                    }
                });
        }

        private void DeleteOrder(string orderNumber)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var orderService = _serviceFactory.CreateClient<IOrderService>();
                using (var scope = new TransactionScope())
                {
                    using (orderService)
                    {
                        bool ret_val = orderService.DeleteOrder(Order.Model);
                        _eventAggregator.GetEvent<OrderDeletedEvent>().Publish($"Order {orderNumber} deleted successfully");
                        _regionManager.RequestNavigate(RegionNames.ContentRegion, ViewNames.OrderHomeView);
                    }
                    scope.Complete();
                }
            });
        }

        private void Context_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            InvalidateCommands();
        }

        private void InvalidateCommands()
        {
            SaveCommand.RaiseCanExecuteChanged();
            DeleteCommand.RaiseCanExecuteChanged();
            CancelCommand.RaiseCanExecuteChanged();
            PrintCommand.RaiseCanExecuteChanged();
            CloseCommand.RaiseCanExecuteChanged();
            GetAccountCommand.RaiseCanExecuteChanged();
            EditOrderItemCommand.RaiseCanExecuteChanged();
            DeleteOrderItemCommand.RaiseCanExecuteChanged();
            NewOrderItemCommand.RaiseCanExecuteChanged();
        }

        private void FindAccount()
        {
            var notification = new ItemSelectionNotification();
            notification.Title = ApplicationStrings.NotificationFindAccount;
            FindAccountRequest.Raise(notification,
                r => {
                    if (r != null && r.Confirmed && r.SelectedItem != null)
                    {
                        if (r.SelectedItem is Client.Entities.Account found_account)
                            GetAccount(found_account.AccountCode);
                    }
                });
        }

        private bool CanDeleteOrderItem()
        {
            return (SelectedOrderItem != null && ((OrderItemWrapper)SelectedOrderItem).ProductKey > 0);
        }

        private void DeleteOrderItem()
        {
            if (SelectedOrderItem is OrderItemWrapper itemToDel)
            {
                if (itemToDel.OrderItemKey == 0)
                    Order.OrderItems.Remove(itemToDel);
                else
                {
                    itemToDel.OrderItemStatus = QIQOOrderItemStatus.Canceled;
                    itemToDel.OrderItemQuantity = 0;
                    itemToDel.OrderItemLineSum = 0M;
                    itemToDel.ItemPricePer = 0M;
                    itemToDel.ProductDesc = itemToDel.ProductDesc + " (Deleted)";
                    itemToDel.OrderItemCompleteDate = DateTime.Now;
                }
                UpdateItemTotals();
            }
        }

        private bool CanEditOrderItem()
        {
            return (SelectedOrderItem != null && ((OrderItemWrapper)SelectedOrderItem).ProductKey > 0);
        }

        private void EditOrderItem()
        {
            if (SelectedOrderItem is OrderItemWrapper itemToEdit)
                ChangeOrderItem(itemToEdit.Model.Copy(), ApplicationStrings.NotificationEdit);
        }

        private bool CanAddOrderItem()
        {
            return Order.Account.AccountKey != 0;
        }

        private void AddOrderItem()
        {
            var existingEmptyItem = Order.OrderItems.Where(i => i.ProductCode == null).FirstOrDefault().Model;
            if (existingEmptyItem != null)
            {
                existingEmptyItem.OrderItemQuantity = 1;
                existingEmptyItem.OrderKey = Order.OrderKey;
                existingEmptyItem.AccountRep = Order.AccountRep.Model;
                existingEmptyItem.SalesRep = Order.SalesRep.Model;
                existingEmptyItem.OrderItemBillToAddress = DefaultBillingAddress.Model;
                existingEmptyItem.OrderItemShipToAddress = DefaultShippingAddress.Model;
                ChangeOrderItem(existingEmptyItem, ApplicationStrings.NotificationEdit);
            }
            else
            {
                var newOrderItem = new OrderItem()
                {
                    AccountRep = Order.AccountRep.Model,
                    OrderItemBillToAddress = DefaultBillingAddress.Model,
                    OrderItemQuantity = 1,
                    OrderItemShipToAddress = DefaultShippingAddress.Model,
                    OrderKey = Order.OrderKey,
                    SalesRep = Order.SalesRep.Model
                };
                ChangeOrderItem(newOrderItem, ApplicationStrings.NotificationAdd);
            }
        }

        private void ChangeOrderItem(OrderItem orderItem, string action)
        {
            if (orderItem != null)
            {
                GridIsEnabled = false;
                var bill_addresses = _currentAccount.Addresses.Where(item => item.AddressType == QIQOAddressType.Billing).ToList();
                var ship_addresses = _currentAccount.Addresses.Where(item => item.AddressType == QIQOAddressType.Shipping).ToList();
                var needed_objects = new Tuple<object, object, object>(orderItem, bill_addresses, ship_addresses);
                var notification = new ItemEditNotification(needed_objects);
                notification.Title = action + " Order Item"; //+ emp_to_edit.PersonCode + " - " + emp_to_edit.PersonFullNameFML;
                EditOrderItemRequest.Raise(notification,
                    r =>
                    {
                        if (r != null && r.Confirmed && r.EditibleObject != null) // 
                        {
                            if (r.EditibleObject is OrderItem obj)
                            {
                                if (action == ApplicationStrings.NotificationEdit)
                                {
                                    var changed_prod = ProductList.Where(item => item.ProductKey == obj.ProductKey).FirstOrDefault();
                                    var item_to_update = SelectedOrderItem as OrderItemWrapper;
                                    item_to_update.ProductKey = obj.ProductKey;
                                    item_to_update.ProductCode = changed_prod.ProductCode;
                                    item_to_update.ProductName = obj.ProductName;
                                    item_to_update.ProductDesc = obj.ProductDesc;
                                    item_to_update.OrderItemQuantity = obj.OrderItemQuantity;
                                    item_to_update.ItemPricePer = obj.ItemPricePer;
                                    item_to_update.OrderItemLineSum = orderItem.OrderItemQuantity * orderItem.ItemPricePer;
                                    item_to_update.OrderItemStatus = obj.OrderItemStatus;
                                    item_to_update.OrderItemProduct.ProductKey = obj.ProductKey;
                                    item_to_update.OrderItemProduct.ProductCode = changed_prod.ProductCode;
                                    item_to_update.OrderItemProduct.ProductDesc = obj.ProductDesc;
                                    item_to_update.OrderItemProduct.ProductName = obj.ProductName;
                                    item_to_update.OrderItemBillToAddress.AddressKey = obj.OrderItemBillToAddress.AddressKey;
                                    item_to_update.OrderItemShipToAddress.AddressKey = obj.OrderItemShipToAddress.AddressKey;
                                    item_to_update.SalesRep.EntityPersonKey = obj.SalesRep.EntityPersonKey;
                                    item_to_update.AccountRep.EntityPersonKey = obj.AccountRep.EntityPersonKey;
                                    item_to_update.OrderItemShipDate = obj.OrderItemShipDate;
                                    item_to_update.OrderItemCompleteDate = obj.OrderItemCompleteDate;
                                }
                                else
                                {
                                    Order.OrderItems.Add(new OrderItemWrapper(obj));
                                }
                                UpdateItemTotals();
                            }
                        }
                    });
                GridIsEnabled = true;
            }
        }

        private void PopulateProductInfo(object param)
        {
            var orderItem = Order.OrderItems[SelectedOrderItemIndex];
            if (orderItem != null && orderItem.ProductKey > 0)
            {
                var sp = ProductList.Where(item => item.ProductKey == orderItem.ProductKey).FirstOrDefault();
                //MessageToDisplay = order_item.ProductKey.ToString() + ": " + sp[0].ProductName;

                if (orderItem.ProductName == "" || orderItem.ProductName == null || orderItem.ProductName != sp.ProductName)
                {
                    if (sp.ProductName != "")
                    {
                        var rp = sp.ProductAttributes.Where(item => item.AttributeType == QIQOAttributeType.Product_PRODBASE).FirstOrDefault();
                        var dq = sp.ProductAttributes.Where(item => item.AttributeType == QIQOAttributeType.Product_PRODDFQTY).FirstOrDefault();

                        //var.ProductKey = sp[0].ProductKey;
                        // order_item.ProductKey = sp.ProductKey;
                        orderItem.ProductCode = sp.ProductCode;
                        orderItem.ProductName = sp.ProductName;
                        orderItem.ProductDesc = sp.ProductDesc;
                        orderItem.OrderItemQuantity = int.Parse(dq.AttributeValue);
                        // Check for Fee Schedule here!
                        decimal fsp = ApplyFeeSchedule(sp.ProductKey, decimal.Parse(rp.AttributeValue));
                        orderItem.ItemPricePer = (fsp != 0M) ? fsp : decimal.Parse(rp.AttributeValue);
                        orderItem.OrderItemLineSum = orderItem.OrderItemQuantity * orderItem.ItemPricePer;
                        
                        //order_item.OrderItemProduct = new ProductWrapper(sp);
                        orderItem.OrderItemProduct.ProductKey = sp.ProductKey;
                        orderItem.OrderItemProduct.ProductCode = sp.ProductCode;
                        orderItem.OrderItemProduct.ProductDesc = sp.ProductDesc;
                        orderItem.OrderItemProduct.ProductName = sp.ProductName;
                        orderItem.OrderItemProduct.ProductType = sp.ProductType;

                        //order_item.OrderItemBillToAddress = DefaultBillingAddress;
                        FillFromDefaultAddress(orderItem, QIQOAddressType.Billing);

                        //order_item.OrderItemShipToAddress = DefaultShippingAddress;
                        FillFromDefaultAddress(orderItem, QIQOAddressType.Shipping);

                        orderItem.AccountRep.EntityPersonKey = _accountReps[0].EntityPersonKey;
                        orderItem.SalesRep.EntityPersonKey = _salesReps[0].EntityPersonKey;
                        orderItem.OrderItemSeq = SelectedOrderItemIndex + 1;
                    }
                }
            }

            Order.OrderItemCount = Order.OrderItems.Sum(item => item.OrderItemQuantity);
            Order.OrderValueSum = Order.OrderItems.Sum(item => item.OrderItemLineSum);
            int seq = Order.OrderItems.Count;
            // Need to think about whether this is the best way to do this. What if they change an existing item?
            if (Order.OrderItems.Where(item => item.ProductKey == 0).FirstOrDefault() == null)
            {
                OrderItemWrapper new_item = new OrderItemWrapper(InitNewOrderItem(seq + 1));
                FillFromDefaultAddress(new_item, QIQOAddressType.Billing);
                FillFromDefaultAddress(new_item, QIQOAddressType.Shipping);
                Order.OrderItems.Add(new_item);
            }
        }

        private void FillFromDefaultAddress(OrderItemWrapper orderItem, QIQOAddressType addressType)
        {
            if (addressType == QIQOAddressType.Billing)
            {
                if (DefaultBillingAddress != null)
                {
                    orderItem.OrderItemBillToAddress.AddressKey = DefaultBillingAddress.AddressKey;
                    orderItem.OrderItemBillToAddress.AddressType = QIQOAddressType.Billing;
                    orderItem.OrderItemBillToAddress.AddressLine1 = DefaultBillingAddress.AddressLine1;
                    orderItem.OrderItemBillToAddress.AddressLine2 = DefaultBillingAddress.AddressLine2;
                    orderItem.OrderItemBillToAddress.AddressCity = DefaultBillingAddress.AddressCity;
                    orderItem.OrderItemBillToAddress.AddressState = DefaultBillingAddress.AddressState;
                    orderItem.OrderItemBillToAddress.AddressPostalCode = DefaultBillingAddress.AddressPostalCode;
                }
            }
            else
            {
                if (DefaultShippingAddress != null)
                {
                    orderItem.OrderItemShipToAddress.AddressKey = DefaultShippingAddress.AddressKey;
                    orderItem.OrderItemShipToAddress.AddressType = QIQOAddressType.Shipping;
                    orderItem.OrderItemShipToAddress.AddressLine1 = DefaultShippingAddress.AddressLine1;
                    orderItem.OrderItemShipToAddress.AddressLine2 = DefaultShippingAddress.AddressLine2;
                    orderItem.OrderItemShipToAddress.AddressCity = DefaultShippingAddress.AddressCity;
                    orderItem.OrderItemShipToAddress.AddressState = DefaultShippingAddress.AddressState;
                    orderItem.OrderItemShipToAddress.AddressPostalCode = DefaultShippingAddress.AddressPostalCode;
                }
            }
        }

        private decimal ApplyFeeSchedule(int productKey, decimal defaultPrice) // think about if this needs to be in a service
        {
            decimal charge = 0M; string type;

            if (FeeScheduleList != null)
            {
                var fs = FeeScheduleList.Where(item => item.ProductKey == productKey).FirstOrDefault();
                if (fs != null)
                {
                    charge = fs.FeeScheduleValue;
                    type = fs.FeeScheduleTypeCode;
                    if (type == "P")
                        charge = defaultPrice * charge;
                }
            }
            else
                charge = defaultPrice;

            return charge;
        }

        private void UpdateItemTotals()
        {
            if (SelectedOrderItemIndex != -1 && Order.OrderItems.Count > 0)
            {
                var orderItem = Order.OrderItems[SelectedOrderItemIndex];
                if (orderItem != null & orderItem.OrderItemStatus != QIQOOrderItemStatus.Canceled)
                {
                    if (orderItem.ItemPricePer <= 0)
                    {
                        if (orderItem.OrderItemProduct != null)
                        {
                            var rp = orderItem.OrderItemProduct.ProductAttributes.Where(item => item.AttributeType == QIQOAttributeType.Product_PRODBASE).FirstOrDefault();
                            if (rp != null)
                                orderItem.ItemPricePer = ApplyFeeSchedule(orderItem.ProductKey, decimal.Parse(rp.AttributeValue));
                        }
                    }
                    if (orderItem.OrderItemQuantity <= 0)
                    {
                        if (orderItem.OrderItemProduct != null)
                        {
                            var dq = orderItem.OrderItemProduct.ProductAttributes.Where(item => item.AttributeType == QIQOAttributeType.Product_PRODDFQTY).FirstOrDefault();
                            if (dq != null)
                                orderItem.OrderItemQuantity = int.Parse(dq.AttributeValue);
                        }
                    }

                    orderItem.OrderItemLineSum = orderItem.OrderItemQuantity * orderItem.ItemPricePer;
                }
            }
            Order.OrderItemCount = Order.OrderItems.Sum(item => item.OrderItemQuantity);
            Order.OrderValueSum = Order.OrderItems.Sum(item => item.OrderItemLineSum);
            Order.OrderItems.ForEach(item => item.OrderItemSeq = Order.OrderItems.IndexOf(item) + 1);
        }

        private void UpdateHeaderFromDetail()
        {
            if (SelectedOrderItemIndex != -1 && Order.OrderItems.Count > 0)
            {
                var orderItem = Order.OrderItems[SelectedOrderItemIndex];
                if (orderItem != null)
                {
                    if (orderItem.OrderItemBillToAddress is AddressWrapper current_bill_address && current_bill_address.AddressKey != 0 && current_bill_address.AddressLine1 != null)
                        DefaultBillingAddress = current_bill_address;

                    if (orderItem.OrderItemShipToAddress is AddressWrapper current_ship_address && current_ship_address.AddressKey != 0 && current_ship_address.AddressLine1 != null)
                        DefaultShippingAddress = current_ship_address;

                    if (orderItem.SalesRep is RepresentativeWrapper current_sales_rep && current_sales_rep.EntityPersonKey != 0) Order.SalesRep.EntityPersonKey = current_sales_rep.EntityPersonKey;

                    if (orderItem.AccountRep is RepresentativeWrapper current_account_rep && current_account_rep.EntityPersonKey != 0) Order.AccountRep.EntityPersonKey = current_account_rep.EntityPersonKey;
                }
            }
        }

        private bool CanGetAccount(string accountCode)
        {
            return accountCode.Length > 2;
        }

        private void GetAccount(string accountCode)
        {
            //throw new NotImplementedException();
            ExecuteFaultHandledOperation(() =>
            {
                var accountService = _serviceFactory.CreateClient<IAccountService>();
                var comp = CurrentCompany as Company;

                using (accountService)
                {
                    Client.Entities.Account account = accountService.GetAccountByCode(accountCode, comp.CompanyCode);
                    if (account != null)
                    {
                        if (account.Employees != null)
                            AccountContacts = new ObservableCollection<AccountPerson>(account.Employees.Where(item => item.CompanyRoleType == QIQOPersonType.AccountContact).ToList());
                        // Get the accounts main contact key
                        var contact = account.Employees.Where(item => item.CompanyRoleType == QIQOPersonType.AccountContact).FirstOrDefault();
                        int cnt_key = contact != null ? contact.EntityPersonKey : 1;
                        
                        Order.Account.AccountKey = account.AccountKey;
                        Order.Account.AccountName = account.AccountName;
                        Order.Account.AccountCode = account.AccountCode;
                        Order.Account.AccountDBA = account.AccountDBA;
                        Order.Account.AccountDesc = account.AccountDesc;
                        Order.AccountKey = account.AccountKey;
                        Order.Account.AccountType = account.AccountType;
                        Order.AccountContactKey = cnt_key; // account.Employees[0].EntityPersonKey;
                        Order.OrderAccountContact.PersonFullNameFML = contact != null ? contact.PersonFullNameFML : "N/A";
                        Order.AccountRepKey = AccountRepList[0].EntityPersonKey;
                        Order.SalesRepKey = SalesRepList[0].EntityPersonKey;
                        DefaultBillingAddress = new AddressWrapper(account.Addresses.Where(item => item.AddressType == QIQOAddressType.Billing).FirstOrDefault());
                        DefaultShippingAddress = new AddressWrapper(account.Addresses.Where(item => item.AddressType == QIQOAddressType.Shipping).FirstOrDefault());
                        FeeScheduleList = new ObservableCollection<FeeSchedule>(account.FeeSchedules);
                        _currentAccount = account;
                        RaisePropertyChanged(nameof(Order));
                        GridIsEnabled = true;
                    }
                    else
                    {
                        DisplayErrorMessage($"Account with code '{accountCode}' not found");
                    }
                }
            });
            _eventAggregator.GetEvent<NavigationEvent>().Publish(ViewNames.OrderHomeView);
        }

        private bool CanDoSave()
        {
            return Order.IsChanged && Order.IsValid;
        }

        private void DoSave()
        {
            _eventAggregator.GetEvent<GeneralMessageEvent>().Publish(ApplicationStrings.BeginningSave);
            ExecuteFaultHandledOperation(() =>
            {
                var orderService = _serviceFactory.CreateClient<IOrderService>();
                using (var scope = new TransactionScope())
                {
                    using (orderService)
                    {
                        if (Order.OrderKey == 0)
                        {
                            var accountService = _serviceFactory.CreateClient<IAccountService>();
                            Order.OrderNumber = accountService.GetAccountNextNumber(Order.Account.Model, QIQOEntityNumberType.OrderNumber);
                            accountService.Dispose();
                        }

                        //TODO: Do something to make sure the order items are in the object properly
                        var new_order_line = Order.OrderItems.Where(item => item.ProductKey == 0).FirstOrDefault();
                        if (new_order_line != null)
                            Order.OrderItems.Remove(new_order_line);

                        // For some reason, these don't seem to get set properly when I add the account object to the Order object
                        Order.Model.OrderItems.ForEach(item => item.OrderItemBillToAddress = DefaultBillingAddress.Model);
                        Order.Model.OrderItems.ForEach(item => item.OrderItemShipToAddress = DefaultShippingAddress.Model);
                        int order_key = orderService.CreateOrder(Order.Model);
                        if (Order.OrderKey == 0) Order.OrderKey = order_key;
                        Order.AcceptChanges();
                        ViewTitle = Order.OrderNumber;                        
                    }
                    scope.Complete();
                }
            });
            if (Order.OrderKey > 0)
            {
                _eventAggregator.GetEvent<OrderUpdatedEvent>().Publish($"Order {Order.OrderNumber} updated successfully");
                _workingOrderService.CloseOrder(Order);
                _regionManager.RequestNavigate(RegionNames.ContentRegion, ViewNames.OrderHomeView);
            }
        }
    }
}
