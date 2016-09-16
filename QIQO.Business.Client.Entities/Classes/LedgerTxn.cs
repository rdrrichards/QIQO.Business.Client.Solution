using System;

namespace QIQO.Business.Client.Entities
{
    public class LedgerTxn
    {
        public int LedgerTxnKey { get; set; }
        public int LedgerKey { get; set; }
        public string Comment { get; set; }
        public string AccountFrom { get; set; }
        public string AccountTo { get; set; }

        public DateTime EntryDate { get; set; }
        public DateTime PostDate { get; set; }

        public decimal Credit { get; set; }
        public decimal Debit { get; set; }

        public int EntityKey { get; set; }
        public QIQOEntityType EntityType { get; set; } = QIQOEntityType.Account;

        public int LedgerTxnNum { get; set; }
        public string AddedUserID { get; set; }
        public DateTime AddedDateTime { get; set; }
        public string UpdateUserID { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}
