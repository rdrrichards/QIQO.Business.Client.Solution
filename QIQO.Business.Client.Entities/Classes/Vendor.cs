using System;
using System.Collections.Generic;

namespace QIQO.Business.Client.Entities
{
    public class Vendor
    {
        public int VendorKey { get; set; }
        public string VendorCode { get; set; }
        public string VendorName { get; set; }
        public string VendorDesc { get; set; }
        public string AddedUserID { get; set; }
        public DateTime AddedDateTime { get; set; }
        public string UpdateUserID { get; set; }
        public DateTime UpdateDateTime { get; set; }

        public List<VendorRepresentative> VendorRepresentatives { get; set; } = new List<VendorRepresentative>();
    }
}
