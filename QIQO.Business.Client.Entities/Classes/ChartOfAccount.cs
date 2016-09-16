using System;

namespace QIQO.Business.Client.Entities
{
    public class ChartOfAccount
    {
        public int ChartOfAccountKey { get; set; }
        public string AccountNo { get; set; }
        public string AccountType { get; set; }
        public string AccountName { get; set; }
        public string BalanceType { get; set; }
        public string BankAccountFlag { get; set; }
        public int CompanyKey { get; set; }
        public string AddedUserID { get; set; }
        public DateTime AddedDateTime { get; set; }
        public string UpdateUserID { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}
