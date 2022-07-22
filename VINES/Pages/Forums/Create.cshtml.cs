using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(int catID, string content, int pID)
        {
            var posts = new Posts
            {
                isVisible = true
            };
            _Db.Post.Add(posts);
            await _Db.SaveChangesAsync();

            var forumpost = new ForumPost
            {
                
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
