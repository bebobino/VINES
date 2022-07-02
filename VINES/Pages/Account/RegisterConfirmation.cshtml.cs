using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using System.Threading.Tasks;
using VINES.Models;

namespace VINES.Pages.Account
{
    [AllowAnonymous]
    public class RegisterConfirmationModel : PageModel
    {

        private readonly DatabaseContext Db;

        public RegisterConfirmationModel(DatabaseContext db)
        {
            Db = db;
        }

        public string Email { get; set; }

        public async Task<IActionResult> OnGetAsync(string email)
        {
            if (email == null)
            {
                return RedirectToPage("/Index");
            }

            var user = Db.Users.Where(f => f.email == email).FirstOrDefault();
            if (user == null)
            {
                return NotFound($"Unable to load user with email '{email}'.");
            }

            Email = email;

            return Page();
        }
    }
}
