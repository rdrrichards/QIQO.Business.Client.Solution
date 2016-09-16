using System.ServiceModel;
using QIQO.Business.Client.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using QIQO.Business.Client.Core;
using System;

namespace QIQO.Business.Client.Contracts
{
    [ServiceContract]
    public interface IFeeScheduleService : IServiceContract, IDisposable
    {
        [OperationContract]
        List<FeeSchedule> GetFeeSchedulesByAccount(Account account);

        [OperationContract]
        List<FeeSchedule> GetFeeSchedulesByCompany(Company company);

        [OperationContract]
        List<FeeSchedule> GetFeeSchedulesByProduct(Product product);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        int CreateFeeSchedule(FeeSchedule fee_schedule);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        bool DeleteFeeSchedule(FeeSchedule fee_schedule);

        [OperationContract]
        FeeSchedule GetFeeSchedule(int fee_schedule);



        [OperationContract]
        Task<List<FeeSchedule>> GetFeeSchedulesByAccountAsync(Account account);

        [OperationContract]
        Task<List<FeeSchedule>> GetFeeSchedulesByCompanyAsync(Company company);

        [OperationContract]
        Task<List<FeeSchedule>> GetFeeSchedulesByProductAsync(Product product);

        [OperationContract]
        Task<int> CreateFeeScheduleAsync(FeeSchedule fee_schedule);

        [OperationContract]
        Task<bool> DeleteFeeScheduleAsync(FeeSchedule fee_schedule);

        [OperationContract]
        Task<FeeSchedule> GetFeeScheduleAsync(int fee_schedule);
    }
}