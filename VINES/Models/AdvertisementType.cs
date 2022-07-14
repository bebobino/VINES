using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VINES.Models
{
    [Table("advertisementType")]
    public class AdvertisementType
    {
        [Key]
        public int advertisementID { get; set; }

        public int userID { get; set; }

        public int institutionID { get; set; }


    }
}
