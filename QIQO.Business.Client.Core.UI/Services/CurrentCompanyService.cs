using QIQO.Business.Client.Contracts;
using QIQO.Business.Client.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace QIQO.Business.Client.Core.UI
{
    public class CurrentCompanyService : ICurrentCompanyService
    {
        readonly IServiceFactory _service_factory;
        string _user_name;
        int _process_id;
        string _user_domain;
        string _host_name;
        private readonly bool _CompanyPromptOnLoad;
        private readonly int _DefaultCompanyKey;

        public CurrentCompanyService(IServiceFactory service_factory)
        {
            _service_factory = service_factory;
            _CompanyPromptOnLoad = Properties.Settings.Default.CompanyPromptOnLoad;
            _DefaultCompanyKey = Properties.Settings.Default.DefaultCompanyKey;
            Initialize();
        }

        private void Initialize()
        {
            Employee emp_obj = null;

            var employee_service = _service_factory.CreateClient<IEmployeeService>();

            var currentProcess = Process.GetCurrentProcess();
            _process_id = currentProcess.Id;

            _host_name = Environment.MachineName;
            _user_domain = Environment.UserDomainName;
            _user_name = Environment.UserName;

            using (employee_service)
            {
                emp_obj = employee_service.GetEmployeeByCredentials(_user_domain + @"\" + _user_name);
            }

            if (emp_obj != null)
            {
                EmployeeCompanies = emp_obj.Companies;
                CurrentEmployee = emp_obj;
                if (EmployeeCompanies.Count == 1)
                {
                    CurrentCompany = emp_obj.Companies[0];
                    IsMultiCompanyEmployee = false;
                }
                else
                {
                    IsMultiCompanyEmployee = true;
                    CurrentCompany = emp_obj.Companies.Where(co => co.CompanyKey == _DefaultCompanyKey).FirstOrDefault();
                }
            }
            else
            {
                throw new AccessViolationException("You do not have the proper permissions to use this application. See your administrator for help.");
            }
        }

        public Company CurrentCompany { get; set; }
        public List<Company> EmployeeCompanies { get; set; }
        public bool IsMultiCompanyEmployee { get; set; }
        public Employee CurrentEmployee { get; set; }
        public string CurrentEmployeeUserName { get { return _user_name; } }
        public string CurrentEmployeeDomainName { get { return _user_domain; } }
        public string CurrentEmployeeHostName { get { return _host_name; } }
        public string CurrentEmployeeDomainUserName { get { return _user_domain + @"\" + _user_name; } }

        public bool CompanyPromptOnLoad { get { return _CompanyPromptOnLoad; } }

        public int DefaultCompanyKey { get { return _DefaultCompanyKey; } }
    }
}
