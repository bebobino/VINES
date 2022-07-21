using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using VINES.Helper;

namespace VINES.Pages
{
    [Authorize("AdvertiserOnly")]
    public class advertiserLandingModel : PageModel
    {
        private IWebHostEnvironment _environment;
        public IConfiguration _configuration { get; }
        public advertiserLandingModel(IWebHostEnvironment environment, IConfiguration configuration)
        {
            _environment = environment;
            _configuration = configuration;
        }
        [BindProperty]
        public IFormFile Photo { get; set; }

         


        public async Task OnPost()
        {
            var file = Path.Combine(_environment.ContentRootPath, "Ads\\Img", Photo.FileName);
            Debug.WriteLine("file is in: "+file);
            using (var fileStream = new FileStream(file, FileMode.Create))
            {
                await Photo.CopyToAsync(fileStream);
            }
        }

        public async Task<IActionResult> OnPostCheckout(double total)
        {
            var payPalAPI = new PayPalAPI(_configuration);
            string url = await payPalAPI.getRedirectURLToPayPal(total, "PHP");
            Debug.WriteLine(payPalAPI.paypalID);
            Debug.WriteLine(url);
            Console.WriteLine(total);
            return Redirect(url);
        }


        public void OnGet()
        {
        }
    }
}
