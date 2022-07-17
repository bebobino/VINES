using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VINES.Data;

namespace VINES.Models
{
    [Table("advertisers")]
    public class Advertisers
    {
        [Key]
        public int advertiserID { get; set; }

        public int userID { get; set; }

        public int institutionID { get; set; }
        [ForeignKey("userID")]
        public User user{ get; set; }
        [ForeignKey("institutionID")]
        public Institutions institution { get; set; }



    }
}
