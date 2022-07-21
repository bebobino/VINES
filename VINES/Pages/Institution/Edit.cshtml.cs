using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VINES.Models;

namespace VINES.Pages.Institution
{
    public class EditModel : PageModel
    {
        private readonly DatabaseContext _context;

        public List<InstitutionTypes> types { get; set; }
        public EditModel(DatabaseContext context)
        {
            _context = context;
            types = _context.InstitutionTypes.ToList();
        }

        [BindProperty]
        public Institutions institutions { get; set; } = default;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if ((id == null || _context.Institutions == null))
            {
                return NotFound();
            }

            var cp = await _context.Institutions.FirstOrDefaultAsync(m => m.institutionID == id);
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
                if (!InstitutionsExists(institutions.institutionID))
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
            return (_context.Institutions?.Any(e => e.institutionID == id)).GetValueOrDefault();
        }
    }
}
