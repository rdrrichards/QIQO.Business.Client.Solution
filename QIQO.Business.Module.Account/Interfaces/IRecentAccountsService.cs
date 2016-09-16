using QIQO.Business.Client.Wrappers;
using System.Collections.Generic;

namespace QIQO.Business.Module.Account.Interfaces
{
    public interface IRecentAccountsService
    {
        Queue<AccountWrapper> GetRecentAccounts();
        bool PushAccount(AccountWrapper account);
    }
}
