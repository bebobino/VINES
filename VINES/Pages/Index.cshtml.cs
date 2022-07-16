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

namespace VINES.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public List<CommunityPost> CommunityPosts { get; set; }
        public List<WebPages> WebPages { get; set; }
        public List<Vaccines> Vaccines { get; set; }
        public List<Diseases> Diseases { get; set; }
        public List<Institutions> Institutions { get; set; }
        private DatabaseContext db;

        public IndexModel(ILogger<IndexModel> logger, DatabaseContext _db)
        {
            _logger = logger;
            db = _db;
        }
        public void OnGet()
        {
            CommunityPosts = db.CommunityPosts.ToList();
            WebPages = db.WebPages.ToList();
            Vaccines = db.Vaccines.Include("diseases").ToList();
            Institutions = db.Institutions.ToList();
            WebPages = WebPages.OrderByDescending(webpage => webpage.uploadDate).ToList();
        }

        
    }
}
