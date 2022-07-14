using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VINES.Models
{
    [Table("sources")]
    public class Sources
    {
        [Key]
        public int sourcesID { get; set; }
        public string sourceName { get; set; }
        public string sourcesURI { get; set; }
    }
}
