using QIQO.Business.Client.Contracts;
using QIQO.Business.Client.Entities;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using Unity;

namespace QIQO.Business.Client.Proxies
{
    public class ProductClient : IProductService
    {
        private IProductService channel = null;

        [InjectionConstructor]
        public ProductClient() : this("NetTcpBinding_IProductService") { }

        public ProductClient(string endpoint)
        {
            channel = new ChannelFactory<IProductService>(endpoint).CreateChannel();
        }

        public int CreateProduct(Product product)
        {
            return channel.CreateProduct(product);
        }

        public Task<int> CreateProductAsync(Product product)
        {
            return channel.CreateProductAsync(product);
        }

        public bool DeleteProduct(Product product)
        {
            return channel.DeleteProduct(product);
        }

        public Task<bool> DeleteProductAsync(Product product)
        {
            return channel.DeleteProductAsync(product);
        }

        public List<Product> GetAllProducts()
        {
            return channel.GetAllProducts();
        }

        public Task<List<Product>> GetAllProductsAsync()
        {
            return channel.GetAllProductsAsync();
        }

        public Product GetProduct(int product_key)
        {
            return channel.GetProduct(product_key);
        }

        public Task<Product> GetProductAsync(int product_key)
        {
            return channel.GetProductAsync(product_key);
        }

        public List<Product> GetProducts(Company company)
        {
            return channel.GetProducts(company);
        }

        public Task<List<Product>> GetProductsAsync(Company company)
        {
            return channel.GetProductsAsync(company);
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