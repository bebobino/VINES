using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

            return RedirectToPage("/patientLanding");
        }
    }
}
