using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VINES.Helper;
using VINES.Models;

namespace VINES.Pages
{
    [Authorize("AdvertiserOnly")]
    public class advertiserLandingModel : PageModel
    {
        private IWebHostEnvironment _environment;
        public IConfiguration _configuration { get; }
        private DatabaseContext _db;
        public advertiserLandingModel(IWebHostEnvironment environment, IConfiguration configuration, DatabaseContext db)
        {
            _environment = environment;
            _configuration = configuration;
            _db = db;
        }
        [BindProperty]
        public IFormFile Photo { get; set; }
        [BindProperty]
        public Advertisement ad { get; set; }
        public List<AdvertisementType> types { get; set; }
        public List<Advertisement> ads { get; set; }
        public int id;

         


        public async Task OnPost()
        {
            var file = Path.Combine(_environment.ContentRootPath, "Pages\\Ads\\Img", Photo.FileName);
            Debug.WriteLine("file is in: "+file);
            using (var fileStream = new FileStream(file, FileMode.Create))
            {
                await Photo.CopyToAsync(fileStream);
            }
            ad.url = file;
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
            try
            {

            }catch(Exception e)
            {

            }
            
            types = _db.advertisementTypes.ToList();
            ads = _db.advertisements.ToList();
        }
    }
}
