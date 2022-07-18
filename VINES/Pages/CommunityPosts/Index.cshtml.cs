using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using VINES.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace VINES.Pages.CommunityPosts
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public List<CommunityPost> CommunityPosts { get; set; }
        public List<CommunityPostCategoriesModel> CommunityPostCategories { get; set; }

        private DatabaseContext db;

        public IndexModel(DatabaseContext _db)
        {
            db = _db;
        }

        public IActionResult OnGet()
        {
            var communityposts = db.CommunityPosts.ToList();
            return RedirectToPage("Index");
        }
    }
}
