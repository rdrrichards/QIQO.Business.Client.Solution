namespace QIQO.Business.Client.Core
{
    public interface IServiceFactory
    {
        T CreateClient<T>() where T : IServiceContract;
    }
}
