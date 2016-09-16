using QIQO.Business.Client.Entities;
using System.Collections.Generic;

namespace QIQO.Business.Client.Core.UI
{
    public interface ICurrentCompanyService
    {
        Company CurrentCompany { get; set; }
        List<Company> EmployeeCompanies { get; set; }
        bool IsMultiCompanyEmployee { get; set; }
        bool CompanyPromptOnLoad { get; }
        Employee CurrentEmployee { get; set; }
        string CurrentEmployeeUserName { get; }
        string CurrentEmployeeDomainName { get; }
        string CurrentEmployeeHostName { get; }
        string CurrentEmployeeDomainUserName { get; }
        int DefaultCompanyKey { get; }
    }
}
