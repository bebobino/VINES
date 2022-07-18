using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using VINES.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace VINES.Pages.ForumPosts
{
    
    public class IndexModel : PageModel
    {
        public List<ForumPost> ForumPosts { get; set; }
        public List<ForumCategory> ForumCategories { get; set; }

        private DatabaseContext db;
        public IndexModel(DatabaseContext _db)
        {
            db = _db;
        }
        public void OnGet()
        {
            ForumPosts = db.ForumPosts.Include("forumPostCategory").Include("posts").ToList();
            ForumCategories = db.ForumCategories.ToList();
        }

        public IActionResult OnPostCreate(int category, string content)
        {
            var post = new Posts
            {
                isVisible = true
            };
            db.Post.Add(post);
            db.SaveChanges();
            var forumpost = new ForumPost
            {
                
                forumCategoryID = category,
                forumContent = content,
                dateAdded = DateTime.Now,
                lastModified = DateTime.Now,
                postID = post.postID


            };
            db.ForumPosts.Add(forumpost);
            db.SaveChanges();
            return RedirectToPage("Index");
        }

        public IActionResult OnPostDelete(int id)
        {
            var forumpost = db.ForumPosts.Find(id);
            db.ForumPosts.Remove(forumpost);
            db.SaveChanges();

            return RedirectToPage("Index");
        }

        public IActionResult OnGetFind(int id)
        {
            var forumPostID = db.ForumCategories.Find(id);

            return new JsonResult(forumPostID);
        }

        public IActionResult OnPostUpdate(int id,  int category, string content)
        {
            var forumpost = db.ForumPosts.Find(id);
            
            forumpost.forumCategoryID = category;
            forumpost.forumContent = content;
            forumpost.lastModified = DateTime.Now;
            db.SaveChanges();
            return RedirectToPage("Index");
        }
    }
}
