
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace VINES.Models
{
    [Table("communityPostCategories")]
    public class CommunityPostCategories
    {
        [Key]
        public int communityPostCategoryID { get; set; }
        public string communityPostCategory { get; set; }
    }
}
