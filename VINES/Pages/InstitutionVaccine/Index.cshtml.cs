using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using VINES.Models;

namespace VINES.Pages.InstitutionVaccine
{
    public class IndexModel : PageModel
    {
        private readonly DatabaseContext _db;

        public IndexModel(DatabaseContext db)
        {
            _db = db;
        }

        public List<InstitutionVaccines> institutionVaccines { get; set; }
        public List<Diseases> disease { get; set; }
        public List<Vaccines> vaccines { get; set; }

        public List<Institutions> institutions { get; set; }
        public async Task OnGet()
        {
            institutionVaccines = await _db.InstitutionVaccines.ToListAsync();
            institutions = await _db.Institutions.ToListAsync();
            disease = await _db.diseases.ToListAsync();
            vaccines = await _db.vaccines.ToListAsync();
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var com = await _db.InstitutionVaccines.FindAsync(id);
            if (com == null)
            {
                return NotFound();

            }
            _db.InstitutionVaccines.Remove(com);
            await _db.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
