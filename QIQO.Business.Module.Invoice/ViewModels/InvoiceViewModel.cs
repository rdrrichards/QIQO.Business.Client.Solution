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
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Transactions;

namespace QIQO.Business.Module.Invoices.ViewModels
{
    public class InvoiceViewModel : ViewModelBase, IRegionMemberLifetime, IConfirmNavigationRequest, IInteractionRequestAware
    {
        private readonly IEventAggregator event_aggregator;
        private readonly IServiceFactory service_factory;
        private readonly IProductListService _product_service;
        private readonly IRegionManager region_manager;
        private IReportService report_service;
        private InvoiceWrapper _invoice;
        private Client.Entities.Account _currentAccount;

        private object _selected_invoice_item;
        private AddressWrapper _ship_address;
        private AddressWrapper _bill_address;
        private ObservableCollection<Product> _productlist;
        private ObservableCollection<Representative> _accountreps;
        private ObservableCollection<Representative> _salesreps;
        private ObservableCollection<FeeSchedule> _feeschedulelist;
        private ObservableCollection<AccountPerson> _account_contacts;
        private string _viewTitle = ApplicationStrings.TabTitleNewInvoice;
        private bool _grid_enabled = true;
        private ItemEditNotification notification;

        public InvoiceViewModel(IEventAggregator event_aggtr, IServiceFactory service_fctry, 
            IProductListService product_service, IRegionManager region_mgr,
            IReportService reportService) //
        {
            event_aggregator = event_aggtr;
            service_factory = service_fctry;
            _product_service = product_service;
            region_manager = region_mgr;
            report_service = reportService;

            GetProductList();
            BindCommands();
            GetCompanyRepLists();
            InitNewInvoice();

            RegisterApplicationCommands();
            IsActive = true;
            IsActiveChanged += InvoiceViewModel_IsActiveChanged;
            event_aggregator.GetEvent<InvoiceUnloadingEvent>().Subscribe(ParentViewUnloadingEvent);
            event_aggregator.GetEvent<InvoiceLoadedEvent>().Publish(string.Empty);
        }

        private void ParentViewUnloadingEvent(object obj)
        {
            bool canClose = true;
            var navContext = obj as NavigationContext;
            if (navContext != null)
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
                if (Invoice.IsChanged && Invoice.Account.AccountCode != null) return true;
                return false;
            }
        }

        public void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            //********** Do some sort fo confirmation with the end user here
            if (Invoice.IsChanged && !string.IsNullOrWhiteSpace(Invoice.Account.AccountCode))
            {
                Confirmation confirm = new Confirmation();
                confirm.Title = ApplicationStrings.SaveChangesTitle;
                confirm.Content = ApplicationStrings.SaveChangesPrompt;
                SaveChangesConfirmationRequest.Raise(confirm,
                    r => {
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
                continuationCallback(true);
        }

        //public bool KeepAlive => Invoice.IsChanged;

        public InvoiceWrapper Invoice
        {
            get { return _invoice; }
            private set { SetProperty(ref _invoice, value); }
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

        public object SelectedInvoiceItem
        {
            get { return _selected_invoice_item; }
            set
            {
                SetProperty(ref _selected_invoice_item, value);
                InvalidateCommands();
            }
        }

        public bool GridIsEnabled
        {
            get { return _grid_enabled; }
            private set { SetProperty(ref _grid_enabled, value); }
        }

        public override string ViewTitle
        {
            get { return _viewTitle; }
            protected set { SetProperty(ref _viewTitle, value); }
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            var paramAccountCode = navigationContext.Parameters.Where(item => item.Key == "AccountCode").FirstOrDefault();
            var paramInvoiceNumber = navigationContext.Parameters.Where(item => item.Key == "InvoiceKey").FirstOrDefault();

            if (paramAccountCode.Value != null)
            {
                Invoice.Account.AccountCode = (string)navigationContext.Parameters["AccountCode"];
                GetAccount(Invoice.Account.AccountCode);
                return;
            }

            if (paramInvoiceNumber.Value != null)
            {
                GetInvoice((int)paramInvoiceNumber.Value);
                return;
            }
        }

        public override bool IsNavigationTarget(NavigationContext navigationContext)
        {
            //return true;
            var paramAccountCode = navigationContext.Parameters.Where(item => item.Key == "AccountCode").FirstOrDefault();
            var paramInvoiceNumber = navigationContext.Parameters.Where(item => item.Key == "InvoiceKey").FirstOrDefault();

            return (paramAccountCode.Value != null || paramInvoiceNumber.Value != null) ? true : false;
            //return navigationContext.Parameters.Conta ? true : false;
        }

        private void RegisterApplicationCommands()
        {
            ApplicationCommands.SaveInvoiceCommand.RegisterCommand(SaveCommand);
            ApplicationCommands.DeleteInvoiceCommand.RegisterCommand(DeleteCommand);
            ApplicationCommands.CancelInvoiceCommand.RegisterCommand(CancelCommand);
            ApplicationCommands.PrintInvoiceCommand.RegisterCommand(PrintCommand);
        }

        public override void OnNavigatedFrom(NavigationContext navigationContext)
        {
            ApplicationCommands.SaveInvoiceCommand.UnregisterCommand(SaveCommand);
            ApplicationCommands.DeleteInvoiceCommand.UnregisterCommand(DeleteCommand);
            ApplicationCommands.CancelInvoiceCommand.UnregisterCommand(CancelCommand);
            ApplicationCommands.PrintInvoiceCommand.UnregisterCommand(PrintCommand);
            event_aggregator.GetEvent<InvoiceUnloadingEvent>().Unsubscribe(ParentViewUnloadingEvent);
        }

        public int SelectedInvoiceItemIndex { get; set; }
        public DelegateCommand CancelCommand { get; set; }
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand DeleteCommand { get; set; }
        public DelegateCommand PrintCommand { get; set; }
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

        public INotification Notification
        {
            get { return notification; }

            set
            {
                if (value is ItemEditNotification)
                {
                    notification = value as ItemEditNotification;
                    var passed_object = notification.EditibleObject as InvoiceWrapper;
                    if (passed_object != null)
                    {
                        GetInvoice(passed_object.InvoiceKey);
                    }
                    OnPropertyChanged(() => Notification);
                }
            }
        }

        public Action FinishInteraction { get; set; }

        //public bool KeepAlive => false;

        protected override void DisplayErrorMessage(Exception ex, [CallerMemberName] string methodName = "")
        {
            event_aggregator.GetEvent<GeneralErrorEvent>().Publish(methodName + " - " + ex.Message);
        }
        protected void DisplayErrorMessage(string msg)
        {
            event_aggregator.GetEvent<GeneralErrorEvent>().Publish(msg);
        }

        private void InitNewInvoice()
        {
            Invoice new_order = new Invoice() //*** GET this initializatoin stuff into the objects themselves!! (complete)
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
            GridIsEnabled = false;
        }

        private InvoiceItem InitNewInvoiceItem(int order_item_seq)
        {
            return new InvoiceItem()
            {
                //InvoiceItemSeq = order_item_seq,
                SalesRep = SalesRepList[0],
                AccountRep = AccountRepList[0]
            };
        }

        private void GetInvoice(int invoice_key)
        {
            ExecuteFaultHandledOperation(() =>
            {
                IInvoiceService order_service = service_factory.CreateClient<IInvoiceService>();
                using (order_service)
                {
                    Invoice = new InvoiceWrapper(order_service.GetInvoice(invoice_key));
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
                event_aggregator.GetEvent<GeneralMessageEvent>().Publish($"Invoice {Invoice.InvoiceNumber} loaded successfully");
                event_aggregator.GetEvent<InvoiceLoadedEvent>().Publish(Invoice.InvoiceNumber);
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
            report_service.ExecuteReport(Reports.Invoice, $"invoice_key={Invoice.InvoiceKey.ToString()}");
        }

        private void NewInvoiceFromOrders()
        {
            ItemSelectionNotification notification = new ItemSelectionNotification(ApplicationStrings.FindOrderForInvoicingPayload);
            notification.Title = ApplicationStrings.NotificationFindOrder;
            FindOrderRequest.Raise(notification,
                r => {
                    if (r != null && r.Confirmed && r.SelectedItem != null)
                    {
                        List<Order> orders = r.SelectedItem as List<Order>;
                        if (orders != null)
                            GenerateInvoiceFromOrderItems(orders);
                    }
                });
        }

        private void GenerateInvoiceFromOrderItems(List<Order> orders)
        {
            // We don't have everything about the order that we need, so we have to go and get them
            List<Order> orders_to_invoice = new List<Order>(orders.Count);
            IOrderService order_service = service_factory.CreateClient<IOrderService>();
            IInvoiceService invoice_service = service_factory.CreateClient<IInvoiceService>();

            foreach (var order in orders)
                orders_to_invoice.Add(order_service.GetOrder(order.OrderKey));

            // Now that we have the full order(s), we can create an invoice from the data in them
            Invoice new_ivoice = new Invoice() {
                AccountKey = orders_to_invoice[0].AccountKey,
                OrderEntryDate = orders_to_invoice[0].OrderEntryDate,
                OrderShipDate = orders_to_invoice[0].OrderShipDate,
                AccountContactKey = orders_to_invoice[0].AccountContactKey,
                FromEntityKey = orders_to_invoice[0].OrderKey,
                InvoiceEntryDate = DateTime.Now,
                InvoiceStatus = QIQOInvoiceStatus.New,
                InvoiceStatusDate = DateTime.Now,
                AccountRepKey = orders_to_invoice[0].AccountRepKey,
                SalesRepKey = orders_to_invoice[0].SalesRepKey
                //SalesRep = orders_to_invoice[0].SalesRep
            };

            new_ivoice.Account.AccountCode = orders_to_invoice[0].Account.AccountCode;
            new_ivoice.Account.AccountName = orders_to_invoice[0].Account.AccountName;
            new_ivoice.SalesRep.PersonFullNameFML = orders_to_invoice[0].SalesRep.PersonFullNameFML;
            new_ivoice.SalesRep.EntityPersonKey = orders_to_invoice[0].SalesRep.EntityPersonKey;
            new_ivoice.AccountRep.PersonFullNameFML = orders_to_invoice[0].AccountRep.PersonFullNameFML;
            new_ivoice.AccountRep.EntityPersonKey = orders_to_invoice[0].AccountRep.EntityPersonKey;
            DefaultBillingAddress = new AddressWrapper(orders_to_invoice[0].Account.Addresses.Where(addr => addr.AddressType == QIQOAddressType.Billing).FirstOrDefault());
            DefaultShippingAddress = new AddressWrapper(orders_to_invoice[0].Account.Addresses.Where(addr => addr.AddressType == QIQOAddressType.Shipping).FirstOrDefault());

            foreach (var ord in orders_to_invoice)
            {
                var items_to_invoice = ord.OrderItems.Where(item => (item.OrderItemStatus != QIQOOrderItemStatus.Canceled && 
                                                        item.OrderItemStatus != QIQOOrderItemStatus.Complete)).ToList();
                foreach (var item in ord.OrderItems)
                {
                    var inv_item = invoice_service.GetInvoiceItemByOrderItemKey(item.OrderItemKey);

                    if (inv_item == null)
                    {
                        InvoiceItem new_invoice_item = new InvoiceItem()
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
                        new_invoice_item.SalesRep.EntityPersonKey = item.SalesRep.EntityPersonKey;
                        //new_invoice_item.OrderItemBillToAddress.AddressKey = item.OrderItemBillToAddress.AddressKey;
                        //new_invoice_item.OrderItemShipToAddress.AddressKey = item.OrderItemShipToAddress.AddressKey;
                        new_invoice_item.InvoiceItemProduct.ProductKey = item.OrderItemProduct.ProductKey;
                        new_invoice_item.InvoiceItemProduct.ProductCode = item.OrderItemProduct.ProductCode;
                        new_invoice_item.InvoiceItemProduct.ProductDesc = item.OrderItemProduct.ProductDesc;
                        new_invoice_item.InvoiceItemProduct.ProductName = item.OrderItemProduct.ProductName;
                        new_invoice_item.InvoiceItemProduct.ProductType = item.OrderItemProduct.ProductType;

                        //order_item.InvoiceItemBillToAddress = DefaultBillingAddress;
                        //FillFromDefaultAddress(new_invoice_item, QIQOAddressType.Billing);
                        new_invoice_item.OrderItemBillToAddress.AddressKey = DefaultBillingAddress.AddressKey;
                        new_invoice_item.OrderItemBillToAddress.AddressType = QIQOAddressType.Billing;
                        new_invoice_item.OrderItemBillToAddress.AddressLine1 = DefaultBillingAddress.AddressLine1;
                        new_invoice_item.OrderItemBillToAddress.AddressLine2 = DefaultBillingAddress.AddressLine2;
                        new_invoice_item.OrderItemBillToAddress.AddressCity = DefaultBillingAddress.AddressCity;
                        new_invoice_item.OrderItemBillToAddress.AddressState = DefaultBillingAddress.AddressState;
                        new_invoice_item.OrderItemBillToAddress.AddressPostalCode = DefaultBillingAddress.AddressPostalCode;

                        //order_item.InvoiceItemShipToAddress = DefaultShippingAddress;
                        //FillFromDefaultAddress(new_invoice_item, QIQOAddressType.Shipping);
                        new_invoice_item.OrderItemShipToAddress.AddressKey = DefaultShippingAddress.AddressKey;
                        new_invoice_item.OrderItemShipToAddress.AddressType = QIQOAddressType.Shipping;
                        new_invoice_item.OrderItemShipToAddress.AddressLine1 = DefaultShippingAddress.AddressLine1;
                        new_invoice_item.OrderItemShipToAddress.AddressLine2 = DefaultShippingAddress.AddressLine2;
                        new_invoice_item.OrderItemShipToAddress.AddressCity = DefaultShippingAddress.AddressCity;
                        new_invoice_item.OrderItemShipToAddress.AddressState = DefaultShippingAddress.AddressState;
                        new_invoice_item.OrderItemShipToAddress.AddressPostalCode = DefaultShippingAddress.AddressPostalCode;

                        new_invoice_item.AccountRep.EntityPersonKey = _accountreps[0].EntityPersonKey;
                        new_invoice_item.SalesRep.EntityPersonKey = _salesreps[0].EntityPersonKey;
                        //new_invoice_item.InvoiceItemSeq = SelectedInvoiceItemIndex + 1;

                        new_ivoice.InvoiceItems.Add(new_invoice_item);
                    }
                }
            }

            Invoice = new InvoiceWrapper(new_ivoice);
            GetAccount(new_ivoice.Account.AccountCode);
            UpdateItemTotals();
            InvalidateCommands();
        }

        private bool CanDoCancel()
        {
            return Invoice.IsChanged;
        }

        private void DoCancel()
        {
            Confirmation confirm = new Confirmation();
            confirm.Title = ApplicationStrings.ConfirmCancelTitle;
            confirm.Content = ApplicationStrings.ConfirmCancelPrompt;
            SaveChangesConfirmationRequest.Raise(confirm,
                r => {
                    if (r != null && r.Confirmed)
                    {
                        InitNewInvoice();
                    }
                });
        }

        private bool CanDoDelete()
        {
            return Invoice.InvoiceKey > 0;
        }

        private void DoDelete()
        {
            Confirmation confirm = new Confirmation();
            confirm.Title = ApplicationStrings.DeleteInvoiceTitle;
            confirm.Content = $"Are you sure you want to delete order {Invoice.InvoiceNumber}?\n\nClick OK to delete. Click Cancel to return to the form.";
            DeleteConfirmationRequest.Raise(confirm,
                r => {
                    if (r != null && r.Confirmed)
                    {
                        DeleteInvoice(Invoice.InvoiceNumber);
                    }
                });
        }

        private void DeleteInvoice(string order_number)
        {
            ExecuteFaultHandledOperation(() =>
            {
                IInvoiceService order_service = service_factory.CreateClient<IInvoiceService>();
                using (TransactionScope scope = new TransactionScope()) // TransactionScopeAsyncFlowOption.Enabled
                {
                    using (order_service)
                    {
                        bool ret_val = order_service.DeleteInvoice(Invoice.Model);
                        InitNewInvoice();
                        ViewTitle = ApplicationStrings.TabTitleNewInvoice;
                        event_aggregator.GetEvent<InvoiceDeletedEvent>().Publish($"Invoice {order_number} deleted successfully");
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
            GetAccountCommand.RaiseCanExecuteChanged();
            EditInvoiceItemCommand.RaiseCanExecuteChanged();
            DeleteInvoiceItemCommand.RaiseCanExecuteChanged();
            NewInvoiceItemCommand.RaiseCanExecuteChanged();
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

        private bool CanDeleteInvoiceItem()
        {
            return (SelectedInvoiceItem != null && ((InvoiceItemWrapper)SelectedInvoiceItem).ProductKey > 0);
        }

        private void DeleteInvoiceItem()
        {
            var item_to_del = SelectedInvoiceItem as InvoiceItemWrapper;
            if (item_to_del != null)
            {
                if (item_to_del.InvoiceItemKey == 0)
                    Invoice.InvoiceItems.Remove(item_to_del);
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
            var item_to_edit = SelectedInvoiceItem as InvoiceItemWrapper;
            if (item_to_edit != null)
                ChangeInvoiceItem(item_to_edit.Model.Copy(), ApplicationStrings.NotificationEdit);
        }

        private bool CanAddInvoiceItem()
        {
            return Invoice.Account.AccountKey != 0;
        }

        private void AddInvoiceItem()
        {
            InvoiceItem new_ord_item = new InvoiceItem()
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

        private void ChangeInvoiceItem(InvoiceItem order_item, string action)
        {
            //var item_to_edit = order_item as InvoiceItem;
            if (order_item != null)
            {
                GridIsEnabled = false;
                var bill_addresses = _currentAccount.Addresses.Where(item => item.AddressType == QIQOAddressType.Billing).ToList();
                var ship_addresses = _currentAccount.Addresses.Where(item => item.AddressType == QIQOAddressType.Shipping).ToList();
                Tuple<object, object, object> needed_objects = new Tuple<object, object, object>(order_item, bill_addresses, ship_addresses);
                ItemEditNotification notification = new ItemEditNotification(needed_objects);
                notification.Title = action + " Invoice Item"; //+ emp_to_edit.PersonCode + " - " + emp_to_edit.PersonFullNameFML;
                EditInvoiceItemRequest.Raise(notification,
                    r =>
                    {
                        if (r != null && r.Confirmed && r.EditibleObject != null) // 
                        {
                            InvoiceItem obj = r.EditibleObject as InvoiceItem;
                            if (obj != null)
                            {
                                if (action == ApplicationStrings.NotificationEdit)
                                {
                                    var changed_prod = ProductList.Where(item => item.ProductKey == obj.ProductKey).FirstOrDefault();
                                    var item_to_update = SelectedInvoiceItem as InvoiceItemWrapper;
                                    item_to_update.ProductKey = obj.ProductKey;
                                    //item_to_update.ProductCode = changed_prod.ProductCode;
                                    item_to_update.ProductName = obj.ProductName;
                                    item_to_update.ProductDesc = obj.ProductDesc;
                                    item_to_update.InvoiceItemQuantity = obj.InvoiceItemQuantity;
                                    item_to_update.ItemPricePer = obj.ItemPricePer;
                                    item_to_update.InvoiceItemLineSum = order_item.InvoiceItemQuantity * order_item.ItemPricePer;
                                    item_to_update.InvoiceItemStatus = obj.InvoiceItemStatus;
                                    item_to_update.InvoiceItemProduct.ProductKey = obj.ProductKey;
                                    item_to_update.InvoiceItemProduct.ProductCode = changed_prod.ProductCode;
                                    item_to_update.InvoiceItemProduct.ProductDesc = obj.ProductDesc;
                                    item_to_update.InvoiceItemProduct.ProductName = obj.ProductName;
                                    item_to_update.OrderItemBillToAddress.AddressKey = obj.OrderItemBillToAddress.AddressKey;
                                    item_to_update.OrderItemShipToAddress.AddressKey = obj.OrderItemShipToAddress.AddressKey;
                                    item_to_update.SalesRep.EntityPersonKey = obj.SalesRep.EntityPersonKey;
                                    item_to_update.AccountRep.EntityPersonKey = obj.AccountRep.EntityPersonKey;
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
            InvoiceItemWrapper invoice_item = Invoice.InvoiceItems[SelectedInvoiceItemIndex];
            //InvoiceItem order_item = InvoiceItems[SelectedInvoiceItemIndex];
            if (invoice_item != null && invoice_item.ProductKey > 0)
            {
                var sp = ProductList.Where(item => item.ProductKey == invoice_item.ProductKey).FirstOrDefault();
                //MessageToDisplay = order_item.ProductKey.ToString() + ": " + sp[0].ProductName;

                if (invoice_item.ProductName == "" || invoice_item.ProductName == null || invoice_item.ProductName != sp.ProductName)
                {
                    if (sp.ProductName != "")
                    {
                        var rp = sp.ProductAttributes.Where(item => item.AttributeType == QIQOAttributeType.Product_PRODBASE).FirstOrDefault();
                        var dq = sp.ProductAttributes.Where(item => item.AttributeType == QIQOAttributeType.Product_PRODDFQTY).FirstOrDefault();

                        //var.ProductKey = sp[0].ProductKey;
                        // order_item.ProductKey = sp.ProductKey;
                        //order_item.ProductCode = sp.ProductCode;
                        invoice_item.ProductName = sp.ProductName;
                        invoice_item.ProductDesc = sp.ProductDesc;
                        invoice_item.InvoiceItemQuantity = int.Parse(dq.AttributeValue);
                        // Check for Fee Schedule here!
                        decimal fsp = ApplyFeeSchedule(sp.ProductKey, decimal.Parse(rp.AttributeValue));
                        invoice_item.ItemPricePer = (fsp != 0M) ? fsp : decimal.Parse(rp.AttributeValue);
                        invoice_item.InvoiceItemLineSum = invoice_item.InvoiceItemQuantity * invoice_item.ItemPricePer;

                        //order_item.InvoiceItemProduct = new ProductWrapper(sp);
                        invoice_item.InvoiceItemProduct.ProductKey = sp.ProductKey;
                        invoice_item.InvoiceItemProduct.ProductCode = sp.ProductCode;
                        invoice_item.InvoiceItemProduct.ProductDesc = sp.ProductDesc;
                        invoice_item.InvoiceItemProduct.ProductName = sp.ProductName;
                        invoice_item.InvoiceItemProduct.ProductType = sp.ProductType;

                        //order_item.InvoiceItemBillToAddress = DefaultBillingAddress;
                        FillFromDefaultAddress(invoice_item, QIQOAddressType.Billing);

                        //order_item.InvoiceItemShipToAddress = DefaultShippingAddress;
                        FillFromDefaultAddress(invoice_item, QIQOAddressType.Shipping);

                        invoice_item.AccountRep.EntityPersonKey = _accountreps[0].EntityPersonKey;
                        invoice_item.SalesRep.EntityPersonKey = _salesreps[0].EntityPersonKey;
                        invoice_item.InvoiceItemSeq = SelectedInvoiceItemIndex + 1;
                    }
                }
            }

            Invoice.InvoiceItemCount = Invoice.InvoiceItems.Sum(item => item.InvoiceItemQuantity);
            Invoice.InvoiceValueSum = Invoice.InvoiceItems.Sum(item => item.InvoiceItemLineSum);
            int seq = Invoice.InvoiceItems.Count;
            // Need to think about whether this is the best way to do this. What if they change an existing item?
            var new_order_line = Invoice.InvoiceItems.Where(item => item.ProductKey == 0).FirstOrDefault();
            if (new_order_line == null)
            {
                InvoiceItemWrapper new_item = new InvoiceItemWrapper(InitNewInvoiceItem(seq + 1));
                FillFromDefaultAddress(new_item, QIQOAddressType.Billing);
                FillFromDefaultAddress(new_item, QIQOAddressType.Shipping);
                Invoice.InvoiceItems.Add(new_item);
            }
        }

        private void FillFromDefaultAddress(InvoiceItemWrapper order_item, QIQOAddressType addr_type)
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
            if (SelectedInvoiceItemIndex != -1 && Invoice.InvoiceItems.Count > 0)
            {
                InvoiceItemWrapper invoice_item = Invoice.InvoiceItems[SelectedInvoiceItemIndex];
                //InvoiceItem order_item = InvoiceItems[SelectedInvoiceItemIndex];
                if (invoice_item != null & invoice_item.InvoiceItemStatus != QIQOInvoiceItemStatus.Canceled)
                {
                    if (invoice_item.ItemPricePer <= 0)
                    {
                        if (invoice_item.InvoiceItemProduct != null)
                        {
                            var rp = invoice_item.InvoiceItemProduct.ProductAttributes.Where(item => item.AttributeType == QIQOAttributeType.Product_PRODBASE).FirstOrDefault();
                            if (rp != null)
                                invoice_item.ItemPricePer = ApplyFeeSchedule(invoice_item.ProductKey, Decimal.Parse(rp.AttributeValue));
                        }
                    }
                    if (invoice_item.InvoiceItemQuantity <= 0)
                    {
                        if (invoice_item.InvoiceItemProduct != null)
                        {
                            var dq = invoice_item.InvoiceItemProduct.ProductAttributes.Where(item => item.AttributeType == QIQOAttributeType.Product_PRODDFQTY).FirstOrDefault();
                            if (dq != null)
                                invoice_item.InvoiceItemQuantity = Int32.Parse(dq.AttributeValue);
                        }
                    }

                    invoice_item.InvoiceItemLineSum = invoice_item.InvoiceItemQuantity * invoice_item.ItemPricePer;
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
                InvoiceItemWrapper order_item = Invoice.InvoiceItems[SelectedInvoiceItemIndex];
                //InvoiceItem order_item = InvoiceItems[SelectedInvoiceItemIndex];
                if (order_item != null)
                {
                    AddressWrapper current_bill_address = order_item.OrderItemBillToAddress as AddressWrapper;
                    if (current_bill_address != null && current_bill_address.AddressKey != 0 && current_bill_address.AddressLine1 != null)
                        DefaultBillingAddress = current_bill_address;

                    AddressWrapper current_ship_address = order_item.OrderItemShipToAddress as AddressWrapper;
                    if (current_ship_address != null && current_ship_address.AddressKey != 0 && current_ship_address.AddressLine1 != null)
                        DefaultShippingAddress = current_ship_address;

                    RepresentativeWrapper current_sales_rep = order_item.SalesRep as RepresentativeWrapper;
                    if (current_sales_rep != null && current_sales_rep.EntityPersonKey != 0) Invoice.SalesRep.EntityPersonKey = current_sales_rep.EntityPersonKey;

                    RepresentativeWrapper current_account_rep = order_item.AccountRep as RepresentativeWrapper;
                    if (current_account_rep != null && current_account_rep.EntityPersonKey != 0) Invoice.AccountRep.EntityPersonKey = current_account_rep.EntityPersonKey;
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
                        OnPropertyChanged(nameof(Invoice));
                        GridIsEnabled = true;
                    }
                    else
                    {
                        DisplayErrorMessage($"Account with code '{account_code}' not found");
                    }
                }
            });
        }

        private bool CanDoSave()
        {
            return Invoice.IsChanged && Invoice.IsValid;
        }

        private void DoSave()
        {
            event_aggregator.GetEvent<GeneralMessageEvent>().Publish(ApplicationStrings.BeginningSave);
            ExecuteFaultHandledOperation(() =>
            {
                IInvoiceService order_service = service_factory.CreateClient<IInvoiceService>();
                using (TransactionScope scope = new TransactionScope()) //TransactionScopeAsyncFlowOption.Enabled
                {
                    using (order_service)
                    {
                        if (Invoice.InvoiceKey == 0)
                        {
                            IAccountService account_service = service_factory.CreateClient<IAccountService>();
                            Invoice.InvoiceNumber = account_service.GetAccountNextNumber(Invoice.Account.Model, QIQOEntityNumberType.InvoiceNumber);
                            account_service.Dispose();
                        }

                        //TODO: Do something to make sure the order items are in the object properly
                        var new_order_line = Invoice.InvoiceItems.Where(item => item.ProductKey == 0).FirstOrDefault();
                        if (new_order_line != null)
                            Invoice.InvoiceItems.Remove(new_order_line);

                        // For some reason, these don't seem to get set properly when I add the account object to the Invoice object
                        Invoice.Model.InvoiceItems.ForEach(item => item.OrderItemBillToAddress = DefaultBillingAddress.Model);
                        Invoice.Model.InvoiceItems.ForEach(item => item.OrderItemShipToAddress = DefaultShippingAddress.Model);
                        int order_key = order_service.CreateInvoice(Invoice.Model);
                        if (Invoice.InvoiceKey == 0) Invoice.InvoiceKey = order_key;
                        ViewTitle = Invoice.InvoiceNumber;
                        Invoice.AcceptChanges();
                        //event_aggregator.GetEvent<InvoiceUpdatedEvent>().Publish($"Invoice {Invoice.InvoiceNumber} updated successfully");
                    }
                    scope.Complete();
                }
            });
            if (Invoice.InvoiceKey > 0)
            {
                GetInvoice(Invoice.InvoiceKey);
                event_aggregator.GetEvent<InvoiceUpdatedEvent>().Publish($"Invoice {Invoice.InvoiceNumber} updated successfully");
            }
            InvalidateCommands();
        }
    }
}
