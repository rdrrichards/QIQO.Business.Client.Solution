using QIQO.Business.Client.Contracts;
using QIQO.Business.Client.Entities;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using Unity;

namespace QIQO.Business.Client.Proxies
{
    public class OrderClient : IOrderService
    {
        private IOrderService channel = null;

        [InjectionConstructor]
        public OrderClient() : this("NetTcpBinding_IOrderService") { }

        public OrderClient(string endpoint)
        {
            channel = new ChannelFactory<IOrderService>(endpoint).CreateChannel();
        }

        public int CreateOrder(Order order)
        {
            return channel.CreateOrder(order);
        }

        public Task<int> CreateOrderAsync(Order order)
        {
            return channel.CreateOrderAsync(order);
        }

        public bool DeleteOrder(Order order)
        {
            return channel.DeleteOrder(order);
        }

        public Task<bool> DeleteOrderAsync(Order order)
        {
            return channel.DeleteOrderAsync(order);
        }

        public Order GetOrder(int order_key)
        {
            return channel.GetOrder(order_key);
        }

        public Task<Order> GetOrderAsync(int order_key)
        {
            return channel.GetOrderAsync(order_key);
        }

        public List<Order> GetOrdersByAccount(Account account)
        {
            return channel.GetOrdersByAccount(account);
        }

        public Task<List<Order>> GetOrdersByAccountAsync(Account account)
        {
            return channel.GetOrdersByAccountAsync(account);
        }

        public List<Order> GetOrdersByCompany(Company company)
        {
            return channel.GetOrdersByCompany(company);
        }

        public Task<List<Order>> GetOrdersByCompanyAsync(Company company)
        {
            return channel.GetOrdersByCompanyAsync(company);
        }

        public List<Order> FindOrdersByCompany(Company company, string search_pattern)
        {
            return channel.FindOrdersByCompany(company, search_pattern);
        }

        public Task<List<Order>> FindOrdersByCompanyAsync(Company company, string search_pattern)
        {
            return channel.FindOrdersByCompanyAsync(company, search_pattern);
        }

        public void Dispose()
        {
            if (channel != null)
            {
                channel.Dispose();
                channel = null;
            }
        }

        public List<Order> GetInvoicableOrdersByAccount(int company_key, int account_key)
        {
            return channel.GetInvoicableOrdersByAccount(company_key, account_key);
        }

        public Task<List<Order>> GetInvoicableOrdersByAccountAsync(int company_key, int account_key)
        {
            return channel.GetInvoicableOrdersByAccountAsync(company_key, account_key);
        }
    }
}