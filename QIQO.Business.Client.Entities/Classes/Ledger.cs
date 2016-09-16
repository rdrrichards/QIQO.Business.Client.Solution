using System;
using System.Collections.Generic;

namespace QIQO.Business.Client.Entities
{
    public class Ledger
    {
        public int LedgerKey { get; set; }
        public int CompanyKey { get; set; }
        public string LedgeCode { get; set; }
        public string LedgeName { get; set; }
        public string LedgeDesc { get; set; }
        public string AddedUserID { get; set; }
        public DateTime AddedDateTime { get; set; }
        public string UpdateUserID { get; set; }
        public DateTime UpdateDateTime { get; set; }

        public List<LedgerTxn> LedgerTxns { get; set; } = new List<LedgerTxn>();
    }
}
