using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VINES.Models
{
    [Table("adReceipts")]
    public class AdReceipts
    {
        [Key]
        public int adReceiptID { get; set; }
        public int advertisementID { get; set; }
        public int advertiserID { get; set; }
        public decimal price { get; set; }
        public string paymentID { get; set; }
        public DateTime dateCreated { get; set; }



        [ForeignKey("advertiserID")]
        public Advertisers advertiser { get; set; }
        [ForeignKey("advertisementID")]
        public Advertisement advertisement { get; set; }
    }
}
