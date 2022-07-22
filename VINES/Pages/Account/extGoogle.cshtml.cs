using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using VINES.Data;
using VINES.Models;
using VINES.Processes;

namespace VINES.Pages.Account
{
    [Authorize(Policy = "Google")]
    public class extGoogleModel : PageModel
    {
        private readonly DatabaseContext db;

        public extGoogleModel(DatabaseContext _db)
        {
            _db = db;

            user = db.Users.ToList();
        }

        [BindProperty]
        public List<User> user { get; set; }



        public async Task<RedirectToPageResult> OnGetAsync()
        {
            var accessToken = await HttpContext.GetTokenAsync(
            GoogleDefaults.AuthenticationScheme, "access_token");


            var claims = User.Claims;

            var name = User.FindFirstValue(ClaimTypes.Name);
            var email = User.FindFirstValue(ClaimTypes.Email);

            var emails = db.Users.Find(email);

            Debug.WriteLine(emails);

            return RedirectToPage("Index");
            
        }

    }
}
