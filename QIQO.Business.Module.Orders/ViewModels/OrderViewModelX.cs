using Microsoft.Practices.ObjectBuilder2;
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

namespace QIQO.Business.Module.Orders.ViewModels
{
    public class OrderViewModelX : ViewModelBase, IRegionMemberLifetime, IConfirmNavigationRequest //, IInteractionRequestAware
    {
        private readonly IEventAggregator event_aggregator;
        private readonly IServiceFactory service_factory;
        private readonly IProductListService _product_service;
        private readonly IRegionManager region_manager;
        private readonly IWorkingOrderService working_order_service;
        private IReportService report_service;
        private OrderWrapper _order;
        private Client.Entities.Account _currentAccount;

        private object _selected_order_item;
        private AddressWrapper _ship_address;
        private AddressWrapper _bill_address;
        private ObservableCollection<Product> _productlist;
        private ObservableCollection<Representative> _accountreps;
        private ObservableCollection<Representative> _salesreps;
        private ObservableCollection<FeeSchedule> _feeschedulelist;
        private ObservableCollection<AccountPerson> _account_contacts;
        private string _viewTitle = ApplicationStrings.TabTitleNewOrder;
        private bool _grid_enabled = true;
        //private ItemEditNotification notification;

        public OrderViewModelX(IEventAggregator event_aggtr, IServiceFactory service_fctry, 
            IProductListService product_service, IRegionManager region_mgr,
            IReportService reportService, IWorkingOrderService working_order_svc) //
        {
            event_aggregator = event_aggtr;
            service_factory = service_fctry;
            _product_service = product_service;
            region_manager = region_mgr;
            report_service = reportService;
            working_order_service = working_order_svc;

            GetProductList();
            BindCommands();
            GetCompanyRepLists();
            //InitNewOrder();

            RegisterApplicationCommands();
            IsActive = true;
            IsActiveChanged += OrderViewModel_IsActiveChanged;
            //event_aggregator.GetEvent<OrderUnloadingEvent>().Subscribe(ParentViewUnloadingEvent);
            event_aggregator.GetEvent<OrderLoadedEvent>().Publish(string.Empty);
        }

        //private void ParentViewUnloadingEvent(object obj)
        //{
        //    bool canClose = true;
        //    var navContext = obj as NavigationContext;
        //    if (navContext != null)
        //    {
        //        ConfirmNavigationRequest(navContext, result =>
        //        {
        //            canClose = result;
        //        });

        //        if (canClose)
        //        {
        //            OnNavigatedFrom(null);
        //        }
        //    }
        //}

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

        //public bool KeepAlive => Order.IsChanged;

        public OrderWrapper Order
        {
            get { return _order; }
            private set { SetProperty(ref _order, value); }
        }

        public AddressWrapper DefaultBillingAddress
        {
            get { return _bill_address; }
            private set { SetProperty(ref _bill_address, value); }
        }
        public AddressWrapper DefaultShippingAddress
        {
            get { return _ship_address; }
            private set { SetProperty(ref _ship_address, value); }
        }

        public ObservableCollection<AccountPerson> AccountContacts
        {
            get { return _account_contacts; }
            private set { SetProperty(ref _account_contacts, value); }
        }

        public ObservableCollection<Product> ProductList
        {
            get { return _productlist; }
            private set { SetProperty(ref _productlist, value); }
        }
        public ObservableCollection<Representative> AccountRepList
        {
            get { return _accountreps; }
            private set { SetProperty(ref _accountreps, value); }
        }
        public ObservableCollection<Representative> SalesRepList
        {
            get { return _salesreps; }
            private set { SetProperty(ref _salesreps, value); }
        }
        public ObservableCollection<FeeSchedule> FeeScheduleList
        {
            get { return _feeschedulelist; }
            private set { SetProperty(ref _feeschedulelist, value); }
        }

        public object SelectedOrderItem
        {
            get { return _selected_order_item; }
            set
            {
                SetProperty(ref _selected_order_item, value);
                InvalidateCommands();
            }
        }

        public bool GridIsEnabled
        {
            get { return _grid_enabled; }
            private set { SetProperty(ref _grid_enabled, value); }
        }

        public override string ViewTitle { get { return _viewTitle; }
            protected set { SetProperty(ref _viewTitle, value); }
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            //RunSomething();
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
                Order = working_order_service.GetOrder((string)navigationContext.Parameters["OrderNumber"]);
                GetAccount(Order.Account.AccountCode);
                event_aggregator.GetEvent<OrderLoadedEvent>().Publish(Order.OrderNumber);
                return;
            }

            InitNewOrder();
        }

        //private async void RunSomething()
        //{
        //    await Task.Run(() => { Console.WriteLine("---****  I am running something!!"); });
        //}

        public override bool IsNavigationTarget(NavigationContext navigationContext)
        {
            //return true;
            var paramAccountCode = navigationContext.Parameters.Where(item => item.Key == "AccountCode").FirstOrDefault();
            var paramOrderNumber = navigationContext.Parameters.Where(item => item.Key == "OrderKey").FirstOrDefault();

            return (paramAccountCode.Value != null || paramOrderNumber.Value != null) ? true : false;
            //return navigationContext.Parameters.Conta ? true : false;
        }

        private void RegisterApplicationCommands()
        {
            //ApplicationCommands.SaveOrderCommand.RegisterCommand(SaveCommand);
            //ApplicationCommands.DeleteOrderCommand.RegisterCommand(DeleteCommand);
            //ApplicationCommands.CancelOrderCommand.RegisterCommand(CancelCommand);
            //ApplicationCommands.PrintOrderCommand.RegisterCommand(PrintCommand);
        }

        public override void OnNavigatedFrom(NavigationContext navigationContext)
        {
            //ApplicationCommands.SaveOrderCommand.UnregisterCommand(SaveCommand);
            //ApplicationCommands.DeleteOrderCommand.UnregisterCommand(DeleteCommand);
            //ApplicationCommands.CancelOrderCommand.UnregisterCommand(CancelCommand);
            //ApplicationCommands.PrintOrderCommand.UnregisterCommand(PrintCommand);
            //event_aggregator.GetEvent<OrderUnloadingEvent>().Unsubscribe(ParentViewUnloadingEvent);
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

        //public INotification Notification
        //{
        //    get { return notification; }

        //    set
        //    {
        //        if (value is ItemEditNotification)
        //        {
        //            notification = value as ItemEditNotification;
        //            var passed_object = notification.EditibleObject as OrderWrapper;
        //            if (passed_object != null)
        //            {
        //                GetOrder(passed_object.OrderKey);
        //            }
        //            OnPropertyChanged(() => Notification);
        //        }
        //    }
        //}

        //public Action FinishInteraction { get; set; }

        //public bool KeepAlive => false;

        protected override void DisplayErrorMessage(Exception ex, [CallerMemberName] string methodName = "")
        {
            event_aggregator.GetEvent<GeneralErrorEvent>().Publish(methodName + " - " + ex.Message);
        }
        protected void DisplayErrorMessage(string msg)
        {
            event_aggregator.GetEvent<GeneralErrorEvent>().Publish(msg);
        }

        private void InitNewOrder()
        {
            Order new_order = new Order() //*** GET this initializatoin stuff into the objects themselves!! (complete)
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
            working_order_service.OpenOrder(Order);

            GridIsEnabled = false;
            //event_aggregator.GetEvent<OrderNewOrderAddEvent>().Publish(ViewNames.OrderHomeView);
        }

        private OrderItem InitNewOrderItem(int order_item_seq)
        {
            return new OrderItem()
            {
                //OrderItemSeq = order_item_seq,
                SalesRep = SalesRepList[0],
                AccountRep = AccountRepList[0]
            };
        }

        private void GetOrder(int order_key)
        {
            ExecuteFaultHandledOperation(() =>
            {
                IOrderService order_service = service_factory.CreateClient<IOrderService>();
                using (order_service)
                {
                    Order = new OrderWrapper(order_service.GetOrder(order_key));
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
                //AccountContacts = new ObservableCollection<AccountPerson>(Order.Account.Model.Employees.Where(item => 
                //                                                            item.CompanyRoleType == QIQOPersonType.AccountContact).ToList());
                event_aggregator.GetEvent<GeneralMessageEvent>().Publish($"Order {Order.OrderNumber} loaded successfully");
                event_aggregator.GetEvent<OrderLoadedEvent>().Publish(Order.OrderNumber);
                event_aggregator.GetEvent<NavigationEvent>().Publish(ViewNames.OrderHomeView);
            });
        }

        private void GetCompanyRepLists()
        {
            IEmployeeService employee_service = service_factory.CreateClient<IEmployeeService>();
            using (employee_service)
            {
                AccountRepList = new ObservableCollection<Representative>(employee_service.GetAccountRepsByCompany(CurrentCompanyKey));
                SalesRepList = new ObservableCollection<Representative>(employee_service.GetSalesRepsByCompany(CurrentCompanyKey));
            }

        }

        private void GetProductList()
        {
            ProductList = new ObservableCollection<Product>(_product_service.ProductList);
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
            report_service.ExecuteReport(Reports.Order, $"order_key={Order.OrderKey.ToString()}");
        }

        private void NewAccount()
        {
            //region_manager.RequestNavigate(RegionNames.RibbonRegion, ViewNames.AccountRibbonView);
            region_manager.RequestNavigate(RegionNames.ContentRegion, ViewNames.AccountView);
        }

        private bool CanDoCancel()
        {
            return Order.IsChanged;
        }

        private void DoCancel()
        {
            working_order_service.CloseOrder(Order);
            //event_aggregator.GetEvent<OrderNewOrderCancelEvent>().Publish(ViewNames.OrderHomeView);
            region_manager.RequestNavigate(RegionNames.ContentRegion, ViewNames.OrderHomeView);
            //Confirmation confirm = new Confirmation();
            //    confirm.Title = ApplicationStrings.ConfirmCancelTitle;
            //    confirm.Content = ApplicationStrings.ConfirmCancelPrompt;
            //    SaveChangesConfirmationRequest.Raise(confirm,
            //        r => {
            //            if (r != null && r.Confirmed)
            //            {
            //                //InitNewOrder();

            //            }
            //        });
        }

        private bool CanDoDelete()
        {
            return Order.OrderKey > 0;
        }

        private void DoDelete()
        {
            Confirmation confirm = new Confirmation();
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

        private void DeleteOrder(string order_number)
        {
            ExecuteFaultHandledOperation(() =>
            {
                IOrderService order_service = service_factory.CreateClient<IOrderService>();
                using (TransactionScope scope = new TransactionScope())
                {
                    using (order_service)
                    {
                        bool ret_val = order_service.DeleteOrder(Order.Model);
                        //InitNewOrder();
                        //ViewTitle = ApplicationStrings.TabTitleNewOrder;
                        event_aggregator.GetEvent<OrderDeletedEvent>().Publish($"Order {order_number} deleted successfully");
                        //event_aggregator.GetEvent<OrderNewOrderCancelEvent>().Publish(ViewNames.OrderHomeView);
                        region_manager.RequestNavigate(RegionNames.ContentRegion, ViewNames.OrderHomeView);
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
            ItemSelectionNotification notification = new ItemSelectionNotification();
            notification.Title = ApplicationStrings.NotificationFindAccount;
            FindAccountRequest.Raise(notification,
                r => {
                    if (r != null && r.Confirmed && r.SelectedItem != null)
                    {
                        Client.Entities.Account found_account = r.SelectedItem as Client.Entities.Account;
                        if (found_account != null)
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
            var item_to_del = SelectedOrderItem as OrderItemWrapper;
            if (item_to_del != null)
            {
                if (item_to_del.OrderItemKey == 0)
                    Order.OrderItems.Remove(item_to_del);
                else
                {
                    item_to_del.OrderItemStatus = QIQOOrderItemStatus.Canceled;
                    item_to_del.OrderItemQuantity = 0;
                    item_to_del.OrderItemLineSum = 0M;
                    item_to_del.ItemPricePer = 0M;
                    item_to_del.ProductDesc = item_to_del.ProductDesc + " (Deleted)";
                    item_to_del.OrderItemCompleteDate = DateTime.Now;
                }
                UpdateItemTotals();
                //InvalidateCommands();
            }
        }

        private bool CanEditOrderItem()
        {
            return (SelectedOrderItem != null && ((OrderItemWrapper)SelectedOrderItem).ProductKey > 0);
        }

        private void EditOrderItem()
        {
            var item_to_edit = SelectedOrderItem as OrderItemWrapper;
            if (item_to_edit != null)
                ChangeOrderItem(item_to_edit.Model.Copy(), ApplicationStrings.NotificationEdit);
        }

        private bool CanAddOrderItem()
        {
            return Order.Account.AccountKey != 0;
        }

        private void AddOrderItem()
        {
            var existing_empty_item = Order.OrderItems.Where(i => i.ProductCode == null).FirstOrDefault().Model;
            if (existing_empty_item != null)
            {
                existing_empty_item.OrderItemQuantity = 1;
                existing_empty_item.OrderKey = Order.OrderKey;
                existing_empty_item.AccountRep = Order.AccountRep.Model;
                existing_empty_item.SalesRep = Order.SalesRep.Model;
                existing_empty_item.OrderItemBillToAddress = DefaultBillingAddress.Model;
                existing_empty_item.OrderItemShipToAddress = DefaultShippingAddress.Model;
                ChangeOrderItem(existing_empty_item, ApplicationStrings.NotificationEdit);
            }
            else
            {
                OrderItem new_ord_item = new OrderItem()
                {
                    AccountRep = Order.AccountRep.Model,
                    OrderItemBillToAddress = DefaultBillingAddress.Model,
                    //OrderItemSeq = Order.OrderItems.Max(item => item.OrderItemSeq) + 1,
                    OrderItemQuantity = 1,
                    OrderItemShipToAddress = DefaultShippingAddress.Model,
                    OrderKey = Order.OrderKey,
                    SalesRep = Order.SalesRep.Model
                };
                ChangeOrderItem(new_ord_item, ApplicationStrings.NotificationAdd);
            }
        }

        private void ChangeOrderItem(OrderItem order_item, string action)
        {
            //var item_to_edit = order_item as OrderItem;
            if (order_item != null)
            {
                GridIsEnabled = false;
                var bill_addresses = _currentAccount.Addresses.Where(item => item.AddressType == QIQOAddressType.Billing).ToList();
                var ship_addresses = _currentAccount.Addresses.Where(item => item.AddressType == QIQOAddressType.Shipping).ToList();
                Tuple<object, object, object> needed_objects = new Tuple<object, object, object>(order_item, bill_addresses, ship_addresses);
                ItemEditNotification notification = new ItemEditNotification(needed_objects);
                notification.Title = action + " Order Item"; //+ emp_to_edit.PersonCode + " - " + emp_to_edit.PersonFullNameFML;
                EditOrderItemRequest.Raise(notification,
                    r =>
                    {
                        if (r != null && r.Confirmed && r.EditibleObject != null) // 
                        {
                            OrderItem obj = r.EditibleObject as OrderItem;
                            if (obj != null)
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
                                    item_to_update.OrderItemLineSum = order_item.OrderItemQuantity * order_item.ItemPricePer;
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
            OrderItemWrapper order_item = Order.OrderItems[SelectedOrderItemIndex];
            //OrderItem order_item = OrderItems[SelectedOrderItemIndex];
            if (order_item != null && order_item.ProductKey > 0)
            {
                var sp = ProductList.Where(item => item.ProductKey == order_item.ProductKey).FirstOrDefault();
                //MessageToDisplay = order_item.ProductKey.ToString() + ": " + sp[0].ProductName;

                if (order_item.ProductName == "" || order_item.ProductName == null || order_item.ProductName != sp.ProductName)
                {
                    if (sp.ProductName != "")
                    {
                        var rp = sp.ProductAttributes.Where(item => item.AttributeType == QIQOAttributeType.Product_PRODBASE).FirstOrDefault();
                        var dq = sp.ProductAttributes.Where(item => item.AttributeType == QIQOAttributeType.Product_PRODDFQTY).FirstOrDefault();

                        //var.ProductKey = sp[0].ProductKey;
                        // order_item.ProductKey = sp.ProductKey;
                        order_item.ProductCode = sp.ProductCode;
                        order_item.ProductName = sp.ProductName;
                        order_item.ProductDesc = sp.ProductDesc;
                        order_item.OrderItemQuantity = int.Parse(dq.AttributeValue);
                        // Check for Fee Schedule here!
                        decimal fsp = ApplyFeeSchedule(sp.ProductKey, decimal.Parse(rp.AttributeValue));
                        order_item.ItemPricePer = (fsp != 0M) ? fsp : decimal.Parse(rp.AttributeValue);
                        order_item.OrderItemLineSum = order_item.OrderItemQuantity * order_item.ItemPricePer;
                        
                        //order_item.OrderItemProduct = new ProductWrapper(sp);
                        order_item.OrderItemProduct.ProductKey = sp.ProductKey;
                        order_item.OrderItemProduct.ProductCode = sp.ProductCode;
                        order_item.OrderItemProduct.ProductDesc = sp.ProductDesc;
                        order_item.OrderItemProduct.ProductName = sp.ProductName;
                        order_item.OrderItemProduct.ProductType = sp.ProductType;

                        //order_item.OrderItemBillToAddress = DefaultBillingAddress;
                        FillFromDefaultAddress(order_item, QIQOAddressType.Billing);

                        //order_item.OrderItemShipToAddress = DefaultShippingAddress;
                        FillFromDefaultAddress(order_item, QIQOAddressType.Shipping);

                        order_item.AccountRep.EntityPersonKey = _accountreps[0].EntityPersonKey;
                        order_item.SalesRep.EntityPersonKey = _salesreps[0].EntityPersonKey;
                        order_item.OrderItemSeq = SelectedOrderItemIndex + 1;
                    }
                }
            }

            Order.OrderItemCount = Order.OrderItems.Sum(item => item.OrderItemQuantity);
            Order.OrderValueSum = Order.OrderItems.Sum(item => item.OrderItemLineSum);
            int seq = Order.OrderItems.Count;
            // Need to think about whether this is the best way to do this. What if they change an existing item?
            var new_order_line = Order.OrderItems.Where(item => item.ProductKey == 0).FirstOrDefault();
            if (new_order_line == null)
            {
                OrderItemWrapper new_item = new OrderItemWrapper(InitNewOrderItem(seq + 1));
                FillFromDefaultAddress(new_item, QIQOAddressType.Billing);
                FillFromDefaultAddress(new_item, QIQOAddressType.Shipping);
                Order.OrderItems.Add(new_item);
            }
        }

        private void FillFromDefaultAddress(OrderItemWrapper order_item, QIQOAddressType addr_type)
        {
            if (addr_type == QIQOAddressType.Billing)
            {
                if (DefaultBillingAddress != null)
                {
                    order_item.OrderItemBillToAddress.AddressKey = DefaultBillingAddress.AddressKey;
                    order_item.OrderItemBillToAddress.AddressType = QIQOAddressType.Billing;
                    order_item.OrderItemBillToAddress.AddressLine1 = DefaultBillingAddress.AddressLine1;
                    order_item.OrderItemBillToAddress.AddressLine2 = DefaultBillingAddress.AddressLine2;
                    order_item.OrderItemBillToAddress.AddressCity = DefaultBillingAddress.AddressCity;
                    order_item.OrderItemBillToAddress.AddressState = DefaultBillingAddress.AddressState;
                    order_item.OrderItemBillToAddress.AddressPostalCode = DefaultBillingAddress.AddressPostalCode;
                }
            }
            else
            {
                if (DefaultShippingAddress != null)
                {
                    order_item.OrderItemShipToAddress.AddressKey = DefaultShippingAddress.AddressKey;
                    order_item.OrderItemShipToAddress.AddressType = QIQOAddressType.Shipping;
                    order_item.OrderItemShipToAddress.AddressLine1 = DefaultShippingAddress.AddressLine1;
                    order_item.OrderItemShipToAddress.AddressLine2 = DefaultShippingAddress.AddressLine2;
                    order_item.OrderItemShipToAddress.AddressCity = DefaultShippingAddress.AddressCity;
                    order_item.OrderItemShipToAddress.AddressState = DefaultShippingAddress.AddressState;
                    order_item.OrderItemShipToAddress.AddressPostalCode = DefaultShippingAddress.AddressPostalCode;
                }
            }
        }

        private decimal ApplyFeeSchedule(int product_key, decimal def_price) // think about if this needs to be in a service
        {
            decimal charge = 0M; string type;

            if (FeeScheduleList != null)
            {
                var fs = FeeScheduleList.Where(item => item.ProductKey == product_key).FirstOrDefault();
                if (fs != null)
                {
                    charge = fs.FeeScheduleValue;
                    type = fs.FeeScheduleTypeCode;
                    if (type == "P")
                        charge = def_price * charge;
                }
            }
            else
                charge = def_price;

            return charge;
        }

        private void UpdateItemTotals()
        {
            if (SelectedOrderItemIndex != -1 && Order.OrderItems.Count > 0)
            {
                OrderItemWrapper order_item = Order.OrderItems[SelectedOrderItemIndex];
                //OrderItem order_item = OrderItems[SelectedOrderItemIndex];
                if (order_item != null & order_item.OrderItemStatus != QIQOOrderItemStatus.Canceled)
                {
                    if (order_item.ItemPricePer <= 0)
                    {
                        if (order_item.OrderItemProduct != null)
                        {
                            var rp = order_item.OrderItemProduct.ProductAttributes.Where(item => item.AttributeType == QIQOAttributeType.Product_PRODBASE).FirstOrDefault();
                            if (rp != null)
                                order_item.ItemPricePer = ApplyFeeSchedule(order_item.ProductKey, decimal.Parse(rp.AttributeValue));
                        }
                    }
                    if (order_item.OrderItemQuantity <= 0)
                    {
                        if (order_item.OrderItemProduct != null)
                        {
                            var dq = order_item.OrderItemProduct.ProductAttributes.Where(item => item.AttributeType == QIQOAttributeType.Product_PRODDFQTY).FirstOrDefault();
                            if (dq != null)
                                order_item.OrderItemQuantity = int.Parse(dq.AttributeValue);
                        }
                    }

                    order_item.OrderItemLineSum = order_item.OrderItemQuantity * order_item.ItemPricePer;
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
                OrderItemWrapper order_item = Order.OrderItems[SelectedOrderItemIndex];
                //OrderItem order_item = OrderItems[SelectedOrderItemIndex];
                if (order_item != null)
                {
                    AddressWrapper current_bill_address = order_item.OrderItemBillToAddress as AddressWrapper;
                    if (current_bill_address != null && current_bill_address.AddressKey != 0 && current_bill_address.AddressLine1 != null)
                        DefaultBillingAddress = current_bill_address;

                    AddressWrapper current_ship_address = order_item.OrderItemShipToAddress as AddressWrapper;
                    if (current_ship_address != null && current_ship_address.AddressKey != 0 && current_ship_address.AddressLine1 != null)
                        DefaultShippingAddress = current_ship_address;

                    RepresentativeWrapper current_sales_rep = order_item.SalesRep as RepresentativeWrapper;
                    if (current_sales_rep != null && current_sales_rep.EntityPersonKey != 0) Order.SalesRep.EntityPersonKey = current_sales_rep.EntityPersonKey;

                    RepresentativeWrapper current_account_rep = order_item.AccountRep as RepresentativeWrapper;
                    if (current_account_rep != null && current_account_rep.EntityPersonKey != 0) Order.AccountRep.EntityPersonKey = current_account_rep.EntityPersonKey;
                }
            }
        }

        private bool CanGetAccount(string account_code)
        {
            return account_code.Length > 2;
        }

        private void GetAccount(string account_code)
        {
            //throw new NotImplementedException();
            ExecuteFaultHandledOperation(() =>
            {
                IAccountService acct_service = service_factory.CreateClient<IAccountService>();
                var comp = CurrentCompany as Company;

                using (acct_service)
                {
                    Client.Entities.Account account = acct_service.GetAccountByCode(account_code, comp.CompanyCode);
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
                        OnPropertyChanged(nameof(Order));
                        GridIsEnabled = true;
                    }
                    else
                    {
                        DisplayErrorMessage($"Account with code '{account_code}' not found");
                    }
                }
            });
            event_aggregator.GetEvent<NavigationEvent>().Publish(ViewNames.OrderHomeView);
        }

        private bool CanDoSave()
        {
            return Order.IsChanged && Order.IsValid;
        }

        private void DoSave()
        {
            event_aggregator.GetEvent<GeneralMessageEvent>().Publish(ApplicationStrings.BeginningSave);
            ExecuteFaultHandledOperation(() =>
            {
                IOrderService order_service = service_factory.CreateClient<IOrderService>();
                using (TransactionScope scope = new TransactionScope())
                {
                    using (order_service)
                    {
                        if (Order.OrderKey == 0)
                        {
                            IAccountService account_service = service_factory.CreateClient<IAccountService>();
                            Order.OrderNumber = account_service.GetAccountNextNumber(Order.Account.Model, QIQOEntityNumberType.OrderNumber);
                            account_service.Dispose();
                        }

                        //TODO: Do something to make sure the order items are in the object properly
                        var new_order_line = Order.OrderItems.Where(item => item.ProductKey == 0).FirstOrDefault();
                        if (new_order_line != null)
                            Order.OrderItems.Remove(new_order_line);

                        // For some reason, these don't seem to get set properly when I add the account object to the Order object
                        Order.Model.OrderItems.ForEach(item => item.OrderItemBillToAddress = DefaultBillingAddress.Model);
                        Order.Model.OrderItems.ForEach(item => item.OrderItemShipToAddress = DefaultShippingAddress.Model);
                        int order_key = order_service.CreateOrder(Order.Model);
                        if (Order.OrderKey == 0) Order.OrderKey = order_key;
                        Order.AcceptChanges();
                        ViewTitle = Order.OrderNumber;
                        
                        //event_aggregator.GetEvent<OrderUpdatedEvent>().Publish($"Order {Order.OrderNumber} updated successfully");
                    }
                    scope.Complete();
                }
            });
            if (Order.OrderKey > 0)
            {
                //GetOrder(Order.OrderKey);
                event_aggregator.GetEvent<OrderUpdatedEvent>().Publish($"Order {Order.OrderNumber} updated successfully");
                //event_aggregator.GetEvent<OrderNewOrderCompleteEvent>().Publish(ViewNames.OrderHomeView);
                working_order_service.CloseOrder(Order);
                region_manager.RequestNavigate(RegionNames.ContentRegion, ViewNames.OrderHomeView);
            }
        }
    }
}
