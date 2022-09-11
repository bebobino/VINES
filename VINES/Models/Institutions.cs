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
        [Required]
        public int institutionTypeID { get; set; }
        [Required]
        [Display(Name = "Institution Name")]
        public string institutionName { get; set; }
        [Required]
        [Display(Name = "Longitude")]
        public decimal Long { get; set;}
        [Required]
        [Display(Name = "Latitude")]
        public decimal lat { get; set;}
        [Display(Name = "Notes")]
        public string notes { get; set; }
        [Required]
        public DateTime dateAdded { get; set; }
        [Required]
        public DateTime lastModified { get; set; }
        [ForeignKey("institutionTypeID")]
        public InstitutionTypes institutionType { get; set; }
    }
}
