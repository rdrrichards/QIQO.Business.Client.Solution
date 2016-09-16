using System;
using System.Collections.Generic;

namespace QIQO.Business.Client.Entities
{
    public class Invoice
    {
        public int InvoiceKey { get; set; }
        public int FromEntityKey { get; set; }
        public int AccountKey { get; set; }
        public Account Account { get; set; } = new Account();
        public int AccountContactKey { get; set; }
        public string InvoiceNumber { get; set; }
        public PersonBase InvoiceAccountContact { get; set; } = new PersonBase();
        public int InvoiceItemCount { get; set; }
        public decimal InvoiceValueSum { get; set; }

        public DateTime OrderEntryDate { get; set; }
        public DateTime InvoiceEntryDate { get; set; }
        public DateTime InvoiceStatusDate { get; set; }
        public DateTime? OrderShipDate { get; set; }
        public DateTime? InvoiceCompleteDate { get; set; }

        public int SalesRepKey { get; set; }
        public Representative SalesRep { get; set; } = new Representative(QIQOPersonType.SalesRepresentative);
        public int AccountRepKey { get; set; }
        public Representative AccountRep { get; set; } = new Representative(QIQOPersonType.AccountRepresentative);

        public QIQOInvoiceStatus InvoiceStatus { get; set; } = QIQOInvoiceStatus.New;
        public InvoiceStatus InvoiceStatusData { get; set; } = new InvoiceStatus();

        public List<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public string AddedUserID { get; set; }
        public DateTime AddedDateTime { get; set; }
        public string UpdateUserID { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}
