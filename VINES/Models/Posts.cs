using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VINES.Models
{
    [Table("posts")]
    public class Posts
    {
        [Key]
        public int postID { get; set; }
        public bool isVisible { get; set; }
    }
}
