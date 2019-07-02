using System;

namespace QIQO.Business.Client.Entities
{
    public class ContactType
    {
        public int ContactTypeKey { get; set; }

        public string ContactTypeCategory { get; set; }
        public string ContactTypeCode { get; set; }
        public string ContactTypeName { get; set; }
        public string ContactTypeDesc { get; set; }
        public string AddedUserID { get; set; }
        public DateTime AddedDateTime { get; set; }
        public string UpdateUserID { get; set; }
        public DateTime UpdateDateTime { get; set; }

        public int TypeRowKey
        {
            get { return ContactTypeKey; }

            set { ContactTypeKey = value; }
        }
    }
}
