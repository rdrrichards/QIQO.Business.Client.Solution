using QIQO.Business.Client.Core.UI;
using QIQO.Business.Client.Entities;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace QIQO.Business.Client.UIX
{
    public class CompanyChooserViewModel : ViewModelBase
    {
        private readonly ObservableCollection<Company> _empCompanies;

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
