using QIQO.Business.Client.Core;
using QIQO.Business.Client.Entities;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace QIQO.Business.Client.Contracts
{
    [ServiceContract]
    public interface IEmployeeService : IServiceContract, IDisposable
    {
        [OperationContract]
        List<Employee> GetEmployees(Company company);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        int CreateEmployee(Employee employee);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        bool DeleteEmployee(Employee employee);

        [OperationContract]
        Employee GetEmployee(int entity_person_key);

        [OperationContract]
        Employee GetEmployeeByCredentials(string user_name);

        [OperationContract]
        List<Representative> GetAccountRepsByCompany(int company_key);

        [OperationContract]
        List<Representative> GetSalesRepsByCompany(int company_key);



        [OperationContract]
        Task<List<Employee>> GetEmployeesAsync(Company company);

        [OperationContract]
        Task<int> CreateEmployeeAsync(Employee employee);

        [OperationContract]
        Task<bool> DeleteEmployeeAsync(Employee employee);

        [OperationContract]
        Task<Employee> GetEmployeeAsync(int entity_person_key);

        [OperationContract]
        Task<Employee> GetEmployeeByCredentialsAsync(string user_name);

        [OperationContract]
        Task<List<Representative>> GetAccountRepsByCompanyAsync(int company_key);

        [OperationContract]
        Task<List<Representative>> GetSalesRepsByCompanyAsync(int company_key);
    }
}