using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VINES.Models;

namespace VINES.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public List<CommunityPost> CommunityPosts { get; set; }
        public List<CommunityPostCategories> CommunityPostCategories { get; set; }

        private DatabaseContext db;

        public IndexModel(ILogger<IndexModel> logger, DatabaseContext _db)
        {
            _logger = logger;
            db = _db;
        }
        public void OnGet()
        {
            CommunityPostCategories = db.CommunityPostCategories.ToList();
            CommunityPosts = db.CommunityPosts.ToList();
        }
        
    }
}
