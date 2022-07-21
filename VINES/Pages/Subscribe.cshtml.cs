using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Configuration;
using System.Threading.Tasks;
using System;
using VINES.Helper;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

namespace VINES.Pages
{
    public class SubscribeModel : PageModel
    {
        public IConfiguration configuration { get; }
        public SubscribeModel(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPostCheckout(double total)
        {
            var payPalAPI = new PayPalAPI(configuration);
            string url = await payPalAPI.getRedirectURLToPayPal(total, "PHP");
            Debug.WriteLine(payPalAPI.paypalID);
            Debug.WriteLine(url);
            Console.WriteLine(total);
            return Redirect(url);
        }
    }
}
