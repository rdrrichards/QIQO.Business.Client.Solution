using Microsoft.Practices.Unity;
using QIQO.Business.Client.Contracts;
using System.ServiceModel;
using QIQO.Business.Client.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QIQO.Business.Client.Proxies
{
    public class CompanyClient : ICompanyService
    {
        private ICompanyService channel = null;

        [InjectionConstructor]
        public CompanyClient() : this("NetTcpBinding_ICompanyService") { }

        public CompanyClient(string endpoint)
        {
            channel = new ChannelFactory<ICompanyService>(endpoint).CreateChannel();
        }

        public int CompanyAddEmployee(Company company, Employee emp, string role, string comment)
        {
            return channel.CompanyAddEmployee(company, emp, role, comment);
        }

        public Task<int> CompanyAddEmployeeAsync(Company company, Employee emp, string role, string comment)
        {
            return channel.CompanyAddEmployeeAsync(company, emp, role, comment);
        }

        public bool CompanyDeleteEmployee(Company company, Employee emp)
        {
            return channel.CompanyDeleteEmployee(company, emp);
        }

        public Task<bool> CompanyDeleteEmployeeAsync(Company company, Employee emp)
        {
            return channel.CompanyDeleteEmployeeAsync(company, emp);
        }

        public int CreateCompany(Company company)
        {
            return channel.CreateCompany(company);
        }

        public Task<int> CreateCompanyAsync(Company company)
        {
            return channel.CreateCompanyAsync(company);
        }

        public bool DeleteCompany(Company company)
        {
            return channel.DeleteCompany(company);
        }

        public Task<bool> DeleteCompanyAsync(Company company)
        {
            return channel.DeleteCompanyAsync(company);
        }

        public List<Company> GetCompanies(Employee emp)
        {
            return channel.GetCompanies(emp);
        }

        public Task<List<Company>> GetCompaniesAsync(Employee emp)
        {
            return channel.GetCompaniesAsync(emp);
        }

        public Company GetCompany(int company_key)
        {
            return channel.GetCompany(company_key);
        }

        public Task<Company> GetCompanyAsync(int company_key)
        {
            return channel.GetCompanyAsync(company_key);
        }

        public string GetCompanyNextNumber(Company company, QIQOEntityNumberType number_type)
        {
            return channel.GetCompanyNextNumber(company, number_type);
        }

        public Task<string> GetCompanyNextNumberAsync(Company company, QIQOEntityNumberType number_type)
        {
            return channel.GetCompanyNextNumberAsync(company, number_type);
        }

        public string GetEmployeeRoleInCompany(Employee emp)
        {
            return channel.GetEmployeeRoleInCompany(emp);
        }

        public Task<string> GetEmployeeRoleInCompanyAsync(Employee emp)
        {
            return channel.GetEmployeeRoleInCompanyAsync(emp);
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