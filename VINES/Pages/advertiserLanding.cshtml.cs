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
using System.Security.Claims;
using System.Threading.Tasks;
using VINES.Helper;
using VINES.Models;
using VINES.Processes;

namespace VINES.Pages
{
    [Authorize("AdvertiserOnly")]
    public class advertiserLandingModel : PageModel
    {
        private IWebHostEnvironment _environment;
        public IConfiguration _configuration { get; }
        private readonly DatabaseContext _db;
        public advertiserLandingModel(IWebHostEnvironment environment, IConfiguration configuration, DatabaseContext db)
        {
            _environment = environment;
            _configuration = configuration;
            _db = db;
        }

        public List<AdvertisementType> AdTypes { get; set; }
        public List<Advertisement> Ads { get; set; }
        [BindProperty]
        public IFormFile Photo { get; set; }
        [BindProperty]
        public Advertisement Ad { get; set; }
        
        public List<AdReceipts> adr { get; set; }

         


        public async Task<IActionResult> OnPost()
        {
            string[] fileType = Photo.FileName.Split(".");
            int x = fileType.Length - 1;
            if (string.Equals(fileType[x], "jpg", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(fileType[x], "png", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(fileType[x], "jpeg", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(fileType[x], "gif", StringComparison.OrdinalIgnoreCase)
                )
            {


                string uploadsFolder = Path.Combine(_environment.WebRootPath, "images");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                Debug.WriteLine("file is in: " + filePath);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await Photo.CopyToAsync(fileStream);
                }

                var aid = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var uid = int.Parse(aid);
                var adver = _db.advertisers.Where(a => a.userID == uid).FirstOrDefault();

                Ad.imgsrc = uniqueFileName;
                Ad.advertiserID = adver.advertiserID;
                Ad.lastModified = DateTime.UtcNow;
                Ad.advertisementTypeID = 1;
                Ad.dateAdded = DateTime.UtcNow;
                Ad.clicks = 0;
                Ad.endDate = DateTime.UtcNow;
                _db.advertisements.Add(Ad);
                await _db.SaveChangesAsync();
                Debug.WriteLine("tama ka");
            }
            else
            {
                Debug.WriteLine("bobo");
            }
            return RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnPostCheckout(double total, int id)
        {
            var help = new Help();
            var adid = _db.advertisements.Find(id);
            var payPalAPI = new PayPalAPI(_configuration);
            string url = await payPalAPI.getRedirectURLToPayPal(total, "PHP");
            adid.textContent = help.Encrypt(payPalAPI.paypalID);
            _db.SaveChanges();
            Debug.WriteLine(url);
            Console.WriteLine(total);
            return Redirect(url);
        }


        public void OnGet()
        {
            var aid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var uid = int.Parse(aid);
            AdTypes = _db.advertisementTypes.ToList();
            Ads = _db.advertisements.Where(a => a.advertiser.userID == uid).ToList();
            adr = _db.AdReceipts.Where(a => a.advertiser.userID == uid).ToList();
        }
    }
}
