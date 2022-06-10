using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace VINES.Areas.Identity.Data
{
    public class VINESUser : IdentityUser
    {
        [PersonalData]
        public string ? Name { get; set; }
        [PersonalData]
        public DateTime DOB { get; set; }
        [PersonalData]
        public string Gender { get; set; }
        
    }
}
