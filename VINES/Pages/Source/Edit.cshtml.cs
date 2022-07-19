using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using VINES.Models;

namespace VINES.Pages.Source
{
    public class EditModel : PageModel
    {
        private readonly DatabaseContext _context;

        public EditModel(DatabaseContext context)
        {
            _context = context;
        }

        public Sources source { get; set; } = default;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if ((id == null || _context.sources == null))
            {
                return NotFound();
            }

            var cp = await _context.sources.FirstOrDefaultAsync(m => m.sourcesID == id);
            if (cp == null)
            {
                return NotFound();
            }

            source = cp;
            return Page();

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(source).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SourcesExists(source.sourcesID))
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
        private bool SourcesExists(int id)
        {
            return (_context.sources?.Any(e => e.sourcesID == id)).GetValueOrDefault();
        }
    }
}
