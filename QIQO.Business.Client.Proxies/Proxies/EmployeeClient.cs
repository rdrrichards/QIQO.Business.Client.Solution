using Microsoft.Practices.Unity;
using QIQO.Business.Client.Contracts;
using QIQO.Business.Client.Entities;
using QIQO.Common.Core.Logging;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Windows;

namespace QIQO.Business.Client.Proxies
{
    public class EmployeeClient : IEmployeeService
    {
        private IEmployeeService channel = null;

        [InjectionConstructor]
        public EmployeeClient() : this("NetTcpBinding_IEmployeeService") { }

        public EmployeeClient(string endpoint)
        {
            channel = new ChannelFactory<IEmployeeService>(endpoint).CreateChannel();
        }

        public int CreateEmployee(Employee employee)
        {
            return channel.CreateEmployee(employee);
        }

        public Task<int> CreateEmployeeAsync(Employee employee)
        {
            return channel.CreateEmployeeAsync(employee);
        }

        public bool DeleteEmployee(Employee employee)
        {
            return channel.DeleteEmployee(employee);
        }

        public Task<bool> DeleteEmployeeAsync(Employee employee)
        {
            return channel.DeleteEmployeeAsync(employee);
        }

        public List<Representative> GetAccountRepsByCompany(int company_key)
        {
            return channel.GetAccountRepsByCompany(company_key);
        }

        public Task<List<Representative>> GetAccountRepsByCompanyAsync(int company_key)
        {
            return channel.GetAccountRepsByCompanyAsync(company_key);
        }

        public Employee GetEmployee(int entity_person_key)
        {
            return channel.GetEmployee(entity_person_key);
        }

        public Task<Employee> GetEmployeeAsync(int entity_person_key)
        {
            return channel.GetEmployeeAsync(entity_person_key);
        }

        public Employee GetEmployeeByCredentials(string user_name)
        {
            try
            {
                return channel.GetEmployeeByCredentials(user_name);
            }
            catch (FaultException ex)
            {
                Log.Error(ex, ex.Message);
                MessageBox.Show(ex.Message, "WCF Fault Exception!", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(1);
            }
            catch (CommunicationException ex)
            {
                Log.Error(ex, ex.Message);
                MessageBox.Show("A Communication Exception has occured! This is more than likely due to the fact that the service isn't running, or a permissions setting issue.\n\nExiting application.",
                    "Communication Exception!", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(1);
            }
            catch(Exception ex)
            {
                Log.Error(ex, ex.Message);
                MessageBox.Show("A Unknown Exception has occured! See the application log (log.txt) for more information.\n\nExiting application.",
                    "Unknown Exception!", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(1);
            }
            return null;
        }

        public Task<Employee> GetEmployeeByCredentialsAsync(string user_name)
        {
            return channel.GetEmployeeByCredentialsAsync(user_name);
        }

        public List<Employee> GetEmployees(Company company)
        {
            return channel.GetEmployees(company);
        }

        public Task<List<Employee>> GetEmployeesAsync(Company company)
        {
            return channel.GetEmployeesAsync(company);
        }

        public List<Representative> GetSalesRepsByCompany(int company_key)
        {
            return channel.GetSalesRepsByCompany(company_key);
        }

        public Task<List<Representative>> GetSalesRepsByCompanyAsync(int company_key)
        {
            return channel.GetSalesRepsByCompanyAsync(company_key);
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