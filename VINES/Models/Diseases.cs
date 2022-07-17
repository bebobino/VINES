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
        public string diseaseName { get; set; }
        [Required]
        public string notes { get; set; }
        [Required]
        public DateTime dateAdded { get; set; }
        [Required]
        public DateTime lastModified { get; set; }
    }
}
