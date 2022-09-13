using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VINES.Models
{
    [Table("IPAddresses")]
    public class IPAddresses
    {
        [Key]
        public int IPAddressID { get; set; }
        [Display(Name = "IP Address")]
        public string IPAddress { get; set; }
        [Display(Name = "Violations")]
        public int violations { get; set; }
        [Display(Name = "Status")]
        public bool isBlocked { get; set; }
        public DateTime dateAdded { get; set; }
    }
}
