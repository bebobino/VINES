using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VINES.Models
{
    public class PatientModel
    {
        [Key]
        [Required]
        public int patientID { get; set; }
        [Required]
        public int userID { get; set; }
        [Required]
        public bool chronicIllness { get; set; }
        [Required]
        public bool treatment { get; set; }
        [Required]
        public bool vaccineAllergies { get; set; }
        [Required]
        public bool illnessVaccine { get; set; }
    }
}
