using System;
using System.Collections.Generic;

namespace QIQO.Business.Client.Entities
{
    public class Product
    {
        public int ProductKey { get; set; }
        public QIQOProductType ProductType { get; set; } = QIQOProductType.Sweet9;
        public ProductType ProductTypeData { get; set; } = new ProductType();
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string ProductDesc { get; set; }
        public string ProductNameShort { get; set; }
        public string ProductNameLong { get; set; }
        public string ProductImagePath { get; set; }
        public List<EntityAttribute> ProductAttributes { get; set; } = new List<EntityAttribute>();
        public string ProductDescCombo { get; set; }
        public string AddedUserID { get; set; }
        public DateTime AddedDateTime { get; set; }
        public string UpdateUserID { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}
