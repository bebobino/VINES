
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using VINES.Models;

namespace VINES.Pages.Forums
{

    public class EditModel : PageModel
    {
        private readonly DatabaseContext _context;
        public List<ForumCategory> categories { get; set; }
        public EditModel(DatabaseContext context)
        {
            _context = context;

        }

        [BindProperty]
        public ForumPost ForumPost { get; set; } = default;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            categories = _context.ForumCategories.ToList();
            if ((id == null || _context.ForumPosts == null))
            {
                return NotFound();
            }

            var fp = await _context.ForumPosts.FirstOrDefaultAsync(m => m.forumPostID == id);
            if (fp == null)
            {
                return NotFound();
            }

            ForumPost = fp;
            return Page();

        }

        public async Task<IActionResult> OnPostAsync(int catID)
        {
            if (!ModelState.IsValid)
            {

                return Page();
            }

            _context.Attach(ForumPost).State = EntityState.Modified;



            try
            {
                ForumPost.forumCategoryID = catID;
                ForumPost.lastModified = DateTime.Now;
                ForumPost.dateAdded = DateTime.Now;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ForumPostExists(ForumPost.forumPostID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("Index");
        }
        private bool ForumPostExists(int id)
        {
            return (_context.ForumPosts?.Any(e => e.forumPostID == id)).GetValueOrDefault();
        }
    }
}
