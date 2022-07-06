using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using VINES.Models;
using VINES.Helper;

namespace VINES.Pages.Payments
{
    public class IndexModel : PageModel
    {


        public IConfiguration configuration { get; }
        public IndexModel(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public string Message { get; set; }










        public void OnPost()
        {
            Message = "Post used";
            Console.WriteLine(Message);
        }

        public async Task<IActionResult> OnPostCheckout(double total)
        {
            var payPalAPI = new PayPalAPI(configuration);
            string url = await payPalAPI.getRedirectURLToPayPal(total, "PHP");
            Console.WriteLine(total);
            return Redirect(url);
        }
    }
}