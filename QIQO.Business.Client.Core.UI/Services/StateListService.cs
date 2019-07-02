using QIQO.Business.Client.Contracts;
using QIQO.Business.Client.Entities;
using System.Collections.Generic;

namespace QIQO.Business.Client.Core.UI
{
    public class StateListService : IStateListService
    {
        readonly IServiceFactory _service_factor;
        const string country = "United States";
        public StateListService(IServiceFactory service_factory)
        {
            _service_factor = service_factory;
            Initialize();
        }

        private void Initialize()
        {
            var address_service = _service_factor.CreateClient<IAddressService>();
            using (address_service)
            {
                try
                {
                    var prod_list = address_service.GetStateListByCountry(country);
                    StateList = prod_list;
                }
                catch
                {
                    StateList = new List<AddressPostal>();
                }
            }
        }

        public List<AddressPostal> StateList { get; private set; }
    }
}
