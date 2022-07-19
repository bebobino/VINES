using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VINES.Models;

namespace VINES.Pages.InstitutionVaccine
{
    public class EditModel : PageModel
    {
        
        public List<Diseases> disease { get; set; }
        public List<Institutions> institution { get; set; }
        public List<Vaccines> vaccines { get; set; }

        private readonly DatabaseContext _context;
        public EditModel(DatabaseContext context)
        {
            _context = context;
        }

        [BindProperty]
        public InstitutionVaccines institutions { get; set; } = default;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if ((id == null || _context.InstitutionVaccines == null))
            {
                return NotFound();
            }

            var cp = await _context.InstitutionVaccines.FirstOrDefaultAsync(m => m.institutionVaccineID == id);
            if (cp == null)
            {
                return NotFound();
            }

            institutions = cp;
            return Page();

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {

                return Page();
            }

            _context.Attach(institutions).State = EntityState.Modified;



            try
            {
                institutions.lastModified = DateTime.Now;
                institutions.dateAdded = DateTime.Now;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InstitutionsExists(institutions.institutionVaccineID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("Index");
        }
        private bool InstitutionsExists(int id)
        {
            return (_context.InstitutionVaccines?.Any(e => e.institutionVaccineID == id)).GetValueOrDefault();
        }
    }
}
