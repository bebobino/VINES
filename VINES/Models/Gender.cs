
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VINES.Models
{
    [Table("Gender")]
    public class Gender
    {
        [Key]
        public int genderID { get; set; }
        public string gender { get; set; }
    }
}
