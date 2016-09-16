using QIQO.Business.Client.Core;
using Microsoft.Practices.Unity;

namespace QIQO.Business.Client.Proxies
{
    public class ServiceFactory : IServiceFactory
    {
        public T CreateClient<T>() where T : IServiceContract
        {
            return Unity.Container.Resolve<T>();
        }
    }
}
