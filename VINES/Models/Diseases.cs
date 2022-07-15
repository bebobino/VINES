using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.ComponentModel.DataAnnotations;

namespace VINES.Models
{
    [Table("diseases")]
    public class diseases
    {
        [Key]
        public int diseaseID { get; set; }
        public string diseaseName { get; set; }
        public string notes { get; set; }
        public DateTime dateAdded { get; set; }
        public DateTime dateModified { get; set; }
    }
}
