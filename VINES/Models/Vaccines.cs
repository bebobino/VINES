using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VINES.Models
{
    [Table("vaccines")]
    public class Vaccines
    {
        [Key]
        public int vaccineID { get; set; }
        public int diseaseID { get; set; }
        public string vaccineName { get; set; }
        public string notes { get; set; }
        public DateTime dateAdded { get; set; }
        public DateTime dateModified { get; set; }
    }
}
