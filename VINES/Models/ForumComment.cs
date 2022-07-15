using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace VINES.Models
{
    [Table("forumComments")]
    public class ForumComment
    {
        [Key]
        public int forumPostID { get; set; }

        public int postID { get; set; }
        public int forumPostCategoryID { get; set; }
        public int forumPostCategory { get; set; }
        public string forumPostTitle { get; set; }

        public int userID { get; set; }

        public string forumPostContent { get; set; }

        public DateTime dateAdded { get; set; }

        public DateTime lastModified { get; set; }
    }
}
