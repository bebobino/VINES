using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using VINES.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Threading.Tasks;

namespace VINES.Pages.CommunityPosts
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly DatabaseContext _db;
        public IndexModel(DatabaseContext db)
        {
            _db = db;
        }

        public List<CommunityPost> communityPosts { get; set; }
        public List<CommunityPostCategoriesModel> categories { get; set; }

        public async Task OnGet()
        {
            communityPosts = await _db.CommunityPosts.ToListAsync();
            categories = await _db.CommunityPostCategories.ToListAsync();
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var com = await _db.CommunityPosts.FindAsync(id);
            if (com == null)
            {
                return NotFound();

            }
            _db.CommunityPosts.Remove(com);
            await _db.SaveChangesAsync();

            return RedirectToPage("Index");
        }

    }
}
