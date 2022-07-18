using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public Institutions institutions { get; set; }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost(int typeID, string name, decimal Long, decimal lat, string notes )
        {
            var institutions = new Institutions
            {
                institutionTypeID = typeID,
                institutionName = name,
                Long = Long,
                lat = lat,
                notes = notes,
                dateAdded = DateTime.Now,
                lastModified = DateTime.Now
            };

            _Db.Institutions.Add(institutions);
            await _Db.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
