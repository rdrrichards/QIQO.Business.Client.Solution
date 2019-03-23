using QIQO.Business.Client.Core;
using Unity;

namespace QIQO.Business.Client.Proxies
{
    public class ServiceFactory : IServiceFactory
    {
        private readonly IUnityContainer _unityContainer;

        public ServiceFactory(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }
        public T CreateClient<T>() where T : IServiceContract
        {
            return _unityContainer.Resolve<T>();
        }
    }
}
