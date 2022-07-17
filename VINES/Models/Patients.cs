using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VINES.Data;

namespace VINES.Models
{
    [Table("patients")]
    public class Patients
    {
        [Key]
        public int patientID { get; set; }
        public int userID { get; set; }
        public bool showAds { get; set; }
        public bool isSubscribed { get; set; }
        public DateTime subStart { get; set; }
        public DateTime subEnd { get; set; }
        [ForeignKey("userID")]
        public User user { get; set; }
    }
}
