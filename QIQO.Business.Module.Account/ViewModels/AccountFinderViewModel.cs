using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using Microsoft.Practices.Unity;
using QIQO.Business.Client.Contracts;
using QIQO.Business.Client.Core;
using QIQO.Business.Client.Core.UI;
using QIQO.Business.Client.Entities;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using QIQO.Business.Client.Core.Infrastructure;
using Prism.Regions;
using QIQO.Business.Module.Account.Views;

namespace QIQO.Business.Module.Account.ViewModels
{
    public class AccountFinderViewModel : ViewModelBase, IInteractionRequestAware
    {
        private readonly IEventAggregator event_aggregator;
        private readonly IServiceFactory service_factory;
        private readonly IRegionManager _regionManager;
        private ObservableCollection<Client.Entities.Account> _accounts;
        private string _viewTitle = "Account Find";
        private string _search_term;
        private ItemSelectionNotification notification;
        private bool _is_searching;

        public object SelectedAccount { get; set; }
        public int SelectedIndex { get; set; }

        public AccountFinderViewModel()
        {
            event_aggregator = Unity.Container.Resolve<IEventAggregator>();
            service_factory = Unity.Container.Resolve<IServiceFactory>();
            _regionManager = Unity.Container.Resolve<IRegionManager>();
            BindCommands();
        }
        public ICommand GetAccountsCommand { get; set; }
        public ICommand ChooseAccountCommand { get; set; }
        public ICommand ChooseAccountCommandX { get; set; }
        public ICommand CloseWindowCommand { get; set; }

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
                    OnPropertyChanged(() => Notification);
                }
            }
        }

        public ObservableCollection<Client.Entities.Account> Accounts
        {
            get { return _accounts; }
            private set { SetProperty(ref _accounts, value); }
        }

        public string SearchTerm
        {
            get { return _search_term; }
            set { SetProperty(ref _search_term, value); }
        }

        public bool IsLoading
        {
            get { return _is_searching; }
            private set { SetProperty(ref _is_searching, value); }
        }

        private void BindCommands()
        {
            GetAccountsCommand = new DelegateCommand(GetAccounts);
            ChooseAccountCommand = new DelegateCommand(ChooseAccount);
            ChooseAccountCommandX = new DelegateCommand(ChooseAccountX);
        }

        private void GetAccounts()
        {
            if (!string.IsNullOrWhiteSpace(SearchTerm))
            {
                IsBusy = true;
                IsLoading = true;
                IAccountService account_service = service_factory.CreateClient<IAccountService>();

                using (account_service)
                {
                    try
                    {
                        Accounts = new ObservableCollection<Client.Entities.Account>(account_service.FindAccountByCompany((Company)CurrentCompany, SearchTerm));

                        MessageToDisplay = Accounts.Count.ToString() + " account(s) found";
                    }
                    catch (Exception ex)
                    {
                        MessageToDisplay = ex.Message;
                        return;
                    }
                }
            }
            else
            {
                MessageToDisplay = "You must enter a search term in order to find an account";
            }
            IsLoading = false;
            IsBusy = false;
        }

        private void ChooseAccount()
        {
            Client.Entities.Account sel_acct = SelectedAccount as Client.Entities.Account;
            if (sel_acct != null)
            {
                if (notification != null)
                {
                    notification.SelectedItem = sel_acct;
                    notification.Confirmed = true;
                }

                FinishInteraction();
            }
        }

        private void ChooseAccountX()
        {
            Client.Entities.Account sel_acct = SelectedAccount as Client.Entities.Account;
            if (sel_acct != null)
            {
                var parameters = new NavigationParameters();
                parameters.Add("AccountKey", sel_acct.AccountKey);
                _regionManager.RequestNavigate(RegionNames.ContentRegion, typeof(AccountViewX).FullName, parameters);
            }
        }

    }
}
