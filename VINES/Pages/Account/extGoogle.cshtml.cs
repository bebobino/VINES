using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace VINES.Pages.Account
{
    [Authorize]
    public class extGoogleModel : PageModel
    {

        public async Task OnGetAsync()
        {
            var accessToken = await HttpContext.GetTokenAsync(
            GoogleDefaults.AuthenticationScheme, "access_token");

        }
    }
}
