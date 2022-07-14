using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VINES.Models
{
    [Table("forumComment")]
    public class ForumComment
    {
        [Key]
        public int forumCommentID { get; set; }
        public int forumPostID { get; set; }
        public int userID { get; set; }
        public string comment { get; set; }
        public DateTime dateAdded { get; set; }
        public DateTime lastModified { get; set; }
    }
}
