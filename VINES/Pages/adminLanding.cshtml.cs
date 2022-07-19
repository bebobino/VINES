using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using VINES.Data;
using VINES.Models;
namespace VINES.Pages
{
    [Authorize("AdminOnly")]
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
            Debug.WriteLine("test");
        }
    }
}
