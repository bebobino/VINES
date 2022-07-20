using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VINES.Models
{
    [Table("advertisementType")]
    public class AdvertisementType
    {
        [Key]
        public int advertisementTypeID { get; set; }

        public int advertisementType { get; set; }

        public decimal price { get; set; }
        public int clickLimit { get; set; }
        public int Duration { get; set; }


    }
}
