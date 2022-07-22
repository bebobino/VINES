﻿using System;
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

        public int advertisementTypeID { get; set; }
        public string advertisementTitle { get; set; }
        public int clicks { get; set; }
        public DateTime endDate { get; set; }


        [ForeignKey("advertiserID")]
        public Advertisers advertiser { get; set; }
        [ForeignKey("advertisementTypeID")]
        public AdvertisementType advertisementType { get; set; }
    }
}
