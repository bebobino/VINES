
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace VINES.Models
{
    [Table("posts")]
    public class Post
    {
        [Key]
        public int postID { get; set; }
        public Boolean isVisible { get; set; }
    }
}
