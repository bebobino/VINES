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
        private readonly DatabaseContext _Db;
        public List<Diseases> disease { get; set; }
        public List<Vaccines> vaccines { get; set; }

        public CreateModel(DatabaseContext Db)
        {
            _Db = Db;
            disease = _Db.Diseases.ToList();
            vaccines = _Db.vaccines.ToList();
        }
        [BindProperty]
        public Vaccines Vaccines { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(int dID, int vID, string vName, string notes )
        {
            var vaccines = new Vaccines
            {
                diseaseID = dID,
                vaccineID = vID,
                vaccineName = vName,
                notes = notes,
                dateAdded = DateTime.Now,
                dateModified = DateTime.Now
            };

            _Db.vaccines.Add(Vaccines);
            await _Db.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
