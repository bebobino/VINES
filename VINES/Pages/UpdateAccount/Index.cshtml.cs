using Microsoft.AspNetCore.Authorization;
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
using VINES.Processes;

namespace VINES.Pages.UpdateAccount
{
    [Authorize]
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
            var help = new Help();
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int idd = int.Parse(id);
            user = db.Users.Where(x => x.userID == idd).FirstOrDefault();
            //fname midmane lname contact
            user.firstName = help.Decrypt(user.firstName);
            user.middleName = help.Decrypt(user.middleName);
            user.lastName = help.Decrypt(user.lastName);
            user.contactNumber = help.Decrypt(user.contactNumber);
        }

        public async Task<IActionResult> OnPost()
        {

            var help = new Help();

            user.firstName = help.Encrypt(user.firstName);
            user.middleName = help.Encrypt(user.middleName);
            user.lastName = help.Encrypt(user.lastName);
            user.contactNumber = help.Encrypt(user.contactNumber);
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
