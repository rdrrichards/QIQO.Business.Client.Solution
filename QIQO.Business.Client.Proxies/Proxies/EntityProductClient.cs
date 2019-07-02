using QIQO.Business.Client.Contracts;
using QIQO.Business.Client.Entities;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using Unity;

namespace QIQO.Business.Client.Proxies
{
    public class EntityProductClient : IEntityProductService
    {
        private IEntityProductService channel = null;

        [InjectionConstructor]
        public EntityProductClient() : this("NetTcpBinding_IEntityProductService") { }

        public EntityProductClient(string endpoint)
        {
            channel = new ChannelFactory<IEntityProductService>(endpoint).CreateChannel();
        }

        public int CreateEntityProduct(EntityProduct product)
        {
            return channel.CreateEntityProduct(product);
        }

        public Task<int> CreateEntityProductAsync(EntityProduct product)
        {
            return channel.CreateEntityProductAsync(product);
        }

        public bool DeleteEntityProduct(EntityProduct product)
        {
            return channel.DeleteEntityProduct(product);
        }

        public Task<bool> DeleteEntityProductAsync(EntityProduct product)
        {
            return channel.DeleteEntityProductAsync(product);
        }

        public List<EntityProduct> GetAllEntityProducts()
        {
            return channel.GetAllEntityProducts();
        }

        public Task<List<EntityProduct>> GetAllEntityProductsAsync()
        {
            return channel.GetAllEntityProductsAsync();
        }

        public EntityProduct GetEntityProduct(int product_key)
        {
            return channel.GetEntityProduct(product_key);
        }

        public Task<EntityProduct> GetEntityProductAsync(int product_key)
        {
            return channel.GetEntityProductAsync(product_key);
        }

        public List<EntityProduct> GetEntityProducts(Company company)
        {
            return channel.GetEntityProducts(company);
        }

        public Task<List<EntityProduct>> GetEntityProductsAsync(Company company)
        {
            return channel.GetEntityProductsAsync(company);
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