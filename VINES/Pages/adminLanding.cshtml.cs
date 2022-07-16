using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using VINES.Data;
using VINES.Models;

namespace VINES.Pages
{
    [Authorize]
    public class adminLandingModel : PageModel
    {

        public List<User> Users { get; set; }

        private DatabaseContext db;

        public adminLandingModel(DatabaseContext _db)
        {
            db = _db;
        }

        public void OnGet()
        {
          
        }
    }
}
