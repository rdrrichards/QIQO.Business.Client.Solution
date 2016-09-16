using QIQO.Business.Client.Wrappers;
using System.Collections.ObjectModel;

namespace QIQO.Business.Module.Account.Interfaces
{
    public interface IWorkingAccountService
    {
        AccountWrapper GetAccount(string account_key);
        bool OpenAccount(AccountWrapper account);
        ObservableCollection<AccountWrapper> GetWorkingAccounts();
        bool CloseAccount(AccountWrapper account);
        bool ReplaceAccount(string old_code, AccountWrapper account);
    }
}
