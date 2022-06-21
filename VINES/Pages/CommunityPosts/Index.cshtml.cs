using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Linq;
using VINES.Models;

namespace VINES.Pages.CommunityPosts
{
    public class IndexModel : PageModel
    {
        public List<CommunityPost> CommunityPosts { get; set; }

        private DatabaseContext db;
        public IndexModel(DatabaseContext _db)
        {
            db = _db;
        }
        public void OnGet()
        {
            CommunityPosts = db.CommunityPosts.ToList();
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
