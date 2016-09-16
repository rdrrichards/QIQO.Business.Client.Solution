using System;
using System.Collections.Generic;

namespace QIQO.Business.Client.Entities
{
    public class OrderItem
    {
        public int OrderItemKey { get; set; }
        public int OrderKey { get; set; }
        public int OrderItemSeq { get; set; }
        public int ProductKey { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string ProductDesc { get; set; }
        public int OrderItemQuantity { get; set; }

        public Address OrderItemShipToAddress { get; set; } = new Address();
        public Address OrderItemBillToAddress { get; set; } = new Address();

        public DateTime? OrderItemShipDate { get; set; }
        public DateTime? OrderItemCompleteDate { get; set; }

        public decimal ItemPricePer { get; set; }
        public decimal OrderItemLineSum { get; set; }

        public Representative SalesRep { get; set; } = new Representative(QIQOPersonType.SalesRepresentative);
        public Representative AccountRep { get; set; } = new Representative(QIQOPersonType.AccountRepresentative);

        public QIQOOrderItemStatus OrderItemStatus { get; set; } = QIQOOrderItemStatus.Open;
        public OrderItemStatus OrderItemStatusData { get; set; } = new OrderItemStatus();

        public Product OrderItemProduct { get; set; } = new Product();
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public string AddedUserID { get; set; }
        public DateTime AddedDateTime { get; set; }
        public string UpdateUserID { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}
