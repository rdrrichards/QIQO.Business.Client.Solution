using QIQO.Business.Client.Core;
using QIQO.Business.Client.Entities;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace QIQO.Business.Client.Contracts
{
    [ServiceContract]
    public interface IOrderService : IServiceContract, IDisposable
    {
        [OperationContract]
        List<Order> GetOrdersByAccount(Account account);

        [OperationContract]
        List<Order> GetOrdersByCompany(Company company);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        int CreateOrder(Order order);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        bool DeleteOrder(Order order);

        [OperationContract]
        Order GetOrder(int order_key);

        [OperationContract]
        List<Order> FindOrdersByCompany(Company company, string search_pattern);

        [OperationContract]
        List<Order> GetInvoicableOrdersByAccount(int company_key, int account_key);


        [OperationContract]
        Task<List<Order>> GetOrdersByAccountAsync(Account account);

        [OperationContract]
        Task<List<Order>> GetOrdersByCompanyAsync(Company company);

        [OperationContract]
        Task<int> CreateOrderAsync(Order order);

        [OperationContract]
        Task<bool> DeleteOrderAsync(Order order);

        [OperationContract]
        Task<Order> GetOrderAsync(int order_key);

        [OperationContract]
        Task<List<Order>> FindOrdersByCompanyAsync(Company company, string search_pattern);

        [OperationContract]
        Task<List<Order>> GetInvoicableOrdersByAccountAsync(int company_key, int account_key);
    }
}