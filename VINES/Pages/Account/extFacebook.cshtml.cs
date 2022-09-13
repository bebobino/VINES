using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VINES.Data;
using VINES.Models;
using VINES.Processes;

namespace VINES.Pages.Account
{
    [Authorize(Policy = "Facebook")]
    public class extFacebookModel : PageModel
    {
        public readonly DatabaseContext db;

        public extFacebookModel(DatabaseContext db)
        {
            this.db = db;
        }

        [BindProperty]
        public User users { get; set; }


        public async Task OnGetAsync()
        {
            var accessToken = await HttpContext.GetTokenAsync(
            FacebookDefaults.AuthenticationScheme, "access_token");

        }
        public async Task<IActionResult> OnPost()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, User.FindFirstValue(ClaimTypes.NameIdentifier)),
                    new Claim(ClaimTypes.Name, email),
                    new Claim(ClaimTypes.Role, "Patient"),
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
                    new AuthenticationProperties { IsPersistent = false });

                return RedirectToPage("/patientLanding");
        }
    }
}