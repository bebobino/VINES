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

namespace VINES.Pages.Forums
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly DatabaseContext _db;
        public IndexModel(DatabaseContext db)
        {
            _db = db;
        }

        public List<ForumPost> forumPosts { get; set; }
        public List<ForumCategory> categories { get; set; }

        public async Task OnGet()
        {
            forumPosts = await _db.ForumPosts.ToListAsync();
            categories = await _db.ForumCategories.ToListAsync();
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var forum = await _db.ForumPosts.FindAsync(id);
            if (forum == null)
            {
                return NotFound();

            }
            _db.ForumPosts.Remove(forum);
            await _db.SaveChangesAsync();

            return RedirectToPage("Index");
        }

    }
}
