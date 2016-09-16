using System;

namespace QIQO.Business.Client.Entities
{
    public class OrderItemStatus
    {
        public int OrderItemStatusKey { get; set; }
        public string OrderItemStatusCode { get; set; }
        public string OrderItemStatusName { get; set; }
        public string OrderItemStatusDesc { get; set; }
        public string OrderItemStatusType { get; set; }
        public string AddedUserID { get; set; }
        public DateTime AddedDateTime { get; set; }
        public string UpdateUserID { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}
