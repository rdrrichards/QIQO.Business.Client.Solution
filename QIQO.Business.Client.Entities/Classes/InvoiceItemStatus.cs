using System;

namespace QIQO.Business.Client.Entities
{
    public class InvoiceItemStatus
    {
        public int InvoiceItemStatusKey { get; set; }
        public string InvoiceItemStatusType { get; set; }
        public string InvoiceItemStatusCode { get; set; }
        public string InvoiceItemStatusName { get; set; }
        public string InvoiceItemStatusDesc { get; set; }
        public string AddedUserID { get; set; }
        public DateTime AddedDateTime { get; set; }
        public string UpdateUserID { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}
