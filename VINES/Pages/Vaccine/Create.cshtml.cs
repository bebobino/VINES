using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
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
        public InputModel Input { get; set; }
        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            public int diseaseID { get; set; }

            [Required(ErrorMessage = "This field is required")]
            [Display(Name = "Vaccine Name")]
            [RegularExpression(@"^[a-zA-Z0-9 -#]*$", ErrorMessage = "The name contains invalid characters.")]
            public string vaccineName { get; set; }

            [Display(Name = "Notes")]
            [RegularExpression(@"^[a-zA-Z0-9 -#]*$", ErrorMessage = "The input contains invalid characters.")]
            public string notes { get; set; }

            [Required]
            public DateTime dateAdded { get; set; }

            [Required]
            public DateTime dateModified { get; set; }
        };

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (ModelState.IsValid)
            {
                //This is a test. Must check to see if vaccine name is existing per disease
                //Need to change this.
                var vax = db.vaccines.Where(f => f.vaccineName == Input.vaccineName).FirstOrDefault();
                if (vax != null)
                {
                    ModelState.AddModelError("Error", "Vaccine is already in the Database");
                }
                else
                {
                    ModelState.AddModelError("Success", "Vaccine Has Been Added.");

                    vax = new Vaccines
                    {
                        diseaseID = Input.diseaseID,
                        vaccineName = Input.vaccineName,
                        notes = Input.notes,
                        dateAdded = DateTime.Now,
                        dateModified = DateTime.Now
                    };
                    db.vaccines.Add(vax);
                    await db.SaveChangesAsync();

                    return RedirectToPage("/Vaccine/Index");
                }
            }

            return Page();
        }
    }
}


