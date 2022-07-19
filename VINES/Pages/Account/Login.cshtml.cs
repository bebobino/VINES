using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VINES.Models;
using VINES.Processes;

namespace VINES.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly DatabaseContext Db;

        public LoginModel(DatabaseContext Db)
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
            returnUrl ??= Url.Content("~/patientLanding");

            if (ModelState.IsValid)
            {
                var help = new Help();
                var user = Db.Users.Where(f => f.email == Input.email).FirstOrDefault();
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
                else if (user.roleID != 3)
                {
                    ModelState.AddModelError("Error", "ERROR: Not a Patient.");
                    return Page();
                }
                


                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.userID.ToString()),
                    new Claim(ClaimTypes.Name, user.email),
                    new Claim(ClaimTypes.Role, "Patient"),
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
                    new AuthenticationProperties { IsPersistent = false });

                return LocalRedirect(returnUrl);
            }

            return Page();
        }
       }
    }
