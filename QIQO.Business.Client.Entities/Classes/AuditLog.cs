using System;

namespace QIQO.Business.Client.Entities
{
    public class AuditLog
    {
        public long AuditLogKey { get; set; }
        public string AuditAction { get; set; }
        public string AuditBusinessObject { get; set; }
        public DateTime AuditDateTime { get; set; }
        public string AuditUserID { get; set; }
        public string AuditApplicationName { get; set; }
        public string AuditHostName { get; set; }
        public string AuditComment { get; set; }
        public string AuditOldDataXML { get; set; }
        public string AuditNewDataXML { get; set; }
        public string AddedUserID { get; set; }
        public DateTime AddedDateTime { get; set; }
        public string UpdateUserID { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}
