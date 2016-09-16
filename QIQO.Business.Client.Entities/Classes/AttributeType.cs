using System;

namespace QIQO.Business.Client.Entities
{
    public class AttributeType
    {
        public int AttributeTypeKey { get; set; }
    
        public string AttributeTypeCategory { get; set; }
        public string AttributeTypeCode { get; set; }
        public string AttributeTypeName { get; set; }
        public string AttributeTypeDesc { get; set; }
        public QIQOAttributeDataType AttributeDataTypeKey { get; set; } = QIQOAttributeDataType.String;
        public string AttributeDefaultFormat { get; set; }
        public string AddedUserID { get; set; }
        public DateTime AddedDateTime { get; set; }
        public string UpdateUserID { get; set; }
        public DateTime UpdateDateTime { get; set; }

        public int TypeRowKey
        {
            get { return AttributeTypeKey; }

            set { AttributeTypeKey = value; }
        }
    }
}
