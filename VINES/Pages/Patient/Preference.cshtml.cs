using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VINES.Models;

namespace VINES.Pages.Patient
{
    [Authorize("PatientOnly")]
    public class Preference : PageModel
    {
        private readonly DatabaseContext _Db;
        public List<CommunityPostCategoriesModel> categories { get; set; }
       
        public List<InstitutionVaccines> inst { get; set; }
        public List<Vaccines> vac { get; set; }
        public List<Institutions> i { get; set; }
        public List<InstitutionTypes> ii { get; set; }
        public Preference(DatabaseContext Db)
        {
            _Db = Db;
        }
        public void OnGet(int vax, decimal budget)
        {
            inst = _Db.InstitutionVaccines.Where(v => v.vaccineID == vax && v.price <= budget).ToList();
            i = _Db.Institutions.ToList();
            ii = _Db.InstitutionTypes.ToList();
            vac = _Db.vaccines.ToList();
        }

        
    }
}
