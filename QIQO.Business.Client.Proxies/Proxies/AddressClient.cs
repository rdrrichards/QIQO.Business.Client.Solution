using System.Collections.Generic;
using System.Threading.Tasks;
using QIQO.Business.Client.Contracts;
using QIQO.Business.Client.Entities;
using System.ServiceModel;
using Unity;

namespace QIQO.Business.Client.Proxies
{
    public class AddressClient : IAddressService
    {
        private IAddressService channel = null;

        [InjectionConstructor]
        public AddressClient() : this("NetTcpBinding_IAddressService") { }

        public AddressClient(string endpoint)
        {
            channel = new ChannelFactory<IAddressService>(endpoint).CreateChannel();
        }
        public int CreateAddress(Address address)
        {
            return channel.CreateAddress(address);
        }

        public Task<int> CreateAddressAsync(Address address)
        {
            return channel.CreateAddressAsync(address);
        }

        public bool DeleteAddress(Address address)
        {
            return channel.DeleteAddress(address);
        }

        public Task<bool> DeleteAddressAsync(Address address)
        {
            return channel.DeleteAddressAsync(address);
        }

        public Address GetAddress(int address_key)
        {
            return channel.GetAddress(address_key);
        }

        public Task<Address> GetAddressAsync(int address_key)
        {
            return channel.GetAddressAsync(address_key);
        }

        public List<Address> GetAddressesByCompany(Company company)
        {
            return channel.GetAddressesByCompany(company);
        }

        public Task<List<Address>> GetAddressesByCompanyAsync(Company company)
        {
            return channel.GetAddressesByCompanyAsync(company);
        }

        public List<Address> GetAddressesByEntity(int entity_key, QIQOEntityType entity_type)
        {
            return channel.GetAddressesByEntity(entity_key, entity_type);
        }

        public Task<List<Address>> GetAddressesByEntityAsync(int entity_key, QIQOEntityType entity_type)
        {
            return channel.GetAddressesByEntityAsync(entity_key, entity_type);
        }

        public AddressPostal GetAddressInfoByPostal(string postal_code)
        {
            return channel.GetAddressInfoByPostal(postal_code);
        }

        public Task<AddressPostal> GetAddressInfoByPostalAsync(string postal_code)
        {
            return channel.GetAddressInfoByPostalAsync(postal_code);
        }

        public List<AddressPostal> GetStateListByCountry(string country)
        {
            return channel.GetStateListByCountry(country);
        }

        public Task<List<AddressPostal>> GetStateListByCountryAsync(string country)
        {
            return channel.GetStateListByCountryAsync(country);
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