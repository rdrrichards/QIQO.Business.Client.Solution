using System;
using System.Collections.Generic;

namespace QIQO.Business.Client.Entities
{
    public class Company
    {
        public int CompanyKey { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public string CompanyDesc { get; set; }
        public string AddedUserID { get; set; }
        public DateTime AddedDateTime { get; set; }
        public string UpdateUserID { get; set; }
        public DateTime UpdateDateTime { get; set; }

        public List<Employee> Employees { get; set; } = new List<Employee>();
        public List<ChartOfAccount> GLAccounts { get; set; } = new List<ChartOfAccount>();
        public List<Ledger> Ledgers { get; set; } = new List<Ledger>();
        public List<EntityAttribute> CompanyAttributes { get; set; } = new List<EntityAttribute>();
        public List<Address> CompanyAddresses { get; set; } = new List<Address>();
    }
}
