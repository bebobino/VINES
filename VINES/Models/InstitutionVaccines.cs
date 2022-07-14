using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VINES.Models
{
    [Table("institutionsVaccines")]
    public class InstitutionVaccines
    {
        [Key]
        public int institutionVaccineID { get; set; }
        public int diseaseID { get; set; }
        public int institutionID { get; set; }
        public int vaccineID { get; set;}
        public decimal price { get; set;}
        public string notes { get; set; }
        public DateTime dateAdded { get; set; }
        public DateTime lastModified { get; set; }
    }
}
