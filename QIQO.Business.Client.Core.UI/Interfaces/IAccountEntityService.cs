using QIQO.Business.Client.Entities;

namespace QIQO.Business.Client.Core.UI
{
    public interface IAccountEntityService
    {
        Account InitNewAccount(int company_key);
        Account InitNewAccount(Company company);
    }
}
