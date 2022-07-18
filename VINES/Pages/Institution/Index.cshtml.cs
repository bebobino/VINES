using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using VINES.Models;

namespace VINES.Pages.Institution
{
    public class IndexModel : PageModel
    {
        private readonly DatabaseContext _db;

        public IndexModel(DatabaseContext db)
        {
            _db = db;
        }

        public List<Institutions> institutions { get; set; }
        public List<InstitutionTypes> institutionTypes { get; set; }
        public async Task OnGet()
        {
            institutions = await _db.Institutions.ToListAsync();
            institutionTypes = await _db.InstitutionTypes.ToListAsync();
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var com = await _db.Institutions.FindAsync(id);
            if (com == null)
            {
                return NotFound();

            }
            _db.Institutions.Remove(com);
            await _db.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
