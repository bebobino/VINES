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
        public string IPAddress { get; set; }
        public int violations { get; set; }
        public bool isBlocked { get; set; }
        public DateTime dateAdded { get; set; }
    }
}
