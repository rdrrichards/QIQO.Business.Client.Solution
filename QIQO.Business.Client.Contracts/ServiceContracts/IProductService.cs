using QIQO.Business.Client.Core;
using QIQO.Business.Client.Entities;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace QIQO.Business.Client.Contracts
{
    [ServiceContract]
    public interface IProductService : IServiceContract, IDisposable
    {
        [OperationContract]
        List<Product> GetAllProducts();

        [OperationContract]
        List<Product> GetProducts(Company company);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        int CreateProduct(Product product);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        bool DeleteProduct(Product product);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Product GetProduct(int product_key);



        [OperationContract]
        Task<List<Product>> GetAllProductsAsync();

        [OperationContract]
        Task<List<Product>> GetProductsAsync(Company company);

        [OperationContract]
        Task<int> CreateProductAsync(Product product);

        [OperationContract]
        Task<bool> DeleteProductAsync(Product product);

        [OperationContract]
        Task<Product> GetProductAsync(int product_key);
    }
}