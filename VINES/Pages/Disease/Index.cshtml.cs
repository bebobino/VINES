using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using VINES.Models;

namespace VINES.Pages.Disease

{
    public class IndexModel : PageModel
    {
        private readonly DatabaseContext _db;

        public IndexModel(DatabaseContext db)
        {
            _db = db;
        }

        public List<Diseases> diseases { get; set; }
 
        public async Task OnGet()
        {
            diseases = await _db.Diseases.ToListAsync();
         
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var com = await _db.Diseases.FindAsync(id);
            if (com == null)
            {
                return NotFound();

            }
            _db.Diseases.Remove(com);
            await _db.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
