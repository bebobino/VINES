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
        public void OnGet()
        {
            CommunityPosts = db.CommunityPosts.Include("communityPostCategory").Include("posts").ToList();
            CommunityPostCategories = db.CommunityPostCategories.ToList();
        }

        public IActionResult OnPostCreate(string title, int category, string content)
        {
            var post = new Posts
            {
                isVisible = true
            };
            db.Post.Add(post);
            db.SaveChanges();
            var communitypost = new CommunityPost
            {
                communityPostTitle = title,
                communityPostCategoryID = category,
                communityPostContent = content,
                dateAdded = DateTime.Now,
                lastModified = DateTime.Now,
                postID = post.postID


            };
            db.CommunityPosts.Add(communitypost);
            db.SaveChanges();
            return RedirectToPage("Index");
        }

        public IActionResult OnPostDelete(int id)
        {
            var communitypost = db.CommunityPosts.Find(id);
            db.CommunityPosts.Remove(communitypost);
            db.SaveChanges();

            return RedirectToPage("Index");
        }

        public IActionResult OnGetFind(int id)
        {
            var communitypostID = db.CommunityPosts.Find(id);
            
            return new JsonResult(communitypostID);
        }

        public IActionResult OnPostUpdate(int id, string title, int category, string content)
        {
            var communitypost = db.CommunityPosts.Find(id);
            communitypost.communityPostTitle = title;
            communitypost.communityPostCategoryID = category;
            communitypost.communityPostContent = content;
            communitypost.lastModified = DateTime.Now;
            db.SaveChanges();
            return RedirectToPage("Index");
        }
    }
}
