using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using VINES.Models;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Linq;
using System.Collections.Generic;
using System.Security.Claims;
using VINES.Processes;

namespace VINES.Pages.Account
{
    [AllowAnonymous]
    public class LoginAdvertModel : PageModel
    {
        private readonly DatabaseContext Db;

        public LoginAdvertModel(DatabaseContext Db)
        {
            this.Db = Db;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string password { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/advertiserLanding");

            if (ModelState.IsValid)
            {
                var help = new Help();
                var user = Db.Users.Where(f => f.email == Input.email && f.password == help.Hash(Input.password)).FirstOrDefault();
                if (user == null)
                {
                    ModelState.AddModelError("Error", "ERROR: You are not a registered user.");
                    return Page();
                }
                else if (user.password != help.Hash(Input.password))
                {
                    ModelState.AddModelError("Error", "ERROR: Invalid Email/Password combination.");
                    return Page();
                }
                else if (!user.emailAuth)
                {
                    ModelState.AddModelError("Error", "ERROR: Email not yet authenticated.");
                    return Page();
                }
                else if (user.roleID != 2)
                {
                    ModelState.AddModelError("Error", "ERROR: Not an Advertiser.");
                    return Page();
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.userID.ToString()),
                    new Claim(ClaimTypes.Name, user.email),
                    new Claim(ClaimTypes.Role, "Advertiser"),
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        principal,
                        new AuthenticationProperties { IsPersistent = false });



                return LocalRedirect(returnUrl);
            }

            return Page();
        }
    }
}
