using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace VINES.Pages.Account
{
    public class AccessDeniedModel : PageModel
    {
        public void OnGet()
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("Patient")) {

                Response.Redirect("/patientLanding");
            }
            else if (User.Identity.IsAuthenticated && User.IsInRole("Advertiser"))
            {

                Response.Redirect("/advertiserLanding");
            }
            else if (User.Identity.IsAuthenticated && User.IsInRole("Admin"))
            {

                Response.Redirect("/adminLanding");
            }
            else
            {
                Response.Redirect("/Index");
            }
        }
    }
}
