using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VINES.Data;

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
        [ForeignKey("forumPostID")]
        public ForumPost post { get; set; }
        [ForeignKey("userID")]
        public User user { get; set; }
    }
}
