using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VINES.Models
{
    [Table("vaccines")]
    public class Vaccines
    {
        [Key]
        [Required]
        public int vaccineID { get; set; }
        [Required]
        public int diseaseID { get; set; }
        [Required]
        [Display(Name = "Vaccine Name")]
        public string vaccineName { get; set; }
        [Display(Name = "Notes")]
        public string notes { get; set; }
        [Required]
        public DateTime dateAdded { get; set; }
        [Required]
        public DateTime? dateModified { get; set; }
        [ForeignKey("diseaseID")]
        public Diseases disease { get; set; }
    }
}
