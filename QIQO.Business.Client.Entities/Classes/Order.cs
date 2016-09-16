using System;
using System.Collections.Generic;

namespace QIQO.Business.Client.Entities
{
    public class Order
    {
        public int OrderKey { get; set; }
        public int AccountKey { get; set; }
        public Account Account { get; set; } = new Account();
        public int AccountContactKey { get; set; }
        public string OrderNumber { get; set; }
        public PersonBase OrderAccountContact { get; set; } = new PersonBase();
        public int OrderItemCount { get; set; }
        public decimal OrderValueSum { get; set; }

        public DateTime OrderEntryDate { get; set; }
        public DateTime OrderStatusDate { get; set; }
        public DateTime? OrderShipDate { get; set; }
        public DateTime? OrderCompleteDate { get; set; }
        public DateTime? DeliverByDate { get; set; }

        public int SalesRepKey { get; set; }
        public Representative SalesRep { get; set; } = new Representative(QIQOPersonType.SalesRepresentative);
        public int AccountRepKey { get; set; }
        public Representative AccountRep { get; set; } = new Representative(QIQOPersonType.AccountRepresentative);

        public QIQOOrderStatus OrderStatus { get; set; } = QIQOOrderStatus.Open;
        public OrderStatus OrderStatusData { get; set; } = new OrderStatus();

        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public string AddedUserID { get; set; }
        public DateTime AddedDateTime { get; set; }
        public string UpdateUserID { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}
