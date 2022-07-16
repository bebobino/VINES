using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VINES.Models
{
    [Table("communityPostCategories")]
    public class CommunityPostCategoriesModel
    {
        [Key]
        public int communityPostCategoryID { get; set; }

        public string communityPostCategory { get; set; }
    }
}
