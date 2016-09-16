using Prism.Events;
using QIQO.Business.Client.Core;
using QIQO.Business.Client.Wrappers;
using QIQO.Business.Module.Account.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace QIQO.Business.Module.Account.Services
{
    public class WorkingAccountService : IWorkingAccountService
    {
        private readonly IEventAggregator event_aggregator;

        private Dictionary<string, AccountWrapper> open_accounts;
        public WorkingAccountService(IEventAggregator event_aggtr)
        {
            event_aggregator = event_aggtr;
            Initialize();
        }

        private void Initialize()
        {
            open_accounts = new Dictionary<string, AccountWrapper>();
        }

        public AccountWrapper GetAccount(string account_key)
        {
            if (open_accounts.ContainsKey(account_key))
                return open_accounts[account_key];
            else
                return null;
        }

        public bool OpenAccount(AccountWrapper account)
        {
            if (!open_accounts.ContainsValue(account))
            {
                string new_key = GenAccountKey();
                account.AccountCode = new_key;
                open_accounts.Add(new_key, account);
                event_aggregator.GetEvent<OpenAccountServiceEvent>().Publish(open_accounts.Count);
                return true;
            }
            return false;
        }

        public bool CloseAccount(AccountWrapper account)
        {
            if (open_accounts.ContainsValue(account))
            {
                var key = open_accounts.FirstOrDefault(x => x.Value == account).Key;
                open_accounts.Remove(key);
                event_aggregator.GetEvent<OpenAccountServiceEvent>().Publish(open_accounts.Count);
                return true;
            }
            return false;
        }

        public bool ReplaceAccount(string old_code, AccountWrapper account)
        {
            if (open_accounts.ContainsKey(old_code))
            {
                open_accounts.Remove(old_code);
                open_accounts.Add(account.AccountCode, account);
                event_aggregator.GetEvent<OpenAccountServiceEvent>().Publish(open_accounts.Count);
                return true;
            }
            return false;
        }

        public ObservableCollection<AccountWrapper> GetWorkingAccounts()
        {
            var working_ords = new ObservableCollection<AccountWrapper>();
            foreach (var account_entry in open_accounts)
            {
                working_ords.Add(account_entry.Value);
            }
            return working_ords;
        }

        private string GenAccountKey()
        {
            return $"New Account {open_accounts.Count + 1}";
        }
    }
}
