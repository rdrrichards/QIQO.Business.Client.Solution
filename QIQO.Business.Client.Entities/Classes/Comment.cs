using System;

namespace QIQO.Business.Client.Entities
{
    public class Comment
    {
        public int CommentKey { get; set; }
        public int EntityKey { get; set; }
        public int EntityTypeKey { get; set; }
        public QIQOCommentType CommentType { get; set; } = QIQOCommentType.Public;
        public string CommentValue { get; set; }
        public string AddedUserID { get; set; }
        public DateTime AddedDateTime { get; set; }
        public string UpdateUserID { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}