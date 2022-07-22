using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using VINES.Models;
using VINES.Processes;

namespace VINES.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;




        [BindProperty(SupportsGet = true)]
        

        public List<CommunityPost> CommunityPosts { get; set; }
        public List<WebPages> WebPages { get; set; }
        public List<Vaccines> Vaccines { get; set; }
        public List<Diseases> Diseases { get; set; }
        public List<Institutions> Institutions { get; set; }


        //Ads
        [BindProperty]
        public List<Advertisement> ads { get; set; }
        public int rnd { get; set; }
        public Random rando =new Random(DateTime.Now.Millisecond);



        public List<Sources> Sources { get; set; }
        private DatabaseContext db;

        //pagination
        public int PageNo { get; set; }
        public bool ShowPrevious { get; set; }
        public bool ShowNext { get; set; }
        public int Count { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }


        public async Task<IActionResult> OnPostAd(int id)
        {
            Debug.WriteLine("hahaha "+ id);
            var ads = await db.advertisements.FindAsync(id);
            ads.clicks--;
            await db.SaveChangesAsync();
            return Redirect(ads.url);
        }


        public IndexModel(ILogger<IndexModel> logger, DatabaseContext _db)
        {
            _logger = logger;
            db = _db;
        }
        public void OnGet(int p = 1 , int s = 5)
        {
            rando = new Random(DateTime.Now.Millisecond);
            ads = db.advertisements.Where(ad => ad.endDate > DateTime.UtcNow && ad.clicks > 0).ToList();
            if(ads.Count > 0)
            {
                rnd = rando.Next(0, ads.Count - 1);
            }
            Debug.WriteLine(ads.Count+" ,"+rnd);
            Sources = db.sources.ToList();
            CommunityPosts = db.CommunityPosts.ToList();
            WebPages = db.WebPages.OrderByDescending(webpage => webpage.uploadDate).Skip((p - 1) * s).Take(s).ToList();
            Count = db.WebPages.Count();
            PageSize = s;
            TotalPages = (int)Math.Ceiling(decimal.Divide(Count, PageSize));
            PageNo = p;
            ShowPrevious = (PageNo > 1) ? true : false;
            ShowNext = (PageNo < TotalPages) ? true : false;
            Vaccines = db.vaccines.Include("disease").ToList();
            Institutions = db.Institutions.ToList();
            Help help = new Help();
            Debug.WriteLine("test");
            help.checkIP();

            var rand = new Random();

        }

        
    }
}
