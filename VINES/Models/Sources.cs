using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VINES.Models
{
    [Table("sources")]
    public class Sources
    {
        [Key]
        public int sourcesID { get; set; }
        [Display(Name = "Name")]
        public string sourceName { get; set; }
        [Display(Name = "URI")]
        public string sourcesURI { get; set; }
    }
}
