using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VINES.Models
{
    [Table("bookmarks")]
    public class Bookmarks
    {
        [Key]
        public int bookmarkID { get; set; }
        public int patientID { get; set; }
        public int postID { get; set; }
        [ForeignKey("patientID")]
        public Patients patient { get; set; }
        [ForeignKey("postID")]
        public Posts post { get; set; }
    }


}
