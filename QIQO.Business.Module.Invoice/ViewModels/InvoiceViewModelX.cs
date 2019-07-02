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
using QIQO.Business.Module.Invoices.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Transactions;
using Unity.Interception.Utilities;

namespace QIQO.Business.Module.Invoices.ViewModels
{
    public class InvoiceViewModelX : ViewModelBase, IRegionMemberLifetime, IConfirmNavigationRequest //, IInteractionRequestAware
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IServiceFactory _serviceFactory;
        private readonly IProductListService _productService;
        private readonly IRegionManager _regionManager;
        private readonly IWorkingInvoiceService _workingInvoiceService;
        private readonly IReportService _reportService;
        private InvoiceWrapper _invoice;
        private Client.Entities.Account _currentAccount;

        private object _selectedInvoiceItem;
        private AddressWrapper _shipAddress;
        private AddressWrapper _billAddress;
        private ObservableCollection<Product> _productList;
        private ObservableCollection<Representative> _accountReps;
        private ObservableCollection<Representative> _salesReps;
        private ObservableCollection<FeeSchedule> _feeScheduleList;
        private ObservableCollection<AccountPerson> _accountContacts;
        private string _viewTitle = ApplicationStrings.TabTitleNewInvoice;
        private bool _gridEnabled = true;

        public InvoiceViewModelX(IEventAggregator eventAggregator, IServiceFactory serviceFactory,
            IProductListService productService, IRegionManager regionManager,
            IReportService reportService, IWorkingInvoiceService workingInvoiceService)
        {
            _eventAggregator = eventAggregator;
            _serviceFactory = serviceFactory;
            _productService = productService;
            _regionManager = regionManager;
            _reportService = reportService;
            _workingInvoiceService = workingInvoiceService;

            GetProductList();
            BindCommands();
            GetCompanyRepLists();

            IsActive = true;
            IsActiveChanged += InvoiceViewModel_IsActiveChanged;
            _eventAggregator.GetEvent<InvoiceUnloadingEvent>().Subscribe(ParentViewUnloadingEvent);
            _eventAggregator.GetEvent<InvoiceLoadedEvent>().Publish(string.Empty);
        }

        private void ParentViewUnloadingEvent(object obj)
        {
            var canClose = true;
            if (obj is NavigationContext navContext)
            {
                ConfirmNavigationRequest(navContext, result =>
                {
                    canClose = result;
                });

                if (canClose)
                {
                    OnNavigatedFrom(null);
                }
            }
        }

        private void InvoiceViewModel_IsActiveChanged(object sender, EventArgs e)
        {
            IsActive = true;
        }

        public bool KeepAlive
        {
            get
            {
                if (Invoice.IsChanged && Invoice.Account.AccountCode != null)
                {
                    return true;
                }

                return false;
            }
        }

        public void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            //********** Do some sort fo confirmation with the end user here
            if (Invoice.IsChanged && !string.IsNullOrWhiteSpace(Invoice.Account.AccountCode))
            {
                var confirm = new Confirmation();
                confirm.Title = ApplicationStrings.SaveChangesTitle;
                confirm.Content = ApplicationStrings.SaveChangesPrompt;
                SaveChangesConfirmationRequest.Raise(confirm,
                    r =>
                    {
                        if (r != null && r.Confirmed)
                        {
                            if (Invoice.IsValid)
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
            {
                continuationCallback(true);
            }
        }

        public InvoiceWrapper Invoice
        {
            get { return _invoice; }
            private set { SetProperty(ref _invoice, value); }
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

        public object SelectedInvoiceItem
        {
            get { return _selectedInvoiceItem; }
            set
            {
                SetProperty(ref _selectedInvoiceItem, value);
                InvalidateCommands();
            }
        }

        public bool GridIsEnabled
        {
            get { return _gridEnabled; }
            private set { SetProperty(ref _gridEnabled, value); }
        }

        public override string ViewTitle
        {
            get { return _viewTitle; }
            protected set { SetProperty(ref _viewTitle, value); }
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            var paramAccountCode = navigationContext.Parameters.Where(item => item.Key == "AccountCode").FirstOrDefault();
            var paramInvoiceKey = navigationContext.Parameters.Where(item => item.Key == "InvoiceKey").FirstOrDefault();
            var paramInvoiceNumber = navigationContext.Parameters.Where(item => item.Key == "InvoiceNumber").FirstOrDefault();

            if (paramAccountCode.Value != null)
            {
                Invoice.Account.AccountCode = (string)navigationContext.Parameters["AccountCode"];
                GetAccount(Invoice.Account.AccountCode);
                return;
            }

            if (paramInvoiceKey.Value != null)
            {
                GetInvoice((int)paramInvoiceKey.Value);
                return;
            }

            if (paramInvoiceNumber.Value != null)
            {
                Invoice = _workingInvoiceService.GetInvoice((string)navigationContext.Parameters["InvoiceNumber"]);
                GetAccount(Invoice.Account.AccountCode);
                _eventAggregator.GetEvent<InvoiceLoadedEvent>().Publish(Invoice.InvoiceNumber);
                return;
            }

            InitNewInvoice();
        }

        public override bool IsNavigationTarget(NavigationContext navigationContext)
        {
            var paramAccountCode = navigationContext.Parameters.Where(item => item.Key == "AccountCode").FirstOrDefault();
            var paramInvoiceNumber = navigationContext.Parameters.Where(item => item.Key == "InvoiceKey").FirstOrDefault();

            return (paramAccountCode.Value != null || paramInvoiceNumber.Value != null) ? true : false;
        }

        public int SelectedInvoiceItemIndex { get; set; }
        public DelegateCommand CancelCommand { get; set; }
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand DeleteCommand { get; set; }
        public DelegateCommand PrintCommand { get; set; }
        public DelegateCommand CloseCommand { get; set; }
        public DelegateCommand<string> GetAccountCommand { get; set; }
        public DelegateCommand<object> UpdateProdInfoCommand { get; set; }
        public DelegateCommand UpdateItemTotalCommand { get; set; }
        public DelegateCommand UpdateHeaderFromDetailCommand { get; set; }
        public DelegateCommand NewInvoiceItemCommand { get; set; }
        public DelegateCommand EditInvoiceItemCommand { get; set; }
        public DelegateCommand DeleteInvoiceItemCommand { get; set; }
        public DelegateCommand FindAccountCommand { get; set; }
        public DelegateCommand NewFromOrderCommand { get; set; }

        public InteractionRequest<ItemEditNotification> EditInvoiceItemRequest { get; set; }
        public InteractionRequest<ItemSelectionNotification> FindAccountRequest { get; set; }
        public InteractionRequest<ItemSelectionNotification> FindOrderRequest { get; set; }

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

        private void InitNewInvoice()
        {
            var new_order = new Invoice() //*** GET this initializatoin stuff into the objects themselves!! (complete)
            {
                InvoiceEntryDate = DateTime.Now,
                InvoiceStatusDate = DateTime.Now,
                OrderEntryDate = DateTime.Now,
                //DeliverByDate = DateTime.Now.AddDays(7), // think about a due date instead
                SalesRep = SalesRepList[0],
                AccountRep = AccountRepList[0]
            };

            new_order.InvoiceItems.Add(InitNewInvoiceItem(1));
            SelectedInvoiceItemIndex = 0;

            Invoice = new InvoiceWrapper(new_order);
            DefaultBillingAddress = new AddressWrapper(new Address());
            DefaultShippingAddress = new AddressWrapper(new Address());
            Invoice.PropertyChanged += Context_PropertyChanged;
            Invoice.AcceptChanges();
            _workingInvoiceService.OpenInvoice(Invoice);
            GridIsEnabled = false;
        }

        private InvoiceItem InitNewInvoiceItem(int invoiceItemSeq)
        {
            return new InvoiceItem()
            {
                SalesRep = SalesRepList[0],
                AccountRep = AccountRepList[0]
            };
        }

        private void GetInvoice(int invoiceKey)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var invoiceService = _serviceFactory.CreateClient<IInvoiceService>();
                using (invoiceService)
                {
                    Invoice = new InvoiceWrapper(invoiceService.GetInvoice(invoiceKey));
                    AccountContacts = new ObservableCollection<AccountPerson>(Invoice.Account.Model.Employees.Where(item =>
                                                                                item.CompanyRoleType == QIQOPersonType.AccountContact).ToList());
                    Invoice.PropertyChanged += Context_PropertyChanged;
                    Invoice.AcceptChanges();
                    _currentAccount = Invoice.Account.Model;
                }

                DefaultBillingAddress = Invoice.InvoiceItems[0].OrderItemBillToAddress;
                DefaultShippingAddress = Invoice.InvoiceItems[0].OrderItemShipToAddress;
                ViewTitle = Invoice.InvoiceNumber;
                GridIsEnabled = Invoice.InvoiceStatus != QIQOInvoiceStatus.Complete ? true : false;
                _eventAggregator.GetEvent<GeneralMessageEvent>().Publish($"Invoice {Invoice.InvoiceNumber} loaded successfully");
                _eventAggregator.GetEvent<InvoiceLoadedEvent>().Publish(Invoice.InvoiceNumber);
                _eventAggregator.GetEvent<NavigationEvent>().Publish(ViewNames.InvoiceHomeView);
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
            NewInvoiceItemCommand = new DelegateCommand(AddInvoiceItem, CanAddInvoiceItem);
            EditInvoiceItemCommand = new DelegateCommand(EditInvoiceItem, CanEditInvoiceItem);
            DeleteInvoiceItemCommand = new DelegateCommand(DeleteInvoiceItem, CanDeleteInvoiceItem);
            FindAccountCommand = new DelegateCommand(FindAccount);
            NewFromOrderCommand = new DelegateCommand(NewInvoiceFromOrders);

            EditInvoiceItemRequest = new InteractionRequest<ItemEditNotification>();
            FindAccountRequest = new InteractionRequest<ItemSelectionNotification>();
            FindOrderRequest = new InteractionRequest<ItemSelectionNotification>();
            SaveChangesConfirmationRequest = new InteractionRequest<IConfirmation>();
            DeleteConfirmationRequest = new InteractionRequest<IConfirmation>();
        }

        private void DoPrint()
        {
            _reportService.ExecuteReport(Reports.Invoice, $"invoice_key={Invoice.InvoiceKey.ToString()}");
        }

        private void NewInvoiceFromOrders()
        {
            var notification = new ItemSelectionNotification(ApplicationStrings.FindOrderForInvoicingPayload);
            notification.Title = ApplicationStrings.NotificationFindOrder;
            FindOrderRequest.Raise(notification,
                r =>
                {
                    if (r != null && r.Confirmed && r.SelectedItem != null)
                    {
                        var orders = r.SelectedItem as List<Order>;
                        if (orders != null)
                        {
                            GenerateInvoiceFromOrderItems(orders);
                        }
                    }
                });
        }

        private void GenerateInvoiceFromOrderItems(List<Order> orders)
        {
            // We don't have everything about the order that we need, so we have to go and get them
            var ordersToInvoice = new List<Order>(orders.Count);
            var orderService = _serviceFactory.CreateClient<IOrderService>();
            var invoiceService = _serviceFactory.CreateClient<IInvoiceService>();

            foreach (var order in orders)
            {
                ordersToInvoice.Add(orderService.GetOrder(order.OrderKey));
            }

            // Now that we have the full order(s), we can create an invoice from the data in them
            var newInvoice = new Invoice()
            {
                AccountKey = ordersToInvoice[0].AccountKey,
                OrderEntryDate = ordersToInvoice[0].OrderEntryDate,
                OrderShipDate = ordersToInvoice[0].OrderShipDate,
                AccountContactKey = ordersToInvoice[0].AccountContactKey,
                FromEntityKey = ordersToInvoice[0].OrderKey,
                InvoiceEntryDate = DateTime.Now,
                InvoiceStatus = QIQOInvoiceStatus.New,
                InvoiceStatusDate = DateTime.Now,
                AccountRepKey = ordersToInvoice[0].AccountRepKey,
                SalesRepKey = ordersToInvoice[0].SalesRepKey
                //SalesRep = orders_to_invoice[0].SalesRep
            };

            newInvoice.Account.AccountCode = ordersToInvoice[0].Account.AccountCode;
            newInvoice.Account.AccountName = ordersToInvoice[0].Account.AccountName;
            newInvoice.SalesRep.PersonFullNameFML = ordersToInvoice[0].SalesRep.PersonFullNameFML;
            newInvoice.SalesRep.EntityPersonKey = ordersToInvoice[0].SalesRep.EntityPersonKey;
            newInvoice.AccountRep.PersonFullNameFML = ordersToInvoice[0].AccountRep.PersonFullNameFML;
            newInvoice.AccountRep.EntityPersonKey = ordersToInvoice[0].AccountRep.EntityPersonKey;
            DefaultBillingAddress = new AddressWrapper(ordersToInvoice[0].Account.Addresses.Where(addr => addr.AddressType == QIQOAddressType.Billing).FirstOrDefault());
            DefaultShippingAddress = new AddressWrapper(ordersToInvoice[0].Account.Addresses.Where(addr => addr.AddressType == QIQOAddressType.Shipping).FirstOrDefault());

            foreach (var ord in ordersToInvoice)
            {
                var items_to_invoice = ord.OrderItems.Where(item => (item.OrderItemStatus != QIQOOrderItemStatus.Canceled &&
                                                        item.OrderItemStatus != QIQOOrderItemStatus.Complete)).ToList();
                foreach (var item in ord.OrderItems)
                {
                    var inv_item = invoiceService.GetInvoiceItemByOrderItemKey(item.OrderItemKey);

                    if (inv_item == null)
                    {
                        var newInvoiceItem = new InvoiceItem()
                        {
                            AccountRep = item.AccountRep,
                            SalesRep = item.SalesRep,
                            FromEntityKey = item.OrderItemKey,
                            InvoiceItemLineSum = item.OrderItemLineSum,
                            InvoiceItemProduct = item.OrderItemProduct,
                            InvoiceItemQuantity = item.OrderItemQuantity,
                            InvoiceItemSeq = item.OrderItemSeq,
                            InvoiceItemStatus = QIQOInvoiceItemStatus.New,
                            ItemPricePer = item.ItemPricePer,
                            ProductDesc = item.ProductDesc,
                            ProductKey = item.ProductKey,
                            ProductName = item.ProductName,
                            OrderItemShipDate = item.OrderItemShipDate
                        };
                        newInvoiceItem.SalesRep.EntityPersonKey = item.SalesRep.EntityPersonKey;
                        //new_invoice_item.OrderItemBillToAddress.AddressKey = item.OrderItemBillToAddress.AddressKey;
                        //new_invoice_item.OrderItemShipToAddress.AddressKey = item.OrderItemShipToAddress.AddressKey;
                        newInvoiceItem.InvoiceItemProduct.ProductKey = item.OrderItemProduct.ProductKey;
                        newInvoiceItem.InvoiceItemProduct.ProductCode = item.OrderItemProduct.ProductCode;
                        newInvoiceItem.InvoiceItemProduct.ProductDesc = item.OrderItemProduct.ProductDesc;
                        newInvoiceItem.InvoiceItemProduct.ProductName = item.OrderItemProduct.ProductName;
                        newInvoiceItem.InvoiceItemProduct.ProductType = item.OrderItemProduct.ProductType;

                        //order_item.InvoiceItemBillToAddress = DefaultBillingAddress;
                        //FillFromDefaultAddress(new_invoice_item, QIQOAddressType.Billing);
                        newInvoiceItem.OrderItemBillToAddress.AddressKey = DefaultBillingAddress.AddressKey;
                        newInvoiceItem.OrderItemBillToAddress.AddressType = QIQOAddressType.Billing;
                        newInvoiceItem.OrderItemBillToAddress.AddressLine1 = DefaultBillingAddress.AddressLine1;
                        newInvoiceItem.OrderItemBillToAddress.AddressLine2 = DefaultBillingAddress.AddressLine2;
                        newInvoiceItem.OrderItemBillToAddress.AddressCity = DefaultBillingAddress.AddressCity;
                        newInvoiceItem.OrderItemBillToAddress.AddressState = DefaultBillingAddress.AddressState;
                        newInvoiceItem.OrderItemBillToAddress.AddressPostalCode = DefaultBillingAddress.AddressPostalCode;

                        //order_item.InvoiceItemShipToAddress = DefaultShippingAddress;
                        //FillFromDefaultAddress(new_invoice_item, QIQOAddressType.Shipping);
                        newInvoiceItem.OrderItemShipToAddress.AddressKey = DefaultShippingAddress.AddressKey;
                        newInvoiceItem.OrderItemShipToAddress.AddressType = QIQOAddressType.Shipping;
                        newInvoiceItem.OrderItemShipToAddress.AddressLine1 = DefaultShippingAddress.AddressLine1;
                        newInvoiceItem.OrderItemShipToAddress.AddressLine2 = DefaultShippingAddress.AddressLine2;
                        newInvoiceItem.OrderItemShipToAddress.AddressCity = DefaultShippingAddress.AddressCity;
                        newInvoiceItem.OrderItemShipToAddress.AddressState = DefaultShippingAddress.AddressState;
                        newInvoiceItem.OrderItemShipToAddress.AddressPostalCode = DefaultShippingAddress.AddressPostalCode;

                        newInvoiceItem.AccountRep.EntityPersonKey = _accountReps[0].EntityPersonKey;
                        newInvoiceItem.SalesRep.EntityPersonKey = _salesReps[0].EntityPersonKey;
                        //new_invoice_item.InvoiceItemSeq = SelectedInvoiceItemIndex + 1;

                        newInvoice.InvoiceItems.Add(newInvoiceItem);
                    }
                }
            }

            Invoice = new InvoiceWrapper(newInvoice);
            GetAccount(newInvoice.Account.AccountCode);
            UpdateItemTotals();
            InvalidateCommands();
        }

        private bool CanDoCancel()
        {
            return true; // Invoice.IsChanged;
        }

        private void DoCancel()
        {
            _workingInvoiceService.CloseInvoice(Invoice);
            _regionManager.RequestNavigate(RegionNames.ContentRegion, ViewNames.InvoiceHomeView);
        }

        private bool CanDoDelete()
        {
            return Invoice.InvoiceKey > 0;
        }

        private void DoDelete()
        {
            var confirm = new Confirmation();
            confirm.Title = ApplicationStrings.DeleteInvoiceTitle;
            confirm.Content = $"Are you sure you want to delete order {Invoice.InvoiceNumber}?\n\nClick OK to delete. Click Cancel to return to the form.";
            DeleteConfirmationRequest.Raise(confirm,
                r =>
                {
                    if (r != null && r.Confirmed)
                    {
                        DeleteInvoice(Invoice.InvoiceNumber);
                    }
                });
        }

        private void DeleteInvoice(string invoiceNumber)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var invoiceService = _serviceFactory.CreateClient<IInvoiceService>();
                using (var scope = new TransactionScope()) // TransactionScopeAsyncFlowOption.Enabled
                {
                    using (invoiceService)
                    {
                        var ret_val = invoiceService.DeleteInvoice(Invoice.Model);
                        _eventAggregator.GetEvent<InvoiceDeletedEvent>().Publish($"Invoice {invoiceNumber} deleted successfully");
                        _regionManager.RequestNavigate(RegionNames.ContentRegion, ViewNames.InvoiceHomeView);
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
            EditInvoiceItemCommand.RaiseCanExecuteChanged();
            DeleteInvoiceItemCommand.RaiseCanExecuteChanged();
            NewInvoiceItemCommand.RaiseCanExecuteChanged();
        }

        private void FindAccount()
        {
            var notification = new ItemSelectionNotification();
            notification.Title = ApplicationStrings.NotificationFindAccount;
            FindAccountRequest.Raise(notification,
                r =>
                {
                    if (r != null && r.Confirmed && r.SelectedItem != null)
                    {
                        var found_account = r.SelectedItem as Client.Entities.Account;
                        if (found_account != null)
                        {
                            GetAccount(found_account.AccountCode);
                        }
                    }
                });
        }

        private bool CanDeleteInvoiceItem()
        {
            return (SelectedInvoiceItem != null && ((InvoiceItemWrapper)SelectedInvoiceItem).ProductKey > 0);
        }

        private void DeleteInvoiceItem()
        {
            if (SelectedInvoiceItem is InvoiceItemWrapper item_to_del)
            {
                if (item_to_del.InvoiceItemKey == 0)
                {
                    Invoice.InvoiceItems.Remove(item_to_del);
                }
                else
                {
                    item_to_del.InvoiceItemStatus = QIQOInvoiceItemStatus.Canceled;
                    item_to_del.InvoiceItemQuantity = 0;
                    item_to_del.InvoiceItemLineSum = 0M;
                    item_to_del.ItemPricePer = 0M;
                    item_to_del.ProductDesc = item_to_del.ProductDesc + " (Deleted)";
                    item_to_del.InvoiceItemCompleteDate = DateTime.Now;
                }
                UpdateItemTotals();
            }
        }

        private bool CanEditInvoiceItem()
        {
            return (SelectedInvoiceItem != null && ((InvoiceItemWrapper)SelectedInvoiceItem).ProductKey > 0);
        }

        private void EditInvoiceItem()
        {
            if (SelectedInvoiceItem is InvoiceItemWrapper item_to_edit)
            {
                ChangeInvoiceItem(item_to_edit.Model.Copy(), ApplicationStrings.NotificationEdit);
            }
        }

        private bool CanAddInvoiceItem()
        {
            return Invoice.Account.AccountKey != 0;
        }

        private void AddInvoiceItem()
        {
            var new_ord_item = new InvoiceItem()
            {
                AccountRep = Invoice.AccountRep.Model,
                OrderItemBillToAddress = DefaultBillingAddress.Model,
                //InvoiceItemSeq = Invoice.InvoiceItems.Max(item => item.InvoiceItemSeq) + 1,
                InvoiceItemQuantity = 1,
                OrderItemShipToAddress = DefaultShippingAddress.Model,
                InvoiceKey = Invoice.InvoiceKey,
                SalesRep = Invoice.SalesRep.Model
            };
            ChangeInvoiceItem(new_ord_item, ApplicationStrings.NotificationAdd);
        }

        private void ChangeInvoiceItem(InvoiceItem invoiceItem, string action)
        {
            //var item_to_edit = order_item as InvoiceItem;
            if (invoiceItem != null)
            {
                GridIsEnabled = false;
                var bill_addresses = _currentAccount.Addresses.Where(item => item.AddressType == QIQOAddressType.Billing).ToList();
                var ship_addresses = _currentAccount.Addresses.Where(item => item.AddressType == QIQOAddressType.Shipping).ToList();
                var needed_objects = new Tuple<object, object, object>(invoiceItem, bill_addresses, ship_addresses);
                var notification = new ItemEditNotification(needed_objects);
                notification.Title = action + " Invoice Item"; //+ emp_to_edit.PersonCode + " - " + emp_to_edit.PersonFullNameFML;
                EditInvoiceItemRequest.Raise(notification,
                    r =>
                    {
                        if (r != null && r.Confirmed && r.EditibleObject != null) // 
                        {
                            if (r.EditibleObject is InvoiceItem obj)
                            {
                                if (action == ApplicationStrings.NotificationEdit)
                                {
                                    var changedProduct = ProductList.Where(item => item.ProductKey == obj.ProductKey).FirstOrDefault();
                                    var itemToUpdate = SelectedInvoiceItem as InvoiceItemWrapper;
                                    itemToUpdate.ProductKey = obj.ProductKey;
                                    //item_to_update.ProductCode = changed_prod.ProductCode;
                                    itemToUpdate.ProductName = obj.ProductName;
                                    itemToUpdate.ProductDesc = obj.ProductDesc;
                                    itemToUpdate.InvoiceItemQuantity = obj.InvoiceItemQuantity;
                                    itemToUpdate.ItemPricePer = obj.ItemPricePer;
                                    itemToUpdate.InvoiceItemLineSum = invoiceItem.InvoiceItemQuantity * invoiceItem.ItemPricePer;
                                    itemToUpdate.InvoiceItemStatus = obj.InvoiceItemStatus;
                                    itemToUpdate.InvoiceItemProduct.ProductKey = obj.ProductKey;
                                    itemToUpdate.InvoiceItemProduct.ProductCode = changedProduct.ProductCode;
                                    itemToUpdate.InvoiceItemProduct.ProductDesc = obj.ProductDesc;
                                    itemToUpdate.InvoiceItemProduct.ProductName = obj.ProductName;
                                    itemToUpdate.OrderItemBillToAddress.AddressKey = obj.OrderItemBillToAddress.AddressKey;
                                    itemToUpdate.OrderItemShipToAddress.AddressKey = obj.OrderItemShipToAddress.AddressKey;
                                    itemToUpdate.SalesRep.EntityPersonKey = obj.SalesRep.EntityPersonKey;
                                    itemToUpdate.AccountRep.EntityPersonKey = obj.AccountRep.EntityPersonKey;
                                    itemToUpdate.OrderItemShipDate = obj.OrderItemShipDate;
                                    itemToUpdate.InvoiceItemCompleteDate = obj.InvoiceItemCompleteDate;
                                }
                                else
                                {
                                    Invoice.InvoiceItems.Add(new InvoiceItemWrapper(obj));
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
            var invoiceItem = Invoice.InvoiceItems[SelectedInvoiceItemIndex];
            //InvoiceItem order_item = InvoiceItems[SelectedInvoiceItemIndex];
            if (invoiceItem != null && invoiceItem.ProductKey > 0)
            {
                var sp = ProductList.Where(item => item.ProductKey == invoiceItem.ProductKey).FirstOrDefault();
                //MessageToDisplay = order_item.ProductKey.ToString() + ": " + sp[0].ProductName;

                if (invoiceItem.ProductName == "" || invoiceItem.ProductName == null || invoiceItem.ProductName != sp.ProductName)
                {
                    if (sp.ProductName != "")
                    {
                        var rp = sp.ProductAttributes.Where(item => item.AttributeType == QIQOAttributeType.Product_PRODBASE).FirstOrDefault();
                        var dq = sp.ProductAttributes.Where(item => item.AttributeType == QIQOAttributeType.Product_PRODDFQTY).FirstOrDefault();

                        //var.ProductKey = sp[0].ProductKey;
                        // order_item.ProductKey = sp.ProductKey;
                        //order_item.ProductCode = sp.ProductCode;
                        invoiceItem.ProductName = sp.ProductName;
                        invoiceItem.ProductDesc = sp.ProductDesc;
                        invoiceItem.InvoiceItemQuantity = int.Parse(dq.AttributeValue);
                        // Check for Fee Schedule here!
                        var fsp = ApplyFeeSchedule(sp.ProductKey, decimal.Parse(rp.AttributeValue));
                        invoiceItem.ItemPricePer = (fsp != 0M) ? fsp : decimal.Parse(rp.AttributeValue);
                        invoiceItem.InvoiceItemLineSum = invoiceItem.InvoiceItemQuantity * invoiceItem.ItemPricePer;

                        //order_item.InvoiceItemProduct = new ProductWrapper(sp);
                        invoiceItem.InvoiceItemProduct.ProductKey = sp.ProductKey;
                        invoiceItem.InvoiceItemProduct.ProductCode = sp.ProductCode;
                        invoiceItem.InvoiceItemProduct.ProductDesc = sp.ProductDesc;
                        invoiceItem.InvoiceItemProduct.ProductName = sp.ProductName;
                        invoiceItem.InvoiceItemProduct.ProductType = sp.ProductType;

                        //order_item.InvoiceItemBillToAddress = DefaultBillingAddress;
                        FillFromDefaultAddress(invoiceItem, QIQOAddressType.Billing);

                        //order_item.InvoiceItemShipToAddress = DefaultShippingAddress;
                        FillFromDefaultAddress(invoiceItem, QIQOAddressType.Shipping);

                        invoiceItem.AccountRep.EntityPersonKey = _accountReps[0].EntityPersonKey;
                        invoiceItem.SalesRep.EntityPersonKey = _salesReps[0].EntityPersonKey;
                        invoiceItem.InvoiceItemSeq = SelectedInvoiceItemIndex + 1;
                    }
                }
            }

            Invoice.InvoiceItemCount = Invoice.InvoiceItems.Sum(item => item.InvoiceItemQuantity);
            Invoice.InvoiceValueSum = Invoice.InvoiceItems.Sum(item => item.InvoiceItemLineSum);
            var seq = Invoice.InvoiceItems.Count;
            // Need to think about whether this is the best way to do this. What if they change an existing item?
            var new_order_line = Invoice.InvoiceItems.Where(item => item.ProductKey == 0).FirstOrDefault();
            if (new_order_line == null)
            {
                var new_item = new InvoiceItemWrapper(InitNewInvoiceItem(seq + 1));
                FillFromDefaultAddress(new_item, QIQOAddressType.Billing);
                FillFromDefaultAddress(new_item, QIQOAddressType.Shipping);
                Invoice.InvoiceItems.Add(new_item);
            }
        }

        private void FillFromDefaultAddress(InvoiceItemWrapper invoiceItem, QIQOAddressType addressType)
        {
            if (addressType == QIQOAddressType.Billing)
            {
                if (DefaultBillingAddress != null)
                {
                    invoiceItem.OrderItemBillToAddress.AddressKey = DefaultBillingAddress.AddressKey;
                    invoiceItem.OrderItemBillToAddress.AddressType = QIQOAddressType.Billing;
                    invoiceItem.OrderItemBillToAddress.AddressLine1 = DefaultBillingAddress.AddressLine1;
                    invoiceItem.OrderItemBillToAddress.AddressLine2 = DefaultBillingAddress.AddressLine2;
                    invoiceItem.OrderItemBillToAddress.AddressCity = DefaultBillingAddress.AddressCity;
                    invoiceItem.OrderItemBillToAddress.AddressState = DefaultBillingAddress.AddressState;
                    invoiceItem.OrderItemBillToAddress.AddressPostalCode = DefaultBillingAddress.AddressPostalCode;
                }
            }
            else
            {
                if (DefaultShippingAddress != null)
                {
                    invoiceItem.OrderItemShipToAddress.AddressKey = DefaultShippingAddress.AddressKey;
                    invoiceItem.OrderItemShipToAddress.AddressType = QIQOAddressType.Shipping;
                    invoiceItem.OrderItemShipToAddress.AddressLine1 = DefaultShippingAddress.AddressLine1;
                    invoiceItem.OrderItemShipToAddress.AddressLine2 = DefaultShippingAddress.AddressLine2;
                    invoiceItem.OrderItemShipToAddress.AddressCity = DefaultShippingAddress.AddressCity;
                    invoiceItem.OrderItemShipToAddress.AddressState = DefaultShippingAddress.AddressState;
                    invoiceItem.OrderItemShipToAddress.AddressPostalCode = DefaultShippingAddress.AddressPostalCode;
                }
            }
        }

        private decimal ApplyFeeSchedule(int productKey, decimal defaultPrice) // think about if this needs to be in a service
        {
            var charge = 0M; string type;

            if (FeeScheduleList != null)
            {
                var fs = FeeScheduleList.Where(item => item.ProductKey == productKey).FirstOrDefault();
                if (fs != null)
                {
                    charge = fs.FeeScheduleValue;
                    type = fs.FeeScheduleTypeCode;
                    if (type == "P")
                    {
                        charge = defaultPrice * charge;
                    }
                }
            }
            else
            {
                charge = defaultPrice;
            }

            return charge;
        }

        private void UpdateItemTotals()
        {
            if (SelectedInvoiceItemIndex != -1 && Invoice.InvoiceItems.Count > 0)
            {
                var invoiceItem = Invoice.InvoiceItems[SelectedInvoiceItemIndex];
                //InvoiceItem order_item = InvoiceItems[SelectedInvoiceItemIndex];
                if (invoiceItem != null & invoiceItem.InvoiceItemStatus != QIQOInvoiceItemStatus.Canceled)
                {
                    if (invoiceItem.ItemPricePer <= 0)
                    {
                        if (invoiceItem.InvoiceItemProduct != null)
                        {
                            var rp = invoiceItem.InvoiceItemProduct.ProductAttributes.Where(item => item.AttributeType == QIQOAttributeType.Product_PRODBASE).FirstOrDefault();
                            if (rp != null)
                            {
                                invoiceItem.ItemPricePer = ApplyFeeSchedule(invoiceItem.ProductKey, Decimal.Parse(rp.AttributeValue));
                            }
                        }
                    }
                    if (invoiceItem.InvoiceItemQuantity <= 0)
                    {
                        if (invoiceItem.InvoiceItemProduct != null)
                        {
                            var dq = invoiceItem.InvoiceItemProduct.ProductAttributes.Where(item => item.AttributeType == QIQOAttributeType.Product_PRODDFQTY).FirstOrDefault();
                            if (dq != null)
                            {
                                invoiceItem.InvoiceItemQuantity = Int32.Parse(dq.AttributeValue);
                            }
                        }
                    }

                    invoiceItem.InvoiceItemLineSum = invoiceItem.InvoiceItemQuantity * invoiceItem.ItemPricePer;
                }
            }
            Invoice.InvoiceItemCount = Invoice.InvoiceItems.Sum(item => item.InvoiceItemQuantity);
            Invoice.InvoiceValueSum = Invoice.InvoiceItems.Sum(item => item.InvoiceItemLineSum);
            Invoice.InvoiceItems.ForEach(item => item.InvoiceItemSeq = Invoice.InvoiceItems.IndexOf(item) + 1);
        }

        private void UpdateHeaderFromDetail()
        {
            if (SelectedInvoiceItemIndex != -1 && Invoice.InvoiceItems.Count > 0)
            {
                var invoiceItem = Invoice.InvoiceItems[SelectedInvoiceItemIndex];
                //InvoiceItem order_item = InvoiceItems[SelectedInvoiceItemIndex];
                if (invoiceItem != null)
                {
                    if (invoiceItem.OrderItemBillToAddress is AddressWrapper current_bill_address && current_bill_address.AddressKey != 0 && current_bill_address.AddressLine1 != null)
                    {
                        DefaultBillingAddress = current_bill_address;
                    }

                    if (invoiceItem.OrderItemShipToAddress is AddressWrapper current_ship_address && current_ship_address.AddressKey != 0 && current_ship_address.AddressLine1 != null)
                    {
                        DefaultShippingAddress = current_ship_address;
                    }

                    if (invoiceItem.SalesRep is RepresentativeWrapper current_sales_rep && current_sales_rep.EntityPersonKey != 0)
                    {
                        Invoice.SalesRep.EntityPersonKey = current_sales_rep.EntityPersonKey;
                    }

                    if (invoiceItem.AccountRep is RepresentativeWrapper current_account_rep && current_account_rep.EntityPersonKey != 0)
                    {
                        Invoice.AccountRep.EntityPersonKey = current_account_rep.EntityPersonKey;
                    }
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
                    var account = accountService.GetAccountByCode(accountCode, comp.CompanyCode);
                    if (account != null)
                    {
                        if (account.Employees != null)
                        {
                            AccountContacts = new ObservableCollection<AccountPerson>(account.Employees.Where(item => item.CompanyRoleType == QIQOPersonType.AccountContact).ToList());
                        }
                        // Get the accounts main contact key
                        var contact = account.Employees.Where(item => item.CompanyRoleType == QIQOPersonType.AccountContact).FirstOrDefault();
                        var cnt_key = contact != null ? contact.EntityPersonKey : 1;

                        Invoice.Account.AccountKey = account.AccountKey;
                        Invoice.Account.AccountName = account.AccountName;
                        Invoice.Account.AccountCode = account.AccountCode;
                        Invoice.Account.AccountDBA = account.AccountDBA;
                        Invoice.Account.AccountDesc = account.AccountDesc;
                        Invoice.AccountKey = account.AccountKey;
                        Invoice.Account.AccountType = account.AccountType;
                        Invoice.AccountContactKey = cnt_key; // account.Employees[0].EntityPersonKey;
                        Invoice.InvoiceAccountContact.PersonFullNameFML = contact != null ? contact.PersonFullNameFML : "N/A";
                        //Invoice.AccountRepKey = AccountRepList[0].EntityPersonKey;
                        //Invoice.SalesRepKey = SalesRepList[0].EntityPersonKey;
                        DefaultBillingAddress = new AddressWrapper(account.Addresses.Where(item => item.AddressType == QIQOAddressType.Billing).FirstOrDefault());
                        DefaultShippingAddress = new AddressWrapper(account.Addresses.Where(item => item.AddressType == QIQOAddressType.Shipping).FirstOrDefault());
                        FeeScheduleList = new ObservableCollection<FeeSchedule>(account.FeeSchedules);
                        _currentAccount = account;
                        RaisePropertyChanged(nameof(Invoice));
                        GridIsEnabled = true;
                    }
                    else
                    {
                        DisplayErrorMessage($"Account with code '{accountCode}' not found");
                    }
                }
            });
            _eventAggregator.GetEvent<NavigationEvent>().Publish(ViewNames.InvoiceHomeView);
        }

        private bool CanDoSave()
        {
            return Invoice.IsChanged && Invoice.IsValid;
        }

        private void DoSave()
        {
            _eventAggregator.GetEvent<GeneralMessageEvent>().Publish(ApplicationStrings.BeginningSave);
            ExecuteFaultHandledOperation(() =>
            {
                var invoiceService = _serviceFactory.CreateClient<IInvoiceService>();
                using (var scope = new TransactionScope()) //TransactionScopeAsyncFlowOption.Enabled
                {
                    using (invoiceService)
                    {
                        if (Invoice.InvoiceKey == 0)
                        {
                            var accountService = _serviceFactory.CreateClient<IAccountService>();
                            Invoice.InvoiceNumber = accountService.GetAccountNextNumber(Invoice.Account.Model, QIQOEntityNumberType.InvoiceNumber);
                            accountService.Dispose();
                        }

                        //TODO: Do something to make sure the order items are in the object properly
                        var new_order_line = Invoice.InvoiceItems.Where(item => item.ProductKey == 0).FirstOrDefault();
                        if (new_order_line != null)
                        {
                            Invoice.InvoiceItems.Remove(new_order_line);
                        }

                        // For some reason, these don't seem to get set properly when I add the account object to the Invoice object
                        Invoice.Model.InvoiceItems.ForEach(item => item.OrderItemBillToAddress = DefaultBillingAddress.Model);
                        Invoice.Model.InvoiceItems.ForEach(item => item.OrderItemShipToAddress = DefaultShippingAddress.Model);
                        var orderKey = invoiceService.CreateInvoice(Invoice.Model);
                        if (Invoice.InvoiceKey == 0)
                        {
                            Invoice.InvoiceKey = orderKey;
                        }

                        ViewTitle = Invoice.InvoiceNumber;
                        Invoice.AcceptChanges();
                    }
                    scope.Complete();
                }
            });
            if (Invoice.InvoiceKey > 0)
            {
                _eventAggregator.GetEvent<InvoiceUpdatedEvent>().Publish($"Invoice {Invoice.InvoiceNumber} updated successfully");
                _workingInvoiceService.CloseInvoice(Invoice);
                _regionManager.RequestNavigate(RegionNames.ContentRegion, ViewNames.InvoiceHomeView);
            }
        }
    }
}
