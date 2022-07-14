using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VINES.Models
{
    [Table("forumPost")]
    public class ForumPost
    {
        [Key]
        public int forumPostID { get; set; }
        public int forumCategoryID { get; set; }
        public int userID { get; set; }
        public string forumContent { get; set; }
        public DateTime dateAdded { get; set; }
        public DateTime lastModified { get; set; }
        public int postID { get; set; }
    }
}
