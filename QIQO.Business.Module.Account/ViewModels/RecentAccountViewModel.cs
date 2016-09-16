using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using QIQO.Business.Client.Core;
using QIQO.Business.Client.Core.Infrastructure;
using QIQO.Business.Client.Core.UI;
using QIQO.Business.Client.Wrappers;
using QIQO.Business.Module.Account.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QIQO.Business.Module.Account.ViewModels
{
    public class RecentAccountViewModel : ViewModelBase
    {
        private readonly IRegionManager _regionManager;
        private readonly IRecentAccountsService _recent_acct_service;
        private readonly IEventAggregator event_aggregator;
        private string _header_msg = "Recent Accounts";
        private ObservableCollection<AccountWrapper> _accounts;
        private object _selected_action;

        public RecentAccountViewModel(IRegionManager regionManager, IRecentAccountsService recent_acct_servc, IEventAggregator event_aggtr)
        {
            _regionManager = regionManager;
            _recent_acct_service = recent_acct_servc;
            event_aggregator = event_aggtr;
            InitializeAccounts();
            GotoAccountCommand = new DelegateCommand(GotoAccount);
            event_aggregator.GetEvent<RecentAccountServiceEvent>().Subscribe(InitializeAccounts);
        }

        private void GotoAccount()
        {
            var account = SelectedAccount as AccountWrapper;
            if (account != null)
            {
                var parameters = new NavigationParameters();
                parameters.Add("AccountKey", account.AccountKey);
                _regionManager.RequestNavigate(RegionNames.ContentRegion, ViewNames.AccountViewX, parameters);
            }
        }

        private void InitializeAccounts(int blah = 0)
        {
            RecentAccountItems = new ObservableCollection<AccountWrapper>(_recent_acct_service.GetRecentAccounts());
        }

        public DelegateCommand GotoAccountCommand { get; set; }
        public int SelectedAccountIndex { get; set; }
        public object SelectedAccount
        {
            get { return _selected_action; }
            set { SetProperty(ref _selected_action, value); }
        }

        public ObservableCollection<AccountWrapper> RecentAccountItems
        {
            get { return _accounts; }
            private set { SetProperty(ref _accounts, value); }
        }

        public string HeaderMessage
        {
            get { return _header_msg; }
            private set { SetProperty(ref _header_msg, value); }
        }
    }
}
