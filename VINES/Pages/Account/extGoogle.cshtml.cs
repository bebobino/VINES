using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
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
            db = _db;
        }

        public User user { get; set; }



        public async Task<IActionResult> OnGetAsync()
        {

            var accessToken = await HttpContext.GetTokenAsync(
            GoogleDefaults.AuthenticationScheme, "access_token");

                var email = User.FindFirstValue(ClaimTypes.Email);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, ClaimTypes.NameIdentifier),
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
