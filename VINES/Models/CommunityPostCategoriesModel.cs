using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VINES.Models
{
    [Table("communityPostCategories")]
    public class CommunityPostCategoriesModel
    {
        [Key]
        public int communityPostCategoryID { get; set; }

        [Display(Name = "Category")]

        public string communityPostCategory { get; set; }
    }
}
