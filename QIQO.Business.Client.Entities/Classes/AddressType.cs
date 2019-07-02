using System;

namespace QIQO.Business.Client.Entities
{
    public class AddressType
    {
        public int AddressTypeKey { get; set; }

        //public string AddressCategory { get; set; }
        public string AddressTypeCode { get; set; }
        public string AddressTypeName { get; set; }
        public string AddressTypeDesc { get; set; }
        public string AddedUserID { get; set; }
        public DateTime AddedDateTime { get; set; }
        public string UpdateUserID { get; set; }
        public DateTime UpdateDateTime { get; set; }

        public int TypeRowKey
        {
            get { return AddressTypeKey; }

            set { AddressTypeKey = value; }
        }
    }
}
