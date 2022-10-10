using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VINES.Models;

namespace VINES.Pages.InstitutionVaccine
{
    public class CreateModel : PageModel
    {
        private readonly DatabaseContext _Db;
        public List<Institutions> institutions { get; set; }
        public List<Diseases> disease { get; set; }
        public List<Vaccines> vaccines { get; set; }

        public CreateModel(DatabaseContext Db)
        {
            _Db = Db;
            institutions = _Db.Institutions.ToList();
            disease = _Db.Diseases.ToList();
            vaccines = _Db.vaccines.ToList();
        }
        [BindProperty]
        public InstitutionVaccines institutionVaccines { get; set; }
        public string ReturnUrl { get; set; }

        public void OnGetAsync(string returnUrl)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPost(int instiID, int dID, int vID, decimal price, string notes, string returnUrl = null)
        {
            ReturnUrl ??= Url.Content("~/");

            if (ModelState.IsValid)
            {
                var institutionVax = new InstitutionVaccines
                {
                    institutionID = instiID,
                    diseaseID = dID,
                    vaccineID = vID,
                    price = price,
                    notes = notes,
                    dateAdded = DateTime.Now,
                    lastModified = DateTime.Now
                };

                _Db.InstitutionVaccines.Add(institutionVax);
                await _Db.SaveChangesAsync();
                return RedirectToPage("Index");
            }
            else
            {
             
            }

            return Page();

        }
    }
}