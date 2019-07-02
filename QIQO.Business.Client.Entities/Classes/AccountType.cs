
using System;

namespace QIQO.Business.Client.Entities
{
    public class AccountType
    {
        public int AccountTypeKey { get; set; }

        //public string AccountTypeCategory { get; set; }
        public string AccountTypeCode { get; set; }
        public string AccountTypeName { get; set; }
        public string AccountTypeDesc { get; set; }
        public string AddedUserID { get; set; }
        public DateTime AddedDateTime { get; set; }
        public string UpdateUserID { get; set; }
        public DateTime UpdateDateTime { get; set; }

        public int TypeRowKey
        {
            get { return AccountTypeKey; }

            set { AccountTypeKey = value; }
        }
    }
}
