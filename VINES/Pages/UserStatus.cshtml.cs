using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using VINES.Data;
using VINES.Models;

namespace VINES.Pages
{
    public class UserStatusModel : PageModel
    {
        private readonly DatabaseContext Db;

        public List<User> Users = new List<User>();
        public UserStatusModel(DatabaseContext _Db)
        {

            Db = _Db;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Users = await Db.Users.ToListAsync();

            return Page();
        }
    }
}
