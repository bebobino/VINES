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
    public class ContentModel : PageModel
    {
        public List<ForumPost> ForumPosts { get; set; }
        private DatabaseContext db;
        public ContentModel(DatabaseContext _db)
        {
            db = _db;
        }


        public IActionResult OnGetFind(int id)
        {
            var forumpost = db.ForumPosts.Find(id);
            return new JsonResult(forumpost);
        }

        public IActionResult Details(int id)
        {
            IEnumerable<ForumPost> db = (IEnumerable<ForumPost>)OnGetFind(id);
            var forumPosts = db.First(x => x.forumPostID == id);
            return RedirectToPage("Content");

        }


    }

}
