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
            disease = Db.Diseases.ToList();
        }

        public IActionResult OnPostCreate(string name, string notes)
        {
            var diseases = new Diseases
            {
                diseaseName = name,
                notes = notes,
                dateAdded = DateTime.Now,
                dateModified = DateTime.Now,
            };

            Db.Diseases.Add(diseases);
            Db.SaveChanges();
            return RedirectToPage("Index");
        }

        public IActionResult OnPostDelete(int id)
        {
            var disease = Db.Diseases.Where(x => x.diseaseID == id).FirstOrDefault();
            Db.Diseases.Remove(disease);
            Db.SaveChanges();

            return RedirectToPage("Index");
        }
    }
}
