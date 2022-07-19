using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
using System.Security.Claims;

namespace VINES.Pages
{
    [Authorize("PatientOnly")]
    public class patientLandingModel : PageModel
    {
        public void OnGet()
        {
            var ID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Debug.WriteLine(ID);
        }
    }
}
