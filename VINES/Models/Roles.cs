using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VINES.Models
{
    [Table("roles")]
    public class Roles
    {
        [Key]
        public int roleID { get; set; }
        public string role { get; set; }
    }
}
