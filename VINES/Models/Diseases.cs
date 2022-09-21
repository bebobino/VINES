using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.ComponentModel.DataAnnotations;

namespace VINES.Models
{
    [Table("diseases")]
    public class Diseases
    {
        [Key]
        [Required]
        public int diseaseID { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$",
        ErrorMessage = "Characters are not allowed.")]
        [Display(Name = "Disease Name")]

        public string diseaseName { get; set; }
        [Display(Name = "Notes")]
        public string notes { get; set; }
        [Required]
        public DateTime dateAdded { get; set; }
        [Required]
        public DateTime dateModified { get; set; }
    }
}
