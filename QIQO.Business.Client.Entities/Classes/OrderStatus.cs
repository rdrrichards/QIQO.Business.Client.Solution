using System;

namespace QIQO.Business.Client.Entities
{
    public class OrderStatus
    {
        public int OrderStatusKey { get; set; }
        public string OrderStatusCode { get; set; }
        public string OrderStatusName { get; set; }
        public string OrderStatusDesc { get; set; }
        public string OrderStatusType { get; set; }
        public string AddedUserID { get; set; }
        public DateTime AddedDateTime { get; set; }
        public string UpdateUserID { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}
