using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VINES.Models;

namespace VINES.Pages.Vaccine
{
    public class CreateModel : PageModel
    {
        private readonly DatabaseContext db;
        public List<Diseases> diseases { get; set; }

        public CreateModel(DatabaseContext _db)
        {
            db = _db;
            diseases = _db.diseases.ToList();
        }

        [BindProperty]
        public Vaccines vaccines { get; set; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost(int dId, string name, string notes)
        {
            var vaccines = new Vaccines
            {
                diseaseID = dId,
                vaccineName = name,
                notes = notes,
                dateAdded = DateTime.Now,
                dateModified = DateTime.Now
            };

            db.vaccines.Add(vaccines);
            await db.SaveChangesAsync();
            return RedirectToPage("Index");
        }

    }
}



