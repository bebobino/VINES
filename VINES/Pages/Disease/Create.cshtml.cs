using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VINES.Models;

namespace VINES.Pages.Disease
{
    public class CreateModel : PageModel
    {
        private readonly DatabaseContext _Db;
        public List<Diseases> diseases { get; set; }

        public CreateModel(DatabaseContext Db)
        {
            _Db = Db;
            diseases = _Db.Diseases.ToList();
        }
        [BindProperty]
        public Diseases Diseases{ get; set; }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost(int typeID, string name, string notes )
        {
            var diseases = new Diseases
            {
                diseaseID = typeID,
                diseaseName = name,
                notes = notes,
                dateAdded = DateTime.Now,
                dateModified = DateTime.Now
            };

            _Db.Diseases.Add(diseases);
            await _Db.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
