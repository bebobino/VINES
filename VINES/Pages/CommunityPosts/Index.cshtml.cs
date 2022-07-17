using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using VINES.Models;
using System.Diagnostics;

namespace VINES.Pages.CommunityPosts
{
    [Authorize(Roles = "Admin")]
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
            CommunityPosts = db.CommunityPosts.ToList();
            CommunityPostCategories = db.CommunityPostCategories.ToList();
        }

        public IActionResult OnPostCreate(string title, int category, string content)
        {
            var communitypost = new CommunityPost
            {
                communityPostTitle = title,
                communityPostCategory = category,
                communityPostContent = content,
                dateAdded = DateTime.Now,
                lastModified = DateTime.Now


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
            var communitypost = db.CommunityPosts.Find(id);
            return new JsonResult(communitypost);
        }

        public IActionResult OnPostUpdate(int id, string title, int category, string content)
        {
            Debug.WriteLine("test");

            var communitypost = db.CommunityPosts.Find(id);
            communitypost.communityPostTitle = title;
            communitypost.communityPostCategory = category;
            communitypost.communityPostContent = content;
            communitypost.lastModified = DateTime.Now;
            db.SaveChanges();
            return RedirectToPage("Index");
        }
    }
}
