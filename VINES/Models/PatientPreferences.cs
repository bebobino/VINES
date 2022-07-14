using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VINES.Models
{
    [Table("patientPreferences")]
    public class PatientPreferences
    {
        [Key]
        public int patientPreferenceID { get; set; }
        public int diseaseID { get; set; }
        public int patientID { get; set; }
        public decimal budget { get; set; }
        public DateTime dateAdded { get; set; }
        public DateTime lastModified { get; set; }

    }
}
