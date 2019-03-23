using QIQO.Business.Client.Core;
using CommonServiceLocator;

namespace QIQO.Business.Client.Proxies
{
    public class ServiceFactory : IServiceFactory
    {
        public T CreateClient<T>() where T : IServiceContract
        {
            return ServiceLocator.Current.GetInstance<T>();
        }
    }
}
