using System;

namespace QIQO.Business.Client.Entities
{
    public class Contact
    {
        public int ContactKey { get; set; }
        public int EntityKey { get; set; }
        public int EntityTypeKey { get; set; }
        public int ContactTypeKey { get; set; }
        public QIQOContactType ContactType { get; set; } = QIQOContactType.CellPhone;
        public string ContactValue { get; set; }
        public int ContactDefaultFlg { get; set; }
        public int ContactActiveFlg { get; set; }
        public string AddedUserID { get; set; }
        public DateTime AddedDateTime { get; set; }
        public string UpdateUserID { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}
