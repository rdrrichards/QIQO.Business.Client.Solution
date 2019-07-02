using QIQO.Business.Client.Contracts;
using QIQO.Business.Client.Entities;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using Unity;

namespace QIQO.Business.Client.Proxies
{
    public class AuditClient : IAuditService
    {
        private IAuditService channel = null;

        [InjectionConstructor]
        public AuditClient() : this("NetTcpBinding_IAuditService") { }

        public AuditClient(string endpoint)
        {
            channel = new ChannelFactory<IAuditService>(endpoint).CreateChannel();
        }

        public int CreateAuditLog(AuditLog audit_log)
        {
            return channel.CreateAuditLog(audit_log);
        }

        public Task<int> CreateAuditLogAsync(AuditLog audit_log)
        {
            return channel.CreateAuditLogAsync(audit_log);
        }

        public List<AuditLog> GetAuditLogByBusinessObject(string business_object)
        {
            return channel.GetAuditLogByBusinessObject(business_object);
        }

        public Task<List<AuditLog>> GetAuditLogByBusinessObjectAsync(string business_object)
        {
            return channel.GetAuditLogByBusinessObjectAsync(business_object);
        }

        public void Dispose()
        {
            if (channel != null)
            {
                channel.Dispose();
                channel = null;
            }
        }
    }

}
