using Microsoft.Practices.Unity;
using QIQO.Business.Client.Contracts;
using System.ServiceModel;
using QIQO.Business.Client.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace QIQO.Business.Client.Proxies
{
    public class InvoiceClient : IInvoiceService
    {
        private IInvoiceService channel = null;

        [InjectionConstructor]
        public InvoiceClient() : this("NetTcpBinding_IInvoiceService") { }

        public InvoiceClient(string endpoint)
        {
            channel = new ChannelFactory<IInvoiceService>(endpoint).CreateChannel();
        }

        public int CreateInvoice(Invoice invoice)
        {
            return channel.CreateInvoice(invoice);
        }

        public Task<int> CreateInvoiceAsync(Invoice invoice)
        {
            return channel.CreateInvoiceAsync(invoice);
        }

        public bool DeleteInvoice(Invoice invoice)
        {
            return channel.DeleteInvoice(invoice);
        }

        public Task<bool> DeleteInvoiceAsync(Invoice invoice)
        {
            return channel.DeleteInvoiceAsync(invoice);
        }

        public Invoice GetInvoice(int invoice)
        {
            return channel.GetInvoice(invoice);
        }

        public Task<Invoice> GetInvoiceAsync(int invoice)
        {
            return channel.GetInvoiceAsync(invoice);
        }

        public List<Invoice> GetInvoicesByAccount(Account account)
        {
            return channel.GetInvoicesByAccount(account);
        }

        public Task<List<Invoice>> GetInvoicesByAccountAsync(Account account)
        {
            return channel.GetInvoicesByAccountAsync(account);
        }

        public List<Invoice> GetInvoicesByCompany(Company company)
        {
            return channel.GetInvoicesByCompany(company);
        }

        public Task<List<Invoice>> GetInvoicesByCompanyAsync(Company company)
        {
            return channel.GetInvoicesByCompanyAsync(company);
        }

        public List<Invoice> FindInvoicesByCompany(Company company, string search_pattern)
        {
            return channel.FindInvoicesByCompany(company, search_pattern);
        }

        public Task<List<Invoice>> FindInvoicesByCompanyAsync(Company company, string search_pattern)
        {
            return channel.FindInvoicesByCompanyAsync(company, search_pattern);
        }

        public void Dispose()
        {
            if (channel != null)
            {
                channel.Dispose();
                channel = null;
            }
        }

        public InvoiceItem GetInvoiceItemByOrderItemKey(int order_item_key)
        {
            return channel.GetInvoiceItemByOrderItemKey(order_item_key);
        }

        public Task<InvoiceItem> GetInvoiceItemByOrderItemKeyAsync(int order_item_key)
        {
            return channel.GetInvoiceItemByOrderItemKeyAsync(order_item_key);
        }
    }
}