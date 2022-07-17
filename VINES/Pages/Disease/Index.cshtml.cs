using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using VINES.Models;

namespace VINES.Pages.Disease
{
    public class IndexModel : PageModel
    {
        public List<Diseases> disease { get; set; }
        private readonly DatabaseContext Db;

        public IndexModel(DatabaseContext _Db)
        {
            Db = _Db;
        }

        public void OnGet()
        {
            disease = Db.disease.ToList();
        }

        public IActionResult OnPostCreate(string name, string notes)
        {
            var diseases = new Diseases
            {
                diseaseName = name,
                notes = notes,
                dateAdded = DateTime.Now,
                lastModified = DateTime.Now,
            };

            Db.disease.Add(diseases);
            Db.SaveChanges();
            return RedirectToPage("Index");
        }
    }
}
