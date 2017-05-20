using System;

namespace QIQO.Business.Module.General.Models
{
    public class BusinessItem
    {
        public string ItemId { get; set; }
        public string ItemCode { get; set; }
        public string ItemStatus { get; set; }
        public string ItemName { get; set; }

        public double Total { get; set; }
        public int Quantity { get; set; }

        public string ItemType { get; set; }
        public DateTime ItemEntryDate { get; set; }
        public DateTime ItemStatusDate { get; set; }

        public string ItemPhone { get; set; }
        public string ItemEmail { get; set; }
        public string ItemOwner { get; set; }
        public string ItemMenuText => $"[ {ItemCode} ] {ItemName} {ItemEntryDate.ToShortDateString()}";
        public object BusinessObject { get; set; }
        public string ItemStatusOpenList { get; set; }
    }
}
