using System;
using System.Collections.Generic;

namespace QIQO.Business.Client.Entities
{
    public class Account
    {
        public int AccountKey { get; set; }
        public int CompanyKey { get; set; }
        public QIQOAccountType AccountType { get; set; } = QIQOAccountType.Individual;
        //public AccountType AccountTypeData { get; set; }
        public string AccountCode { get; set; }
        public string AccountName { get; set; }
        public string AccountDesc { get; set; }
        public string AccountDBA { get; set; }
        public DateTime AccountStartDate { get; set; } = DateTime.Today;
        public DateTime AccountEndDate { get; set; } = DateTime.Today.AddYears(50);

        //public QIQOEntityType EntityType { get; private set; }
        public List<Address> Addresses { get; set; } = new List<Address>();
        public List<EntityAttribute> AccountAttributes { get; set; } = new List<EntityAttribute>();
        public List<FeeSchedule> FeeSchedules { get; set; } = new List<FeeSchedule>();
        public List<AccountPerson> Employees { get; set; } = new List<AccountPerson>();
        public List<Contact> Contacts { get; set; } = new List<Contact>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public string AddedUserID { get; set; }
        public DateTime AddedDateTime { get; set; }
        public string UpdateUserID { get; set; }
        public DateTime UpdateDateTime { get; set; }

        public int OwnerAccountKey
        {
            get { return AccountKey; }
        }
    }
}
