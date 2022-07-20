
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

namespace VINES.Pages.CommunityPosts
{
    
    public class EditModel : PageModel
    {
        private readonly DatabaseContext _context;
        public EditModel(DatabaseContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CommunityPost CommunityPost { get; set; } = default;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if ((id == null || _context.CommunityPosts == null))
            {
                return NotFound();
            }

            var cp = await _context.CommunityPosts.FirstOrDefaultAsync(m => m.communityPostID == id);
            if (cp == null)
            {
                return NotFound();
            }

            CommunityPost = cp;
            return Page();

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                
                return Page();
            }

            _context.Attach(CommunityPost).State = EntityState.Modified;

            

            try
            {
                CommunityPost.lastModified = DateTime.Now;
                CommunityPost.dateAdded = DateTime.Now;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommunityPostExists(CommunityPost.communityPostID))
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
        private bool CommunityPostExists(int id)
        {
            return (_context.CommunityPosts?.Any(e => e.communityPostID == id)).GetValueOrDefault();
        }
    }
}
