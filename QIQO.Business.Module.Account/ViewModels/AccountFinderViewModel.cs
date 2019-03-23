using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using CommonServiceLocator;
using QIQO.Business.Client.Contracts;
using QIQO.Business.Client.Core;
using QIQO.Business.Client.Core.UI;
using QIQO.Business.Client.Entities;
using System;
using System.Collections.ObjectModel;
using Prism.Regions;

namespace QIQO.Business.Module.Account.ViewModels
{
    public class AccountFinderViewModel : ViewModelBase, IInteractionRequestAware
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IServiceFactory _serviceFactory;
        private readonly IRegionManager _regionManager;
        private ObservableCollection<Client.Entities.Account> _accounts = new ObservableCollection<Client.Entities.Account>();
        private string _viewTitle = "Account Find";
        private string _searchTerm;
        private ItemSelectionNotification notification;
        private bool _isSearching;

        public object SelectedAccount { get; set; }
        public int SelectedIndex { get; set; }

        public AccountFinderViewModel()
        {
            _eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            _serviceFactory = ServiceLocator.Current.GetInstance<IServiceFactory>();
            _regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
            BindCommands();
        }
        public DelegateCommand GetAccountsCommand { get; set; }
        public DelegateCommand ChooseAccountCommand { get; set; }
        public DelegateCommand CloseWindowCommand { get; set; }

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
                    RaisePropertyChanged(nameof(Notification));
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
            get { return _searchTerm; }
            set { SetProperty(ref _searchTerm, value); }
        }

        public bool IsLoading
        {
            get { return _isSearching; }
            private set { SetProperty(ref _isSearching, value); }
        }

        private void BindCommands()
        {
            GetAccountsCommand = new DelegateCommand(GetAccounts);
            ChooseAccountCommand = new DelegateCommand(ChooseAccount);
        }

        private void GetAccounts()
        {
            if (!string.IsNullOrWhiteSpace(SearchTerm))
            {
                IsBusy = true;
                IsLoading = true;
                var account_service = _serviceFactory.CreateClient<IAccountService>();

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
    }
}
