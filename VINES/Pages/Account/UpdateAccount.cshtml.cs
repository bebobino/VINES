using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using VINES.Data;
using VINES.Models;

namespace VINES.Pages.Account
{
    [Authorize]
    public class UpdateAccountModel : PageModel
    {
        private readonly DatabaseContext Db;

        public List<User> users { get; set; }

        public UpdateAccountModel(DatabaseContext db)
        {
            users = new List<User>();
            Db = db;
        }


        public void OnGet()
        {
        }

    }
}
