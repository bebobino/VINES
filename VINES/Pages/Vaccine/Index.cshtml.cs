using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using VINES.Models;

namespace VINES.Pages.Vaccine
{
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

        public IActionResult OnPostCreate(int ID, string name, string notes)
        {
            var vaccines = new Vaccines
            {
                diseaseID = ID,
                vaccineName = name,
                notes = notes,
                dateAdded = DateTime.Now,
                dateModified = DateTime.Now
            };

            Db.vaccines.Add(vaccines);
            Db.SaveChanges();
            return RedirectToPage("Index");
        }

        public IActionResult OnPostDelete(int id)
        {
            var vaccine = Db.vaccines.Where(x => x.vaccineID == id).FirstOrDefault();
            Db.vaccines.Remove(vaccine);
            Db.SaveChanges();

            return RedirectToPage("Index");
        }
    }
}
