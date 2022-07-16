using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace VINES.Models
{
    [Table("CommunityPost")]
    public class CommunityPost
    {
        [Key]
        public int communityPostID { get; set; }

        public string communityPostTitle { get; set; }

        public int communityPostCategoryID { get; set; }

        public string communityPostContent { get; set; }

        public DateTime dateAdded { get; set; }

        public DateTime lastModified { get; set; }
        public int postID { get; set; }
        [ForeignKey("postID")]
        public Posts posts { get; set; }
        [ForeignKey("communityPostCategoryID")]
        public CommunityPostCategoriesModel communityPostCategory { get; set; }
    }
}
