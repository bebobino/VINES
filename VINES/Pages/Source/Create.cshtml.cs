using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using VINES.Models;

namespace VINES.Pages.Source
{
    [Authorize("AdminOnly")]
    public class CreateModel : PageModel
    {
        private readonly DatabaseContext _Db;

        public CreateModel(DatabaseContext Db)
        {
            _Db = Db;
        }

        public Sources sources { get; set; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost(string name, string uri)
        {
            var sources = new Sources
            {
                sourceName = name,
                sourcesURI = uri
            };

            _Db.sources.Add(sources);
            await _Db.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
