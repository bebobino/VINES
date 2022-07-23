using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using VINES.Data;
using VINES.Models;
namespace VINES.Pages
{
    [Authorize("AdminOnly")]
    public class adminLandingModel : PageModel
    {
        public List<Vaccines> vaccines { get; set; }
        public List<Sources> sources { get; set; }
        public List<Diseases> diseases { get; set; }
        public List<Diseases> disease { get; set; }
        public List<Institutions> institutions { get; set; }
        public List<InstitutionTypes> institutionTypes { get; set; }

        public List<User> Users { get; set; }

        private DatabaseContext db;

        public adminLandingModel(DatabaseContext _db)
        {
            db = _db;
        }

        public void OnGet()
        {
            vaccines = db.vaccines.ToList();
            diseases = db.diseases.ToList();
            institutions = db.Institutions.ToList();
            institutionTypes = db.InstitutionTypes.ToList();
            disease = db.Diseases.ToList();
            sources = db.sources.ToList();
        }
       
    }
}
