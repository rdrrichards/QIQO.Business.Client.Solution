using QIQO.Business.Client.Entities;
using System.Collections.Generic;
using System.Linq;
using CommonServiceLocator;

namespace QIQO.Business.Client.Core.UI
{
    public class UserSettingsViewModel
    {
        private readonly ICurrentCompanyService _company_service;
        public UserSettingsViewModel()
        {
            _company_service = ServiceLocator.Current.GetInstance<ICurrentCompanyService>(); ;

        }

        public bool FormEnabled => _company_service.IsMultiCompanyEmployee;
        public bool CompanyPromptOnLoad => _company_service.CompanyPromptOnLoad;

        public List<Company> CompanyList => _company_service.EmployeeCompanies;
        public object DefaultCompany => _company_service.EmployeeCompanies.Where(co => co.CompanyKey == _company_service.DefaultCompanyKey).FirstOrDefault();
    }
}
