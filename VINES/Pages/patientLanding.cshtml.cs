using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace VINES.Pages
{
    [Authorize("Patient")]
    public class patientLandingModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
