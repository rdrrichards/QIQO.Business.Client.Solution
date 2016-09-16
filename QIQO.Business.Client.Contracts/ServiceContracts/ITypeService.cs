using System.ServiceModel;
using QIQO.Business.Client.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using QIQO.Business.Client.Core;
using System;

namespace QIQO.Business.Client.Contracts
{
    [ServiceContract]
    public interface ITypeService : IServiceContract, IDisposable
    {
        [OperationContract]
        List<AccountType> GetAccountTypeList();

        [OperationContract]
        List<AddressType> GetAddressTypeList();

        [OperationContract]
        List<AttributeType> GetAttributeTypeList();

        [OperationContract]
        List<AttributeType> GetAttributeTypeListByCategory(string category);

        [OperationContract]
        List<CommentType> GetCommentTypeList();

        [OperationContract]
        List<CommentType> GetCommentTypeListByCategory(string category);

        [OperationContract]
        List<ContactType> GetContactTypeList();

        [OperationContract]
        List<ContactType> GetContactTypeListByCategory(string category);

        [OperationContract]
        List<EntityType> GetEntityTypeList();

        [OperationContract]
        List<PersonType> GetPersonTypeList();

        [OperationContract]
        List<PersonType> GetPersonTypeListByCategory(string category);

        [OperationContract]
        List<ProductType> GetProductTypeList();

        [OperationContract]
        List<ProductType> GetProductTypeListByCategory(string category);

        [OperationContract]
        List<OrderStatus> GetOrderStatusList();

        [OperationContract]
        List<OrderItemStatus> GetOrderItemStatusList();

        [OperationContract]
        List<InvoiceStatus> GetInvoiceStatusList();

        [OperationContract]
        List<InvoiceItemStatus> GetInvoiceItemStatusList();






        [OperationContract]
        Task<List<AccountType>> GetAccountTypeListAsync();

        [OperationContract]
        Task<List<AddressType>> GetAddressTypeListAsync();

        [OperationContract]
        Task<List<AttributeType>> GetAttributeTypeListAsync();

        [OperationContract]
        Task<List<AttributeType>> GetAttributeTypeListByCategoryAsync(string category);

        [OperationContract]
        Task<List<CommentType>> GetCommentTypeListAsync();

        [OperationContract]
        Task<List<CommentType>> GetCommentTypeListByCategoryAsync(string category);

        [OperationContract]
        Task<List<ContactType>> GetContactTypeListAsync();

        [OperationContract]
        Task<List<ContactType>> GetContactTypeListByCategoryAsync(string category);

        [OperationContract]
        Task<List<EntityType>> GetEntityTypeListAsync();

        [OperationContract]
        Task<List<PersonType>> GetPersonTypeListAsync();

        [OperationContract]
        Task<List<PersonType>> GetPersonTypeListByCategoryAsync(string category);

        [OperationContract]
        Task<List<ProductType>> GetProductTypeListAsync();

        [OperationContract]
        Task<List<ProductType>> GetProductTypeListByCategoryAsync(string category);

        [OperationContract]
        Task<List<OrderStatus>> GetOrderStatusListAsync();

        [OperationContract]
        Task<List<OrderItemStatus>> GetOrderItemStatusListAsync();

        [OperationContract]
        Task<List<InvoiceStatus>> GetInvoiceStatusListAsync();

        [OperationContract]
        Task<List<InvoiceItemStatus>> GetInvoiceItemStatusListAsync();
    }
}