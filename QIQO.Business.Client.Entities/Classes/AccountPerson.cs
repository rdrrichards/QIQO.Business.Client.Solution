using System;

namespace QIQO.Business.Client.Entities
{
    public class AccountPerson : PersonBase
    {
        //public Account Account { get; set; } = new Account();
        public string RoleInCompany { get; set; }
        public int EntityPersonKey { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Comment { get; set; }
        public QIQOPersonType CompanyRoleType { get; set; } = QIQOPersonType.AccountEmployee;

        public AccountPerson() { }
        public AccountPerson(QIQOPersonType Role)
        {
            PersonTypeData = new PersonType() { PersonTypeKey = (int)Role };
        }
    }
}
