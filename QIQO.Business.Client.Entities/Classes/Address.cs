using System;

namespace QIQO.Business.Client.Entities
{
    public class Address
    {
        public int AddressKey { get; set; }
        public QIQOAddressType AddressType { get; set; } = QIQOAddressType.Billing;
        public AddressType AddressTypeData { get; set; } = new AddressType();
        public int EntityKey { get; set; }
        public QIQOEntityType EntityType { get; set; } = QIQOEntityType.Account;
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string AddressLine4 { get; set; }
        public string AddressCity { get; set; }
        public string AddressState { get; set; }
        public string AddressCounty { get; set; }
        public string AddressCountry { get; set; }
        public string AddressPostalCode { get; set; }
        public string AddressNotes { get; set; }
        public bool AddressDefaultFlag { get; set; }
        public bool AddressActiveFlag { get; set; }
        public string AddedUserID { get; set; }
        public DateTime AddedDateTime { get; set; }
        public string UpdateUserID { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}
