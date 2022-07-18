using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VINES.Models;

namespace VINES.Pages.CommunityPosts
{
    public class CreateModel : PageModel
    {
        private readonly DatabaseContext _Db;
        public List<CommunityPostCategoriesModel> categories { get; set; }
       

        public CreateModel(DatabaseContext Db)
        {
            _Db = Db;
            categories = _Db.CommunityPostCategories.ToList();
        }
        [BindProperty]
        public CommunityPost communityPost { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(string title, int catID, string content, int pID)
        {
            var posts = new Posts
            {
                isVisible = true
            };
            _Db.Post.Add(posts);
            await _Db.SaveChangesAsync();
            
            var communitypost = new CommunityPost
            {
                communityPostTitle = title,
                communityPostCategoryID = catID,
                communityPostContent = content,
                dateAdded = DateTime.Now,
                lastModified = DateTime.Now,
                postID = posts.postID
            };

            _Db.CommunityPosts.Add(communitypost);
           await _Db.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
