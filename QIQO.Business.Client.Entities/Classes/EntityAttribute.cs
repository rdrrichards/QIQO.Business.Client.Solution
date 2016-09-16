using System;

namespace QIQO.Business.Client.Entities
{
    public class EntityAttribute
    {
        public int AttributeKey { get; set; }
        public int EntityKey { get; set; }
        public QIQOEntityType EntityType { get; set; } = QIQOEntityType.Account;
        public EntityType EntityTypeData { get; set; } = new EntityType();
        public QIQOAttributeType AttributeType { get; set; } = QIQOAttributeType.AccountContact_CNCT_MAIN;
        public AttributeType AttributeTypeData { get; set; } = new AttributeType();

        public string AttributeValue { get; set; }
        public int AttributeDataTypeKey { get; set; }
        public QIQOAttributeDataType AttributeDataType { get; set; } = QIQOAttributeDataType.String;
        public string AttributeDisplayFormat { get; set; }
        public string AddedUserID { get; set; }
        public DateTime AddedDateTime { get; set; }
        public string UpdateUserID { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}
