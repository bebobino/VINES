using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VINES.Data;
using VINES.Models;

namespace VINES.Pages.Forums
{
    public class CreateModel : PageModel
    {
        private readonly DatabaseContext _Db;
        public List<ForumCategory> categories { get; set; }


        public CreateModel(DatabaseContext Db)
        {
            _Db = Db;
            categories = _Db.ForumCategories.ToList();
        }
        [BindProperty]
        public ForumPost forumPost { get; set; }
        public User users { get; set; }
        public void OnGet()
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int idd = int.Parse(id);
        }

        public async Task<IActionResult> OnPost(string title, int catID, string content, int pID)
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int idd = int.Parse(id);

            var posts = new Posts
            {
                isVisible = true
            };
            _Db.Post.Add(posts);
            await _Db.SaveChangesAsync();

            var forumpost = new ForumPost
            {

                userID = idd,
                forumTitle = title,
                forumCategoryID = catID,
                forumContent = content,
                dateAdded = DateTime.Now,
                lastModified = DateTime.Now,
                postID = posts.postID
            };

            _Db.ForumPosts.Add(forumpost);
            await _Db.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
