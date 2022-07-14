
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace VINES.Models
{
    [Table("webPages")]
    public class WebPage
    {
        [Key]
        public int webPageID { get; set; }
        public int sourcesID { get; set; }
        public string webURL { get; set; }
        public DateTime uploadDate { get; set; }
        public string pageTitle {get; set; }
        public string summary  {get; set; }
        public string pageContents { get; set; }
        public DateTime dateAdded { get; set; }
        public int postID { get; set; }
    }
}
