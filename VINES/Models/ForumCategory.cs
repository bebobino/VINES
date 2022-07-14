
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VINES.Models
{
    [Table("forumCategory")]
    public class ForumCategory
    {
        [Key]
        public int forumCategoryID { get; set; }
        public string forumCategory { get; set; }
    }
}
