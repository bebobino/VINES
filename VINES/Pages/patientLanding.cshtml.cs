using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VINES.Models;
using VINES.Processes;

namespace VINES.Pages
{
    [Authorize("PatientOnly")]
    public class patientLandingModel : PageModel
    {
        private readonly ILogger<patientLandingModel> _logger;




        public List<WebPages> bookedWeb { get; set; } = new List<WebPages>();
        [BindProperty(SupportsGet = true)]
        public List<CommunityPost> CommunityPosts { get; set; }
        public List<WebPages> WebPages { get; set; }
        public List<Vaccines> Vaccines { get; set; }
        public List<Diseases> Diseases { get; set; }
        public List<Institutions> Institutions { get; set; }
        public List<Sources> Sources { get; set; }
        //public List <InstitutionVaccines> InstitutionVaccines { get; set; }
        private DatabaseContext db;
        public Patients patient { get; set; }

        public decimal maxVax { get; set; }
        [BindProperty]
        public int vax { get; set; }
        [BindProperty]
        public decimal budget { get; set; }
        [BindProperty]
        public string url { get; set; }


        //Ads
        [BindProperty]
        public List<Advertisement> ads { get; set; }

        [BindProperty]
        public List<bookies> Book { get; set; }
        public int rnd { get; set; }
        public Random rando = new Random(DateTime.Now.Millisecond);

        //pagination
        public int PageNo { get; set; }
        public bool ShowPrevious { get; set; }
        public bool ShowNext { get; set; }
        public int Count { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }

        public class bookies
        {
            public WebPages web { get; set; }
            public Boolean isChecked { get; set; }
        }
        


        public patientLandingModel(ILogger<patientLandingModel> logger, DatabaseContext _db)
        {
            _logger = logger;
            db = _db;
        }
        public void OnGet(int p = 1 , int s = 5)
        {
            var location = new Uri($"{Request.Scheme}://{Request.Host}{Request.Path}{Request.QueryString}");

            url = location.AbsoluteUri;
            Debug.WriteLine(url);
            Book = new List<bookies>();
            try
            {
                
                var uid = User.FindFirstValue(ClaimTypes.NameIdentifier);
                int x = int.Parse(uid);
                patient = db.Patients.Where(p => p.userID == x).FirstOrDefault();
                var bookMarks = db.bookmarks.Where(b => b.patientID == patient.patientID).ToList();
                foreach(var b in bookMarks)
                {
                    bookedWeb.Add(db.WebPages.Where(a => a.postID == b.postID).FirstOrDefault());
                }
                maxVax = decimal.Parse(db.InstitutionVaccines.Max(i => i.price).ToString());
            }catch(Exception)
            {

            }
            rando = new Random(DateTime.Now.Millisecond);
            ads = db.advertisements.Where(ad => ad.endDate > DateTime.UtcNow && ad.clicks > 0).ToList();
            if (ads.Count > 0)
            {
                rnd = rando.Next(0, ads.Count - 1);
            }
            Sources = db.sources.ToList();
            CommunityPosts = db.CommunityPosts.ToList();
            WebPages = db.WebPages.OrderByDescending(webpage => webpage.uploadDate).Skip((p - 1) * s).Take(s).ToList();

            foreach (var web in WebPages)
            {
                var b = false;
                if(db.bookmarks.Where(b => b.postID == web.postID && b.patientID == patient.patientID).FirstOrDefault() != null)
                {
                    b = true;
                }
                Book.Add(new bookies
                {
                    web = web,
                    isChecked = b
                });
            }


            Count = db.WebPages.Count();
            PageSize = s;
            TotalPages = (int)Math.Ceiling(decimal.Divide(Count, PageSize));
            PageNo = p;
            ShowPrevious = (PageNo > 1) ? true : false;
            ShowNext = (PageNo < TotalPages) ? true : false;
            Vaccines = db.vaccines.Include("disease").ToList();
            Institutions = db.Institutions.ToList();
            Help help = new Help();
            help.checkIP();

            var rand = new Random();

        }

        public async Task<IActionResult> OnPost()
        {
            Debug.WriteLine(vax);
            Debug.WriteLine(budget);
            return Redirect("/Patient/Preference?vax="+vax+"&budget="+budget);
        }

        public async Task<IActionResult> OnPostBook()
        {
            var uid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int x = int.Parse(uid);
            patient = db.Patients.Where(p => p.userID == x).FirstOrDefault();
            Debug.WriteLine("Eto malupet");
            Debug.WriteLine("Post books: " + Book.Count);
            foreach(var book in Book)
            {

                if (book.isChecked && db.bookmarks.Where(b => b.postID == book.web.postID && b.patientID == patient.patientID).FirstOrDefault() == null)
                {
                    Debug.WriteLine("Added");
                    var mark = new Bookmarks
                    {
                        postID = book.web.postID,
                        patientID = patient.patientID,
                    };
                    db.bookmarks.Add(mark);
                    await db.SaveChangesAsync();
                }
                if (!book.isChecked && db.bookmarks.Where(b => b.postID == book.web.postID && b.patientID == patient.patientID).FirstOrDefault() != null)
                {
                    Debug.WriteLine("Deleted");
                    var mark = db.bookmarks.Where(b => b.postID == book.web.postID && b.patientID == patient.patientID).FirstOrDefault();
                    db.bookmarks.Remove(mark);
                    await db.SaveChangesAsync();
                }




            }
            
            return Redirect(url);
        }



    }
}
