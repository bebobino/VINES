using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VINES.Models
{
    [Table("recommendedInstitutions")]
    public class RecommendedInstitutions
    {
        [Key]
        public int recommendedInstitutionID { get; set; }
        public int institutionID { get; set; }
        public int userPreferenceID { get; set; }
        public DateTime dateAdded { get; set; }
    }
}
