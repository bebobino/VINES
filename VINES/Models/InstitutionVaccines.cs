using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VINES.Models
{
    [Table("institutionVaccines")]
    public class InstitutionVaccines
    {
        [Key]
        public int institutionVaccineID { get; set; }
        [Required]
        public int diseaseID { get; set; }
        [Required]
        public int institutionID { get; set; }
        [Required]
        public int vaccineID { get; set;}
        [Required]
        [Display(Name = "Price")]
        public decimal price { get; set;}
        [Display(Name = "Notes")]
        public string notes { get; set; }
        [Required]
        public DateTime dateAdded { get; set; }
        [Required]
        public DateTime lastModified { get; set; }

        [ForeignKey("diseaseID")]
        public Diseases disease { get; set; }
        [ForeignKey("vaccineID")]
        public Vaccines vaccine { get; set; }
        [ForeignKey("institutionID")]
        public Institutions institution { get; set; }
    }
}
