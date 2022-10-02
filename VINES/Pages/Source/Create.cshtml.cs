using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Xml.Linq;
using VINES.Models;

namespace VINES.Pages.Source
{
    [Authorize("AdminOnly")]
    public class CreateModel : PageModel
    {
        private readonly DatabaseContext _Db;

        public CreateModel(DatabaseContext Db)
        {
            _Db = Db;
        }

        [BindProperty]
        public InputModel Input { get; set; }
        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "Source Name")]
            [RegularExpression(@"^[a-zA-Z0-9 -#]*$", ErrorMessage = "The name contains invalid characters.")]
            public string sourceName { get; set; }
            [Required]
            [Display(Name = "Source URI")]
            [RegularExpression(@"^[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&//=]*)$", ErrorMessage = "Invalid URI")]
            public string sourcesURI { get; set; }

        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl)
        {

            returnUrl ??= Url.Content("~/");

            if (ModelState.IsValid)
            {
                var source = _Db.sources.Where(f => f.sourceName == Input.sourceName).FirstOrDefault();
                if (source != null)
                {
                    ModelState.AddModelError("Error", "Source is already in the database");
                }
                else
                {
                    ModelState.AddModelError("Success", "Source has been added");
                    source = new Sources
                    {
                        sourceName = Input.sourceName,
                        sourcesURI = Input.sourcesURI
                    };

                    _Db.sources.Add(source);
                    await _Db.SaveChangesAsync();

                    return RedirectToPage("/Source/Index");
                }
            }

            return Page();
        }
    }
}
