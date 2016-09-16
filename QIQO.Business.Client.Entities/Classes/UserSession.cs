using System;

namespace QIQO.Business.Client.Entities
{
    public class UserSession
    {
        public int ProcessID { get; set; }
        public string SessionID { get; set; }
        public string UserDomain { get; set; }
        public string UserName { get; set; }
        public string HostName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int Active { get; set; }
        public int CompanyKey { get; set; }
        public string CompanyName { get; set; }
        public string AddedUserID { get; set; }
        public DateTime AddedDateTime { get; set; }
        public string UpdateUserID { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}
