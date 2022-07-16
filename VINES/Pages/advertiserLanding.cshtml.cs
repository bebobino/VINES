using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace VINES.Pages
{
    [Authorize]
    public class advertiserLandingModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
