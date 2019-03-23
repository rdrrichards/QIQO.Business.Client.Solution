using CommonServiceLocator;
using QIQO.Business.Client.Contracts;
using System.ServiceModel;
using QIQO.Business.Client.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QIQO.Business.Client.Proxies
{
    public class LedgerClient : ILedgerService
    {
        private ILedgerService channel = null;

        // [InjectionConstructor]
        public LedgerClient() : this("NetTcpBinding_ILedgerService") { }

        public LedgerClient(string endpoint)
        {
            channel = new ChannelFactory<ILedgerService>(endpoint).CreateChannel();
        }

        public int CreateChartOfAccount(ChartOfAccount chart_of_account)
        {
            return channel.CreateChartOfAccount(chart_of_account);
        }

        public Task<int> CreateChartOfAccountAsync(ChartOfAccount chart_of_account)
        {
            return channel.CreateChartOfAccountAsync(chart_of_account);
        }

        public bool DeleteChartOfAccount(ChartOfAccount chart_of_account)
        {
            return channel.DeleteChartOfAccount(chart_of_account);
        }

        public Task<bool> DeleteChartOfAccountAsync(ChartOfAccount chart_of_account)
        {
            return channel.DeleteChartOfAccountAsync(chart_of_account);
        }

        public ChartOfAccount GetChartOfAccount(int chart_of_account_key)
        {
            return channel.GetChartOfAccount(chart_of_account_key);
        }

        public Task<ChartOfAccount> GetChartOfAccountAsync(int chart_of_account_key)
        {
            return channel.GetChartOfAccountAsync(chart_of_account_key);
        }

        public List<ChartOfAccount> GetChartOfAccounts(Company company)
        {
            return channel.GetChartOfAccounts(company);
        }

        public Task<List<ChartOfAccount>> GetChartOfAccountsAsync(Company company)
        {
            return channel.GetChartOfAccountsAsync(company);
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