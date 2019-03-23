using CommonServiceLocator;
using QIQO.Business.Client.Contracts;
using System.ServiceModel;
using QIQO.Business.Client.Entities;
using System.Threading.Tasks;

namespace QIQO.Business.Client.Proxies
{
    public class SessionClient : ISessionService
    {
        private ISessionService channel = null;

        // [InjectionConstructor]
        public SessionClient() : this("NetTcpBinding_ISessionService") { }

        public SessionClient(string endpoint)
        {
            channel = new ChannelFactory<ISessionService>(endpoint).CreateChannel();
        }

        public UserSession GetSessionObject(int process_id, string host_name, string user_domain, string user_name)
        {
            return channel.GetSessionObject(process_id, host_name, user_domain, user_name);
        }

        public Task<UserSession> GetSessionObjectAsync(int process_id, string host_name, string user_domain, string user_name)
        {
            return channel.GetSessionObjectAsync(process_id, host_name, user_domain, user_name);
        }

        public void RegisterSession(int process_id, string host_name, string user_domain, string user_name, int company_key)
        {
            channel.RegisterSession(process_id, host_name, user_domain, user_name, company_key);
        }

        public void RegisterSessionAsync(int process_id, string host_name, string user_domain, string user_name, int company_key)
        {
            channel.RegisterSessionAsync(process_id, host_name, user_domain, user_name, company_key);
        }

        public void UnregisterSession(int process_id, string host_name, string user_domain, string user_name, int company_key)
        {
            channel.UnregisterSession(process_id, host_name, user_domain, user_name, company_key);
        }

        public void UnregisterSessionAsync(int process_id, string host_name, string user_domain, string user_name, int company_key)
        {
            channel.UnregisterSessionAsync(process_id, host_name, user_domain, user_name, company_key);
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