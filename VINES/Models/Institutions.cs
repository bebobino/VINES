using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VINES.Models
{
    [Table("institutions")]
    public class Institutions
    {
        [Key]
        public int institutionID { get; set; }
        public int institutionType { get; set; }
        public string institutionName { get; set; }
        public decimal longitude { get; set;}
        public decimal latitude { get; set;}
        public string notes { get; set; }
        public DateTime dateAdded { get; set; }
        public DateTime lastModified { get; set; }
    }
}
