using System;

namespace QIQO.Business.Client.Entities
{
    public class ProductType
    {
        public int ProductTypeKey { get; set; }
    
        public string ProductTypeCategory { get; set; }
        public string ProductTypeCode { get; set; }
        public string ProductTypeName { get; set; }
        public string ProductTypeDesc { get; set; }
        public string AddedUserID { get; set; }
        public DateTime AddedDateTime { get; set; }
        public string UpdateUserID { get; set; }
        public DateTime UpdateDateTime { get; set; }

        public int TypeRowKey
        {
            get { return ProductTypeKey; }

            set { ProductTypeKey = value; }
        }
    }
}
