using System.ServiceModel;
using QIQO.Business.Client.Entities;
using QIQO.Business.Client.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace QIQO.Business.Client.Contracts
{
    [ServiceContract]
    public interface IAddressService : IServiceContract, IDisposable
    {
        [OperationContract]
        List<Address> GetAddressesByEntity(int entity_key, QIQOEntityType entity_type);

        [OperationContract]
        List<Address> GetAddressesByCompany(Company company);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        int CreateAddress(Address address);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        bool DeleteAddress(Address address);

        [OperationContract]
        Address GetAddress(int address_key);

        [OperationContract]
        List<AddressPostal> GetStateListByCountry(string country);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        AddressPostal GetAddressInfoByPostal(string postal_code);




        [OperationContract]
        Task<List<Address>> GetAddressesByEntityAsync(int entity_key, QIQOEntityType entity_type);

        [OperationContract]
        Task<List<Address>> GetAddressesByCompanyAsync(Company company);

        [OperationContract]
        Task<int> CreateAddressAsync(Address address);

        [OperationContract]
        Task<bool> DeleteAddressAsync(Address address);

        [OperationContract]
        Task<Address> GetAddressAsync(int address_key);

        [OperationContract]
        Task<List<AddressPostal>> GetStateListByCountryAsync(string country);

        [OperationContract]
        Task<AddressPostal> GetAddressInfoByPostalAsync(string postal_code);
    }
}