using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using VINES.Models;
using System.Threading.Tasks;

namespace VINES.Pages.ForumPosts
{
    public class IndexModel : PageModel
    {
        public List<ForumPost> ForumPosts { get; set; }
        private DatabaseContext db;
        public IndexModel(DatabaseContext _db)
        {
            db = _db;
        }

        public void OnGet()
        {
            ForumPosts = db.ForumPosts.ToList();
        }

        public IActionResult OnPostCreate(string title, int category, string content)
        {
            var forumpost = new ForumPost
            {
                forumPostTitle = title,
                forumPostCategory = category,
                forumPostContent = content,
                dateAdded = DateTime.Now,
                lastModified = DateTime.Now


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
            var forumpost = db.ForumPosts.Find(id);
            return new JsonResult(forumpost);
        }
        
        public IActionResult OnPostDetails(int id, int fid)
        {
            
            return RedirectToPage("Content");
            

        }
        public IActionResult OnPostUpdate(int id, string title, int category, string content)
        {
            var forumPost = db.ForumPosts.Find(id);
            forumPost.forumPostTitle = title;
            forumPost.forumPostCategory = category;
            forumPost.forumPostContent = content;
            forumPost.lastModified = DateTime.Now;
            db.SaveChanges();
            return RedirectToPage("Index");
        }
    }

    

}

