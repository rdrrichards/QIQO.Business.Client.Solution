
using System;

namespace QIQO.Business.Client.Entities
{
    public class PersonType
    {
        public int PersonTypeKey { get; set; }
    
        public string PersonTypeCategory { get; set; }
        public string PersonTypeCode { get; set; }
        public string PersonTypeName { get; set; }
        public string PersonTypeDesc { get; set; }
        public string AddedUserID { get; set; }
        public DateTime AddedDateTime { get; set; }
        public string UpdateUserID { get; set; }
        public DateTime UpdateDateTime { get; set; }

        public int TypeRowKey
        {
            get { return PersonTypeKey; }

            set { PersonTypeKey = value; }
        }
    }
}
