using Microsoft.Practices.Unity;
using Prism.Events;
using QIQO.Business.Client.Contracts;
using QIQO.Business.Client.Core;
using QIQO.Business.Client.Core.UI;
using QIQO.Business.Client.Entities;
using QIQO.Business.Client.Wrappers;
using QIQO.Business.Module.Account.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace QIQO.Business.Module.Account.Services
{
    public class RecentAccountsService : IRecentAccountsService
    {
        private readonly IEventAggregator event_aggregator;
        private readonly IServiceFactory service_factory;
        private Queue<AccountWrapper> recently_viewed_accounts;
        public RecentAccountsService(IEventAggregator event_aggtr, IServiceFactory service_fctry, ICurrentCompanyService company)
        {
            CurrentCompany = company.CurrentCompany;
            event_aggregator = event_aggtr;
            service_factory = service_fctry;
            Initialize();
        }

        public Queue<AccountWrapper> GetRecentAccounts()
        {
            //var recent_accounts = new Queue<AccountWrapper>();
            //foreach (var account_entry in recently_viewed_accounts)
            //{
            //    recent_accounts.Enqueue(account_entry.Value);
            //}
            return recently_viewed_accounts;
        }

        public bool PushAccount(AccountWrapper account)
        {
            var acct_in_queue = recently_viewed_accounts.Where(a => a.AccountCode == account.AccountCode).FirstOrDefault();
            if (acct_in_queue == null)
            {
                var accounts = "";
                recently_viewed_accounts.Enqueue(account);
                //var recent_account_nums = Properties.Settings.Default.RecentViewedAccounts.Split(',').ToList();
                //recent_account_nums.Add(account.AccountCode);

                if (recently_viewed_accounts.Count > 10) recently_viewed_accounts.Dequeue();

                var rec_acct_rev = recently_viewed_accounts.Reverse();
                foreach (var acct in rec_acct_rev)
                {
                    accounts += acct.AccountCode + ",";
                }
                Properties.Settings.Default.RecentViewedAccounts = accounts;
                Properties.Settings.Default.Save();
                event_aggregator.GetEvent<RecentAccountServiceEvent>().Publish(recently_viewed_accounts.Count);
                return true;
            }
            return false;
        }

        private void Initialize()
        {
            recently_viewed_accounts = new Queue<AccountWrapper>();
            var recent_account_nums = Properties.Settings.Default.RecentViewedAccounts.Split(',');
            foreach(var acct_no in recent_account_nums)
            {
                if (acct_no.Trim().Length != 0)
                    recently_viewed_accounts.Enqueue(LoadAccount(acct_no));
            }
        }
        private object CurrentCompany { get; }

        private AccountWrapper LoadAccount(string account_no)
        {
            if (account_no != null & account_no != "")
            {
                var curr_co = CurrentCompany as Company;

                var account_service = service_factory.CreateClient<IAccountService>();
                using (account_service)
                {
                    Client.Entities.Account _account = account_service.GetAccountByCode(account_no, curr_co.CompanyCode);

                    if (_account != null)
                    {
                        return new AccountWrapper(_account);
                    }
                }
            }
            return null;
        }
    }
}
