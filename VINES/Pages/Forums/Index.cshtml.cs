using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using VINES.Models;

namespace VINES.Pages.Forums
{
    public class IndexModel : PageModel
    {
        private readonly DatabaseContext db;
        public List<ForumCategory> forums { get; set; }
        public IndexModel(DatabaseContext db)
        {
            this.db = db;
        }

        public void OnGet()
        {
            forums = db.ForumCategories.ToList();
        }
    }
}
