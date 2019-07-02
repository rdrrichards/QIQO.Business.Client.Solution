using System;

namespace QIQO.Business.Client.Entities
{
    public class EntityType
    {
        public int EntityTypeKey { get; set; }

        //public string EntityTypeCategory { get; set; }
        public string EntityTypeCode { get; set; }
        public string EntityTypeName { get; set; }
        //public string EntityTypeDesc { get; set; }
        public string AddedUserID { get; set; }
        public DateTime AddedDateTime { get; set; }
        public string UpdateUserID { get; set; }
        public DateTime UpdateDateTime { get; set; }

        public int TypeRowKey
        {
            get { return EntityTypeKey; }

            set { EntityTypeKey = value; }
        }
    }
}
