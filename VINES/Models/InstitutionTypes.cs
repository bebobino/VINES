using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VINES.Models
{
    [Table("institutionType")]
    public class InstitutionTypes
    {
        [Key]
        public int institutionTypeID { get;set; }
        public string institutionType { get;set; }
    }
}
