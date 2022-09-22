using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VINES.Models;

namespace VINES.Pages.Vaccine
{
    [Authorize("AdminOnly")]
    public class IndexModel : PageModel
    {
        public List<Vaccines> vaccines { get; set; }
        public List<Diseases> diseases { get; set; }
        private readonly DatabaseContext Db;

        public IndexModel(DatabaseContext _Db)
        {
            Db = _Db;
        }

        public void OnGet()
        {
            vaccines = Db.vaccines.ToList();
            diseases = Db.diseases.ToList();
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var com = await Db.vaccines.FindAsync(id);
            if (com == null)
            {
                return NotFound();

            }
            Db.vaccines.Remove(com);
            await Db.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}



