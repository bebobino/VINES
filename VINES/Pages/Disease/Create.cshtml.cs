using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public InputModel Input { get; set; }
        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "Disease Name")]
            [RegularExpression(@"^[a-zA-Z0-9 -#]*$", ErrorMessage = "The name contains invalid characters.")]
            public string diseaseName { get; set; }

            [Display(Name = "Notes")]
            [RegularExpression(@"^[a-zA-Z0-9 -#]*$", ErrorMessage = "The name contains invalid characters.")]
            public string notes { get; set; }
            [Required]
            public DateTime dateAdded { get; set; }
            [Required]
            public DateTime dateModified { get; set; }
        }

        public async Task OnGetAsync(string returnUrl)
        {
            ReturnUrl = returnUrl;
        }
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            ReturnUrl ??= Url.Content("~/");

            if (ModelState.IsValid)
            {
                var disease = _Db.diseases.Where(d => d.diseaseName == Input.diseaseName).FirstOrDefault();
                if (disease != null)
                {
                    ModelState.AddModelError("Error", "Already in Database.");
                }
                else
                {
                    ModelState.AddModelError("Success", "Disease has been successfully added.");
                    disease = new Diseases
                    {
                        diseaseName = Input.diseaseName,
                        notes = Input.notes,
                        dateAdded = DateTime.Now,
                        dateModified = DateTime.Now
                    };


                    _Db.Diseases.Add(disease);
                    await _Db.SaveChangesAsync();
                    return RedirectToPage("Index");
                }
            }

            return Page();
        }
    }
}
