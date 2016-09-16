using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using QIQO.Business.Client.Contracts;
using QIQO.Business.Client.Core;
using QIQO.Business.Client.Core.UI;
using QIQO.Business.Client.Entities;
using QIQO.Business.Client.Wrappers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Principal;

namespace QIQO.Business.Module.Invoices.ViewModels
{
    public class InvoiceItemViewModel : ViewModelBase, IInteractionRequestAware
    {
        private readonly IEventAggregator event_aggregator;
        private readonly IServiceFactory service_factory;
        private readonly IProductListService product_service;
        private InvoiceItemWrapper _order_item;

        private ObservableCollection<Product> _productlist;
        private ObservableCollection<Representative> _accountreps;
        private ObservableCollection<Representative> _salesreps;
        private ObservableCollection<AddressWrapper> _bill_addresses = new ObservableCollection<AddressWrapper>();
        private ObservableCollection<AddressWrapper> _ship_addresses = new ObservableCollection<AddressWrapper>();
        private string _viewTitle = "Invoice Item Add/Edit";
        private ItemEditNotification notification;

        public InvoiceItemViewModel()
        {
            event_aggregator = Unity.Container.Resolve<IEventAggregator>();
            service_factory = Unity.Container.Resolve<IServiceFactory>();
            product_service = Unity.Container.Resolve<IProductListService>();

            GetProductList();
            GetCompanyRepLists();
            BindCommands();

            var ident = WindowsIdentity.GetCurrent();
            var prin = new WindowsPrincipal(ident);
            DatesEnabled = prin.IsInRole(Security.QIQOInvoiceEntryAdminRole) || prin.IsInRole(Security.QIQOCompanyAdminRole);
        }

        public DelegateCommand CancelCommand { get; set; }
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand UpdateProdInfoCommand { get; set; }

        public override string ViewTitle { get { return _viewTitle; } }
        public bool DatesEnabled { get; }

        public ObservableCollection<Product> ProductList
        {
            get { return _productlist; }
            private set { SetProperty(ref _productlist, value); }
        }

        public InvoiceItemWrapper InvoiceItem
        {
            get { return _order_item; }
            private set { SetProperty(ref _order_item, value); }
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

        public ObservableCollection<AddressWrapper> BillingAddresses
        {
            get { return _bill_addresses; }
            private set { SetProperty(ref _bill_addresses, value); }
        }

        public ObservableCollection<AddressWrapper> ShippingAddresses
        {
            get { return _ship_addresses; }
            private set { SetProperty(ref _ship_addresses, value); }
        }

        public INotification Notification
        {
            get
            {
                return notification;
            }
            set
            {
                if (value is ItemEditNotification)
                {
                    notification = value as ItemEditNotification;
                    var passed_objects = notification.EditibleObject as Tuple<object, object, object>;
                    if (passed_objects != null)
                    {
                        var bill_addresses = passed_objects.Item2 as List<Address>;
                        if (bill_addresses != null)
                            FillAddressCollection(bill_addresses, QIQOAddressType.Billing);

                        var ship_addresses = passed_objects.Item3 as List<Address>;
                        if (ship_addresses != null)
                            FillAddressCollection(ship_addresses, QIQOAddressType.Shipping);

                        var order_item = passed_objects.Item1 as InvoiceItem; //notification.EditibleObject as InvoiceItem;
                        if (order_item != null)
                        {
                            InvoiceItem = new InvoiceItemWrapper(order_item); // need to confirm this is enough to isolate the passed in object 
                            InvoiceItem.PropertyChanged += Context_PropertyChanged;
                        }
                    }
                    OnPropertyChanged(() => Notification);
                }
            }
        }

        private void FillAddressCollection(List<Address> addresses, QIQOAddressType address_type)
        {
            if (address_type == QIQOAddressType.Billing)
            {
                BillingAddresses.Clear();
                foreach (var addr in addresses) BillingAddresses.Add(new AddressWrapper(addr));
            }
            else
            {
                ShippingAddresses.Clear();
                foreach (var addr in addresses) ShippingAddresses.Add(new AddressWrapper(addr));
            }
        }

        public Action FinishInteraction { get; set; }

        private void BindCommands()
        {
            CancelCommand = new DelegateCommand(DoCancel);
            SaveCommand = new DelegateCommand(DoSave, CanDoSave);
            UpdateProdInfoCommand = new DelegateCommand(PopulateProductInfo);
        }

        private void PopulateProductInfo()
        {
            var sp = ProductList.Where(item => item.ProductKey == InvoiceItem.ProductKey).FirstOrDefault();
            if (sp != null)
            {
                if (InvoiceItem.ProductName == "" || InvoiceItem.ProductName == null || InvoiceItem.ProductName != sp.ProductName)
                {
                    if (sp.ProductName != "")
                    {
                        var rp = sp.ProductAttributes.Where(item => item.AttributeType == QIQOAttributeType.Product_PRODBASE).FirstOrDefault();
                        var dq = sp.ProductAttributes.Where(item => item.AttributeType == QIQOAttributeType.Product_PRODDFQTY).FirstOrDefault();
                        
                        InvoiceItem.ProductName = sp.ProductName;
                        InvoiceItem.ProductDesc = sp.ProductDesc;
                        InvoiceItem.InvoiceItemQuantity = int.Parse(dq.AttributeValue);

                        // Check for Fee Schedule here!
                        //decimal fsp = ApplyFeeSchedule(sp.ProductKey, decimal.Parse(rp.AttributeValue));
                        //InvoiceItem.ItemPricePer = (fsp != 0M) ? fsp : decimal.Parse(rp.AttributeValue);
                        InvoiceItem.ItemPricePer = decimal.Parse(rp.AttributeValue);
                        InvoiceItem.InvoiceItemLineSum = InvoiceItem.InvoiceItemQuantity * InvoiceItem.ItemPricePer;

                        InvoiceItem.InvoiceItemProduct.ProductKey = sp.ProductKey;
                        InvoiceItem.InvoiceItemProduct.ProductCode = sp.ProductCode;
                        InvoiceItem.InvoiceItemProduct.ProductDesc = sp.ProductDesc;
                        InvoiceItem.InvoiceItemProduct.ProductName = sp.ProductName;
                        InvoiceItem.InvoiceItemProduct.ProductType = sp.ProductType;
                    }
                }
            }
        }

        private void Context_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            InvalidateCommands();
        }

        private void InvalidateCommands()
        {
            CancelCommand.RaiseCanExecuteChanged();
            SaveCommand.RaiseCanExecuteChanged();
        }

        private void DoCancel()
        {
            InvoiceItem = null;
        }

        private bool CanDoSave()
        {
            if (InvoiceItem == null) return false;
            return InvoiceItem.IsChanged && InvoiceItem.IsValid;
        }

        private void DoSave()
        {
            notification.EditibleObject = InvoiceItem.Model;
            notification.Confirmed = true;
            FinishInteraction();
        }

        private void GetProductList() //Task<ObservableCollection<Product>>
        {
            ProductList = new ObservableCollection<Product>(product_service.ProductList);
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
    }
}
