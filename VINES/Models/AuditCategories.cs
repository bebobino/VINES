using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VINES.Models
{
    [Table("auditCategories")]
    public class AuditCategories
    {
        [Key]
        public int auditCategoryID { get; set; }
        public string auditCategory { get; set; }
    }
}
