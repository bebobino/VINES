using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VINES.Data;
using VINES.Models;
using VINES.Processes;

namespace VINES.Pages.Account
{
    [AllowAnonymous]
    public class RegisterConfirmationModel : PageModel
    {
        private DatabaseContext db;
        public RegisterConfirmationModel(DatabaseContext db)
        {
            this.db = db;
        }

        public void OnGet(int key1, string key2)
        {
            Help help = new Help();
            try
            {
                var user = db.Users.Find(key1);
                if (user == null)
                {
                    Debug.WriteLine("does not exist");
                }
                else
                {
                    var email = user.email;
                    key2 = key2.Replace(" ", "+");
                    if (email.Equals(key2))
                    {
                        user.emailAuth = true;
                        db.SaveChanges();
                        Debug.WriteLine("Success!");
                    }
                    else
                    {
                        Debug.WriteLine("Failure");
                    }
                }
            }
            catch(Exception e)
            {

            }

        }

    }
    
}
