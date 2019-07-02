using QIQO.Business.Client.Contracts;
using QIQO.Business.Client.Entities;
using System.Collections.Generic;

namespace QIQO.Business.Client.Core.UI
{
    public class FeeScheduleEntityService : IFeeScheduleEntityService
    {
        readonly IServiceFactory _service_factor;
        public FeeScheduleEntityService(IServiceFactory service_factory)
        {
            _service_factor = service_factory;
        }

        public List<FeeSchedule> GetFeeSchedules(Product product)
        {
            var fs_service = _service_factor.CreateClient<IFeeScheduleService>();
            using (fs_service)
            {
                try
                {
                    var fs_list = fs_service.GetFeeSchedulesByProductAsync(product);
                    return new List<FeeSchedule>(fs_list.Result);
                }
                catch
                {
                    return new List<FeeSchedule>();
                }
            }
        }

        public List<FeeSchedule> GetFeeSchedules(Account account)
        {
            var fs_service = _service_factor.CreateClient<IFeeScheduleService>();
            using (fs_service)
            {
                try
                {
                    var fs_list = fs_service.GetFeeSchedulesByAccountAsync(account);
                    return new List<FeeSchedule>(fs_list.Result);
                }
                catch
                {
                    return new List<FeeSchedule>();
                }
            }
        }
    }
}
