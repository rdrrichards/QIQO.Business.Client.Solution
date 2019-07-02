using QIQO.Business.Client.Contracts;
using QIQO.Business.Client.Entities;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using Unity;

namespace QIQO.Business.Client.Proxies
{
    public class TypeClient : ITypeService
    {
        private ITypeService channel = null;

        [InjectionConstructor]
        public TypeClient() : this("NetTcpBinding_ITypeService") { }

        public TypeClient(string endpoint)
        {
            channel = new ChannelFactory<ITypeService>(endpoint).CreateChannel();
        }

        public List<AccountType> GetAccountTypeList()
        {
            return channel.GetAccountTypeList();
        }

        public Task<List<AccountType>> GetAccountTypeListAsync()
        {
            return channel.GetAccountTypeListAsync();
        }

        public List<AddressType> GetAddressTypeList()
        {
            return channel.GetAddressTypeList();
        }

        public Task<List<AddressType>> GetAddressTypeListAsync()
        {
            return channel.GetAddressTypeListAsync();
        }

        public List<AttributeType> GetAttributeTypeList()
        {
            return channel.GetAttributeTypeList();
        }

        public Task<List<AttributeType>> GetAttributeTypeListAsync()
        {
            return channel.GetAttributeTypeListAsync();
        }

        public List<AttributeType> GetAttributeTypeListByCategory(string category)
        {
            return channel.GetAttributeTypeListByCategory(category);
        }

        public Task<List<AttributeType>> GetAttributeTypeListByCategoryAsync(string category)
        {
            return channel.GetAttributeTypeListByCategoryAsync(category);
        }

        public List<CommentType> GetCommentTypeList()
        {
            return channel.GetCommentTypeList();
        }

        public Task<List<CommentType>> GetCommentTypeListAsync()
        {
            return channel.GetCommentTypeListAsync();
        }

        public List<CommentType> GetCommentTypeListByCategory(string category)
        {
            return channel.GetCommentTypeListByCategory(category);
        }

        public Task<List<CommentType>> GetCommentTypeListByCategoryAsync(string category)
        {
            return channel.GetCommentTypeListByCategoryAsync(category);
        }

        public List<ContactType> GetContactTypeList()
        {
            return channel.GetContactTypeList();
        }

        public Task<List<ContactType>> GetContactTypeListAsync()
        {
            return channel.GetContactTypeListAsync();
        }

        public List<ContactType> GetContactTypeListByCategory(string category)
        {
            return channel.GetContactTypeListByCategory(category);
        }

        public Task<List<ContactType>> GetContactTypeListByCategoryAsync(string category)
        {
            return channel.GetContactTypeListByCategoryAsync(category);
        }

        public List<EntityType> GetEntityTypeList()
        {
            return channel.GetEntityTypeList();
        }

        public Task<List<EntityType>> GetEntityTypeListAsync()
        {
            return channel.GetEntityTypeListAsync();
        }

        public List<InvoiceItemStatus> GetInvoiceItemStatusList()
        {
            return channel.GetInvoiceItemStatusList();
        }

        public Task<List<InvoiceItemStatus>> GetInvoiceItemStatusListAsync()
        {
            return channel.GetInvoiceItemStatusListAsync();
        }

        public List<InvoiceStatus> GetInvoiceStatusList()
        {
            return channel.GetInvoiceStatusList();
        }

        public Task<List<InvoiceStatus>> GetInvoiceStatusListAsync()
        {
            return channel.GetInvoiceStatusListAsync();
        }

        public List<OrderItemStatus> GetOrderItemStatusList()
        {
            return channel.GetOrderItemStatusList();
        }

        public Task<List<OrderItemStatus>> GetOrderItemStatusListAsync()
        {
            return channel.GetOrderItemStatusListAsync();
        }

        public List<OrderStatus> GetOrderStatusList()
        {
            return channel.GetOrderStatusList();
        }

        public Task<List<OrderStatus>> GetOrderStatusListAsync()
        {
            return channel.GetOrderStatusListAsync();
        }

        public List<PersonType> GetPersonTypeList()
        {
            return channel.GetPersonTypeList();
        }

        public Task<List<PersonType>> GetPersonTypeListAsync()
        {
            return channel.GetPersonTypeListAsync();
        }

        public List<PersonType> GetPersonTypeListByCategory(string category)
        {
            return channel.GetPersonTypeListByCategory(category);
        }

        public Task<List<PersonType>> GetPersonTypeListByCategoryAsync(string category)
        {
            return channel.GetPersonTypeListByCategoryAsync(category);
        }

        public List<ProductType> GetProductTypeList()
        {
            return channel.GetProductTypeList();
        }

        public Task<List<ProductType>> GetProductTypeListAsync()
        {
            return channel.GetProductTypeListAsync();
        }

        public List<ProductType> GetProductTypeListByCategory(string category)
        {
            return channel.GetProductTypeListByCategory(category);
        }

        public Task<List<ProductType>> GetProductTypeListByCategoryAsync(string category)
        {
            return channel.GetProductTypeListByCategoryAsync(category);
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