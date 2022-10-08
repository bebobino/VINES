using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VINES.Data;
using VINES.Models;
using VINES.Processes;

namespace VINES.Pages.Status
{
    [Authorize("AdminOnly")]
    public class IndexModel : PageModel
    {
        private readonly DatabaseContext _db;

        public IndexModel(DatabaseContext db)
        {
            _db = db;
        }
        public List<User> users { get; set; }
        public List<Roles> roles { get; set; }

        public void OnGet()
        {
            users = _db.Users.Where(e => e.roleID != 1).ToList();
            roles = _db.roles.ToList();
        }

        public async Task<IActionResult> OnPost(List<User> users)
        {
            var help = new Help();
            foreach (var use in users)
            {
                
                var x = _db.Users.Where(c => c.userID == use.userID).FirstOrDefault();
                if(x.isBlocked != use.isBlocked)
                {
                    if (use.isBlocked)
                    {
                        Console.WriteLine("Blocking now... bye "+help.Decrypt(x.email));
                        help.sendEmail(help.Decrypt(x.email), "ACCOUNT BLOCKED", "Your account has been blocked for activities deemed inappropriate by the administrators. For questions please contact us via email at vinessupport@gmail.com");

                    }
                    else
                    {
                        Console.WriteLine("Unblocking now");
                        help.sendEmail(help.Decrypt(x.email), "ACCOUNT UNBLOCKED", "Your account has been unblocked you may now use your account. For questions please contact us via email at vinessupport@gmail.com");
                    }
                }
                else
                {
                    Console.WriteLine("Unchanged");
                }
                _db.Users.Where(c => c.userID == use.userID).FirstOrDefault().isBlocked = use.isBlocked;
                }

            _db.SaveChanges();
            return RedirectToPage("Index");
        }
    }
}