using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VINES.Models
{
    [Table("vaccinePreference")]
    public class VaccinePreference
    {
        [Key]
        public int vaccinePreferenceID { get; set; }
        public int patientPreferenceID { get; set; }
        public int vaccineID { get; set; }
        public DateTime dateAdded { get; set; }
        public DateTime lastModified { get; set; }

    }
}
