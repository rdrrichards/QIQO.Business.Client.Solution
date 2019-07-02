using QIQO.Business.Client.Core;
using QIQO.Business.Client.Entities;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace QIQO.Business.Client.Contracts
{
    [ServiceContract]
    public interface IAuditService : IServiceContract, IDisposable
    {
        [OperationContract]
        List<AuditLog> GetAuditLogByBusinessObject(string business_object);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        int CreateAuditLog(AuditLog audit_log);


        [OperationContract]
        Task<List<AuditLog>> GetAuditLogByBusinessObjectAsync(string business_object);

        [OperationContract]
        Task<int> CreateAuditLogAsync(AuditLog audit_log);
    }
}