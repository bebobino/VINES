using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VINES.Data;
using VINES.Models;
using VINES.Processes;

namespace VINES.Pages.Forums
{
    public class DetailsModel : PageModel
    {

        private readonly DatabaseContext _context;
        public List<ForumCategory> categories { get; set; }


        public DetailsModel(DatabaseContext context)
        {
            _context = context;
        }

        [BindProperty]
        public User user { get; set; }

        [BindProperty]
        public ForumPost forumpost { get; set; }
        [BindProperty]
        public ForumComment comment { get; set; }
        public List<User> users { get; set; }

        public List<ForumComment> comments { get; set; }

        Help help = new Help();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var uid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int idd = int.Parse(uid);


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

            forumpost = fp;

            users = _context.Users.ToList();
            foreach (var x in users)
            {
                x.email = help.Decrypt(x.email);
            }
            comments = _context.ForumComments.Where(c => c.forumPostID == fp.forumPostID).ToList();

            return Page();
        }


        public async Task<IActionResult> OnPost(string comment, int? id)
        {
            var fp = await _context.ForumPosts.FirstOrDefaultAsync(m => m.forumPostID == id);

            forumpost = fp;

            var uid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int idd = int.Parse(uid);

            var fcomment = new ForumComment
            {
                forumPostID = fp.forumPostID,
                userID = idd,
                comment = comment,
                dateAdded = DateTime.Now,
                lastModified = DateTime.Now
            };

            _context.ForumComments.Add(fcomment);
            await _context.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
