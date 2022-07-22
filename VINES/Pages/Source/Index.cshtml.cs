using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using VINES.Models;

namespace VINES.Pages.Source
{
    [Authorize("AdminOnly")]
    public class IndexModel : PageModel
    {
        private readonly DatabaseContext _db;

        public IndexModel(DatabaseContext db)
        {
            _db = db;
        }

        public List<Sources> source { get; set; }

        public async Task OnGet()
        {
            source = await _db.sources.ToListAsync();
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var com = await _db.sources.FindAsync(id);
            if (com == null)
            {
                return NotFound();
            }

            _db.sources.Remove(com);
            await _db.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
