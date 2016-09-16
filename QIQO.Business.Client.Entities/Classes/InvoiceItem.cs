using System;

namespace QIQO.Business.Client.Entities
{
    public class InvoiceItem
    {
        public int InvoiceItemKey { get; set; }
        public int FromEntityKey { get; set; }
        public int InvoiceKey { get; set; }
        public int InvoiceItemSeq { get; set; }
        public int ProductKey { get; set; }
        public string ProductName { get; set; }
        public string ProductDesc { get; set; }
        public int InvoiceItemQuantity { get; set; }

        public Address OrderItemShipToAddress { get; set; } = new Address();
        public Address OrderItemBillToAddress { get; set; } = new Address();

        //public DateTime InvoiceEntry
        public DateTime? OrderItemShipDate { get; set; }
        public DateTime? InvoiceItemCompleteDate { get; set; }

        public decimal ItemPricePer { get; set; }
        public decimal InvoiceItemLineSum { get; set; }

        public Representative SalesRep { get; set; } = new Representative(QIQOPersonType.SalesRepresentative);
        public Representative AccountRep { get; set; } = new Representative(QIQOPersonType.AccountRepresentative);

        public QIQOInvoiceItemStatus InvoiceItemStatus { get; set; } = QIQOInvoiceItemStatus.New;
        public InvoiceItemStatus InvoiceItemStatusData { get; set; } = new InvoiceItemStatus();

        public Product InvoiceItemProduct { get; set; } = new Product();
        public string AddedUserID { get; set; }
        public DateTime AddedDateTime { get; set; }
        public string UpdateUserID { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}
