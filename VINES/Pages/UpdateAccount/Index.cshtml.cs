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

        Help help = new Help();

        public void OnGet()
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int idd = int.Parse(id);
            user = db.Users.Where(x => x.userID == idd).FirstOrDefault();

            user.firstName = help.Decrypt(user.firstName);
            user.middleName = help.Decrypt(user.middleName);
            user.lastName = help.Decrypt(user.lastName);
            user.contactNumber = help.Decrypt(user.contactNumber);
        }

        public async Task<IActionResult> OnPost(string firstName, string middleName, string lastName, string contactNumber)
        {

            db.Attach(user).State = EntityState.Modified;

            user.firstName = help.Encrypt(firstName);
            user.middleName = help.Encrypt(middleName);
            user.lastName = help.Encrypt(lastName);
            user.contactNumber = help.Encrypt(contactNumber);
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