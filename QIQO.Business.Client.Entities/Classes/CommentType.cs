
using System;

namespace QIQO.Business.Client.Entities
{
    public class CommentType
    {
        public int CommentTypeKey { get; set; }

        public string CommentTypeCategory { get; set; }
        public string CommentTypeCode { get; set; }
        public string CommentTypeName { get; set; }
        public string CommentTypeDesc { get; set; }
        public string AddedUserID { get; set; }
        public DateTime AddedDateTime { get; set; }
        public string UpdateUserID { get; set; }
        public DateTime UpdateDateTime { get; set; }

        public int TypeRowKey
        {
            get { return CommentTypeKey; }

            set { CommentTypeKey = value; }
        }
    }

}
