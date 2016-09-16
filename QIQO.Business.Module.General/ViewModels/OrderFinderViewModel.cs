using Prism.Commands;
//using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using Microsoft.Practices.Unity;
using QIQO.Business.Client.Contracts;
using QIQO.Business.Client.Core;
using QIQO.Business.Client.Core.UI;
using QIQO.Business.Client.Entities;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using QIQO.Business.Client.Core.Infrastructure;
using System.Linq;

namespace QIQO.Business.Module.General.ViewModels
{
    public class OrderFinderViewModel : ViewModelBase, IInteractionRequestAware
    {
        //IEventAggregator event_aggregator;
        IServiceFactory service_factory;
        private ObservableCollection<Order> _orders;
        private string _viewTitle = "Order Find";
        private string _search_term;
        private ItemSelectionNotification notification;
        private List<Order> _selected_orders = new List<Order>();
        private bool _filter_to_account = false;
        private ObservableCollection<Account> _accounts;
        private object _selected_account;
        private int _selected_index;

        public OrderFinderViewModel()
        {
            //event_aggregator = Unity.Container.Resolve<IEventAggregator>();
            service_factory = Unity.Container.Resolve<IServiceFactory>();
            BindCommands();
        }
        public DelegateCommand GetOrdersCommand { get; set; }
        public DelegateCommand ChooseOrdersCommand { get; set; }
        public DelegateCommand<object> SelectedItemsCommand { get; set; }

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
                    var payload = notification.Payload;
                    if (payload != null)
                    {
                        if ((string)payload == ApplicationStrings.FindOrderForInvoicingPayload)
                            _filter_to_account = true;
                    }
                    OnPropertyChanged(() => Notification);
                    GetAccountList();
                }
            }
        }

        public ObservableCollection<Order> Orders
        {
            get { return _orders; }
            private set { SetProperty(ref _orders, value); }
        }

        public ObservableCollection<Account> AccountList
        {
            get { return _accounts; }
            private set { SetProperty(ref _accounts, value); }
        }

        public object SelectedAccount
        {
            get { return _selected_account; }
            set { SetProperty(ref _selected_account, value); }
        }

        public int SelectedIndex
        {
            get { return _selected_index; }
            set { SetProperty(ref _selected_index, value); }
        }

        public string SearchTerm
        {
            get { return _search_term; }
            set { SetProperty(ref _search_term, value); }
        }
        public bool FilterByAccount => _filter_to_account;

        private void BindCommands()
        {
            GetOrdersCommand = new DelegateCommand(GetOrders);
            ChooseOrdersCommand = new DelegateCommand(ChooseOrder);
            SelectedItemsCommand = new DelegateCommand<object>(SelectOrders);
        }

        private void SelectOrders(object orders)
        {
            System.Collections.IList selected_orders = (System.Collections.IList)orders;
            //var collection = selected_orders
            if (selected_orders != null)
            {
                _selected_orders.Clear();
                foreach (var order in selected_orders)
                {
                    _selected_orders.Add((Order)order);
                }
            }
        }

        private void GetOrders()
        {
            //if (!string.IsNullOrWhiteSpace(SearchTerm))
            //{
            IsBusy = true;
            IOrderService order_service = service_factory.CreateClient<IOrderService>();

            using (order_service)
            {
                try
                {
                    //Orders = new ObservableCollection<Order>(order_service.FindOrdersByCompany((Company)CurrentCompany, SearchTerm));
                    var account = SelectedAccount as Account;
                    if (account != null)
                    {
                        Orders = new ObservableCollection<Order>(order_service.GetInvoicableOrdersByAccount(CurrentCompanyKey, account.AccountKey));

                        MessageToDisplay = Orders.Count.ToString() + " order(s) found";
                    }
                }
                catch (Exception ex)
                {
                    MessageToDisplay = ex.Message;
                    IsBusy = false;
                    return;
                }
            }
            //}
            //else
            //{
            //    MessageToDisplay = "You must enter a search term in order to find an... order?";
            //}
            IsBusy = false;
        }

        private void ChooseOrder()
        {
            if (_selected_orders != null)
            {
                if (notification != null)
                {
                    notification.SelectedItem = _selected_orders;
                    notification.Confirmed = true;
                }

                FinishInteraction();
            }
        }

        private void GetAccountList()
        {
            IAccountService account_service = service_factory.CreateClient<IAccountService>();
            ICurrentCompanyService company_service = Unity.Container.Resolve<ICurrentCompanyService>();

            using (account_service)
            {
                var accounts = account_service.GetAccountsByCompany(company_service.CurrentCompany);
                AccountList = new ObservableCollection<Account>(accounts);
            }            
        }
    }
}
