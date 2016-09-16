using System.Collections.Generic;

namespace QIQO.Business.Client.Entities
{
    public class Representative : Employee
    {
        public List<Account> Accounts { get; set; } = new List<Account>();
        public Representative() { }
        public Representative(QIQOPersonType RepType)
        {
            CompanyRoleType = RepType;
        }
    }
}
