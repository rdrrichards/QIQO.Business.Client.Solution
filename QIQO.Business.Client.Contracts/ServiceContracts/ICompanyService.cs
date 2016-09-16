using System.ServiceModel;
using QIQO.Business.Client.Entities;
using QIQO.Business.Client.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace QIQO.Business.Client.Contracts
{
    [ServiceContract]
    public interface ICompanyService : IServiceContract, IDisposable
    {
        [OperationContract]
        List<Company> GetCompanies(Employee emp);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        int CreateCompany(Company company);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        bool DeleteCompany(Company company);

        [OperationContract]
        Company GetCompany(int company_key);

        [OperationContract]
        string GetEmployeeRoleInCompany(Employee emp); // , Company company

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        int CompanyAddEmployee(Company company, Employee emp, string role, string comment);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        string GetCompanyNextNumber(Company company, QIQOEntityNumberType number_type);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        bool CompanyDeleteEmployee(Company company, Employee emp);



        [OperationContract]
        Task<List<Company>> GetCompaniesAsync(Employee emp);

        [OperationContract]
        Task<int> CreateCompanyAsync(Company company);

        [OperationContract]
        Task<bool> DeleteCompanyAsync(Company company);

        [OperationContract]
        Task<Company> GetCompanyAsync(int company_key);

        [OperationContract]
        Task<string> GetEmployeeRoleInCompanyAsync(Employee emp); // , Company company

        [OperationContract]
        Task<int> CompanyAddEmployeeAsync(Company company, Employee emp, string role, string comment);

        [OperationContract]
        Task<string> GetCompanyNextNumberAsync(Company company, QIQOEntityNumberType number_type);

        [OperationContract]
        Task<bool> CompanyDeleteEmployeeAsync(Company company, Employee emp);
    }
}