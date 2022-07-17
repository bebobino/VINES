using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace VINES.Pages.Shared
{
    [Authorize]
    public class _LayoutAdminModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
