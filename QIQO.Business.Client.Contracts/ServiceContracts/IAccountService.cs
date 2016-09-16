using System.ServiceModel;
using QIQO.Business.Client.Entities;
using QIQO.Business.Client.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace QIQO.Business.Client.Contracts
{
    [ServiceContract]
    public interface IAccountService : IServiceContract, IDisposable
    {
        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        [FaultContract(typeof(AuthorizationValidationException))]
        Account GetAccountByID(int account_key, bool full_load);

        [OperationContract]
        List<Account> GetAccountsByEmployee(Employee employee);

        [OperationContract]
        List<Account> GetAccountsByCompany(Company company);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        int CreateAccount(Account account);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        bool DeleteAccount(Account account);
        
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        string GetAccountNextNumber(Account account, QIQOEntityNumberType number_type);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        [FaultContract(typeof(AuthorizationValidationException))]
        Account GetAccountByCode(string account_code, string company_code);

        [OperationContract]
        List<Account> FindAccountByCompany(Company company, string pattern);



        [OperationContract]
        Task<Account> GetAccountByIDAsync(int account_key, bool full_load);

        [OperationContract]
        Task<List<Account>> GetAccountsByEmployeeAsync(Employee employee);

        [OperationContract]
        Task<List<Account>> GetAccountsByCompanyAsync(Company company);

        [OperationContract]
        Task<int> CreateAccountAsync(Account account);

        [OperationContract]
        Task<bool> DeleteAccountAsync(Account account);

        [OperationContract]
        Task<string> GetAccountNextNumberAsync(Account account, QIQOEntityNumberType number_type);

        [OperationContract]
        Task<Account> GetAccountByCodeAsync(string account_code, string company_code);

        [OperationContract]
        Task<List<Account>> FindAccountByCompanyAsync(Company company, string pattern);
    }
}
