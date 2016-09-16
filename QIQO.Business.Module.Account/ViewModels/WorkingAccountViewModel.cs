using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using QIQO.Business.Client.Core;
using QIQO.Business.Client.Core.Infrastructure;
using QIQO.Business.Client.Core.UI;
using QIQO.Business.Client.Wrappers;
using QIQO.Business.Module.Account.Interfaces;
using QIQO.Business.Module.Account.Views;
using System.Collections.ObjectModel;

namespace QIQO.Business.Module.Account.ViewModels
{
    public class WorkingAccountViewModel : ViewModelBase, IRegionMemberLifetime
    {
        private readonly IWorkingAccountService working_accounts_service;
        private readonly IEventAggregator event_aggregator;
        private readonly IRegionManager _regionManager;
        private ObservableCollection<AccountWrapper> _working_accounts;
        private object _selected_account;
        private string _header_msg = "Working Accounts";

        public WorkingAccountViewModel(IWorkingAccountService working_accounts_svc, IEventAggregator event_aggtr, IRegionManager regionManager)
        {
            working_accounts_service = working_accounts_svc;
            event_aggregator = event_aggtr;
            _regionManager = regionManager;

            OpenAccountCommand = new DelegateCommand(OpenAccount);
            InitWorkingAccountList();
            event_aggregator.GetEvent<OpenAccountServiceEvent>().Subscribe(OnOpenAccountChangedEvent, ThreadOption.BackgroundThread);
        }

        private void OnOpenAccountChangedEvent(int open_account_cnt)
        {
            InitWorkingAccountList();
            HeaderMessage = $"Working Accounts ({WorkingAccounts.Count})";
        }
        public bool IsLoading => false;

        private void InitWorkingAccountList()
        {
            WorkingAccounts = working_accounts_service.GetWorkingAccounts();
        }

        public bool KeepAlive => false;
        public DelegateCommand OpenAccountCommand { get; set; }

        public ObservableCollection<AccountWrapper> WorkingAccounts
        {
            get { return _working_accounts; }
            private set { SetProperty(ref _working_accounts, value); }
        }

        public int SelectedAccountIndex { get; set; }
        public object SelectedAccount
        {
            get { return _selected_account; }
            set { SetProperty(ref _selected_account, value); }
        }

        public string HeaderMessage
        {
            get { return _header_msg; }
            private set { SetProperty(ref _header_msg, value); }
        }
        private void OpenAccount()
        {
            var selectedAccount = SelectedAccount as AccountWrapper;
            if (selectedAccount != null)
            {
                var parameters = new NavigationParameters();
                parameters.Add("AccountCode", selectedAccount.AccountCode);
                _regionManager.RequestNavigate(RegionNames.ContentRegion, typeof(AccountViewX).FullName, parameters);
            }
        }
    }
}
