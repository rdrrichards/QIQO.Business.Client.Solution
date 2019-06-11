using QIQO.Business.Client.Contracts;
using System.ServiceModel;
using QIQO.Business.Client.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity;

namespace QIQO.Business.Client.Proxies
{
    public class FeeScheduleClient : IFeeScheduleService
    {
        private IFeeScheduleService channel = null;

        [InjectionConstructor]
        public FeeScheduleClient() : this("NetTcpBinding_IFeeScheduleService") { }

        public FeeScheduleClient(string endpoint)
        {
            channel = new ChannelFactory<IFeeScheduleService>(endpoint).CreateChannel();
        }

        public int CreateFeeSchedule(FeeSchedule fee_schedule)
        {
            return channel.CreateFeeSchedule(fee_schedule);
        }

        public Task<int> CreateFeeScheduleAsync(FeeSchedule fee_schedule)
        {
            return channel.CreateFeeScheduleAsync(fee_schedule);
        }

        public bool DeleteFeeSchedule(FeeSchedule fee_schedule)
        {
            return channel.DeleteFeeSchedule(fee_schedule);
        }

        public Task<bool> DeleteFeeScheduleAsync(FeeSchedule fee_schedule)
        {
            return channel.DeleteFeeScheduleAsync(fee_schedule);
        }

        public FeeSchedule GetFeeSchedule(int fee_schedule)
        {
            return channel.GetFeeSchedule(fee_schedule);
        }

        public Task<FeeSchedule> GetFeeScheduleAsync(int fee_schedule)
        {
            return channel.GetFeeScheduleAsync(fee_schedule);
        }

        public List<FeeSchedule> GetFeeSchedulesByCompany(Company company)
        {
            return channel.GetFeeSchedulesByCompany(company);
        }

        public Task<List<FeeSchedule>> GetFeeSchedulesByCompanyAsync(Company company)
        {
            return channel.GetFeeSchedulesByCompanyAsync(company);
        }

        public List<FeeSchedule> GetFeeSchedulesByAccount(Account account)
        {
            return channel.GetFeeSchedulesByAccount(account);
        }

        public Task<List<FeeSchedule>> GetFeeSchedulesByAccountAsync(Account account)
        {
            return channel.GetFeeSchedulesByAccountAsync(account);
        }

        public List<FeeSchedule> GetFeeSchedulesByProduct(Product product)
        {
            return channel.GetFeeSchedulesByProduct(product);
        }

        public Task<List<FeeSchedule>> GetFeeSchedulesByProductAsync(Product product)
        {
            return channel.GetFeeSchedulesByProductAsync(product);
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