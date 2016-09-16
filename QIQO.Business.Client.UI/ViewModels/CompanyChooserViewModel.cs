using QIQO.Business.Client.Core.UI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using QIQO.Business.Client.Entities;

namespace QIQO.Business.Client.UI
{
    public class CompanyChooserViewModel : ViewModelBase
    {
        private ObservableCollection<Company> _empCompanies;

        public CompanyChooserViewModel(List<Company> companies)
        {
            _empCompanies = new ObservableCollection<Company>(companies);
        }

        public ObservableCollection<Company> EmployeeCompanies
        {
            get { return _empCompanies; }
        }

    }
}
