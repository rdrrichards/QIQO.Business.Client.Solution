using System.ServiceModel;
using QIQO.Business.Client.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using QIQO.Business.Client.Core;
using System;

namespace QIQO.Business.Client.Contracts
{
    [ServiceContract]
    public interface ILedgerService : IServiceContract, IDisposable
    {
        [OperationContract]
        List<ChartOfAccount> GetChartOfAccounts(Company company);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        int CreateChartOfAccount(ChartOfAccount chart_of_account);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        bool DeleteChartOfAccount(ChartOfAccount chart_of_account);

        [OperationContract]
        ChartOfAccount GetChartOfAccount(int chart_of_account_key);



        [OperationContract]
        Task<List<ChartOfAccount>> GetChartOfAccountsAsync(Company company);

        [OperationContract]
        Task<int> CreateChartOfAccountAsync(ChartOfAccount chart_of_account);

        [OperationContract]
        Task<bool> DeleteChartOfAccountAsync(ChartOfAccount chart_of_account);

        [OperationContract]
        Task<ChartOfAccount> GetChartOfAccountAsync(int chart_of_account_key);
    }
}