using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace VINES.Pages.Account
{
    [Authorize(Policy = "Facebook")]
    public class extFacebookModel : PageModel
    {
        public async Task<IActionResult> OnGetAsync()
        {
            var accessToken = await HttpContext.GetTokenAsync(
            FacebookDefaults.AuthenticationScheme, "access_token");

            var email = User.FindFirstValue(ClaimTypes.Email);

            var claims = new List<Claim>
                {
                    
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
