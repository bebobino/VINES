using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VINES.Models
{
    [Table("advertisement")]
    public class Advertisement
    {
        [Key]
        public int advertisementID { get; set; }

        public int advertiserID { get; set; }

        public string imgsrc { get; set; }

        public DateTime lastModified { get; set; }

        public DateTime dateAdded { get; set; }

        public string textContent { get; set; }

        public string url { get; set; }

        public int advertiseTypeID { get; set; }
        public int advertisementTitle { get; set; }
        public int clicks { get; set; }
        public DateTime endDate { get; set; }
        [ForeignKey("postID")]
        public Posts posts { get; set; }
        [ForeignKey("advertiserID")]
        public Advertisers advertiser { get; set; }
    }
}
