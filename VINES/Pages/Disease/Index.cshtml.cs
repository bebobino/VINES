using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using VINES.Models;

namespace VINES.Pages.Disease
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public List<Diseases> diseases { get; set; }
        private readonly DatabaseContext Db;

        public IndexModel(DatabaseContext _Db)
        {
            Db = _Db;
        }

        public void OnGet()
        {
            diseases = Db.diseases.ToList();
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

            Db.diseases.Add(diseases);
            Db.SaveChanges();
            return RedirectToPage("Index");
        }

        public IActionResult OnPostDelete(int id)
        {
            var diseases = Db.Diseases.Where(x => x.diseaseID == id).FirstOrDefault();
            Db.Diseases.Remove(diseases);
            Db.SaveChanges();

            return RedirectToPage("Index");
        }
    }
}
