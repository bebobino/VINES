using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using VINES.Models;

namespace VINES.Pages.Institution
{
    public class CreateModel : PageModel
    {
        private readonly DatabaseContext _Db;
        public List<InstitutionTypes> institutionTypes { get; set; }

        public CreateModel(DatabaseContext Db)
        {
            _Db = Db;
            institutionTypes = _Db.InstitutionTypes.ToList();
        }
        [BindProperty]
        public InputModel Input { get; set; }
        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            public int institutionTypeID { get; set; }
            [Required]
            [Display(Name = "Institution Name")]
            [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$",
            ErrorMessage = "Characters are not allowed.")]
            public string institutionName { get; set; }
            [Required]
            [Display(Name = "Longitude")]
            public decimal Long { get; set; }
            [Required]
            [Display(Name = "Latitude")]
            public decimal lat { get; set; }

            [Display(Name = "Notes")]
            [RegularExpression(@"^[a-zA-Z0-9 -#]*$", ErrorMessage = "The name contains invalid characters.")]
            public string notes { get; set; }
            [Required]
            public DateTime dateAdded { get; set; }
            [Required]
            public DateTime lastModified { get; set; }
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
                var insti = _Db.Institutions.Where(f => f.institutionName == Input.institutionName).FirstOrDefault();
                if (insti != null)
                {
                    ModelState.AddModelError("Error", "Already in Database");
                }
                else
                {
                    ModelState.AddModelError("Success", "Institution Has Been Added");
                    insti = new Institutions
                    {
                        institutionTypeID = Input.institutionTypeID,
                        institutionName = Input.institutionName,
                        Long = Input.Long,
                        lat = Input.lat,
                        notes = Input.notes,
                        dateAdded = DateTime.Now,
                        lastModified = DateTime.Now,
                    };

                    _Db.Institutions.Add(insti);
                    await _Db.SaveChangesAsync();
                    return RedirectToPage("/Institution/Index");
                }
            }

            return Page();
        }
    }
}
