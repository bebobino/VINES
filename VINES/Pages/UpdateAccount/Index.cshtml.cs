using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VINES.Data;
using VINES.Models;

namespace VINES.Pages.UpdateAccount
{
    public class IndexModel : PageModel
    {
        private readonly DatabaseContext db;

        public IndexModel(DatabaseContext _db)
        {
            db = _db;
        }
        [BindProperty]
        public User user { get; set; }

        public void OnGet()
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int idd = int.Parse(id);
            user = db.Users.Where(x => x.userID == idd).FirstOrDefault();
        }

        public async Task<IActionResult> OnPost()
        {
            db.Attach(user).State = EntityState.Modified;

            user.dateOfBirth = user.dateOfBirth;
            user.isBlocked = user.isBlocked;
            user.isLocked = user.isLocked;
            user.failedAttempts = user.failedAttempts;
            user.emailAuth = user.emailAuth;
            user.genderID = user.genderID;
            user.lastModified = DateTime.UtcNow;

            db.SaveChanges();

            return RedirectToPage("Index");
        }
    }
}
