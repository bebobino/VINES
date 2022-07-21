using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using VINES.Data;
using VINES.Models;
using VINES.Processes;

namespace VINES.Pages.UpdateAccount
{
    public class ChangePasswordModel : PageModel
    {
        public DatabaseContext Db;

        public ChangePasswordModel(DatabaseContext _db)
        {
            Db = _db;
        }
        public User user { get; set; }
        [BindProperty]
        public InputModel input { get; set; }

        Help help = new Help();
        public class InputModel
        {
            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Old Password")]
            public string password { get; set; }
            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "New Password")]
            public string newPassword { get; set; }
            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Confirm Password")]
            [Compare("newPassword", ErrorMessage = "Your Password does not match")]
            public string confirmPassword { get; set; }
        }

        public void OnGet()
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int idd = int.Parse(id);
            user = Db.Users.Where(x => x.userID == idd).FirstOrDefault();
        }

        public void OnPost()
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int idd = int.Parse(id);
            var _user = Db.Users.Find(idd);

            if (_user.password.Equals(help.Hash(input.password)))
            {
                //okay to
                if (input.newPassword.Equals(input.confirmPassword))
                {
                    _user.password = help.Hash(input.newPassword);
                    Db.SaveChanges();

                }
                else
                {
                    ModelState.AddModelError("Error", "Your password does not match");
                }
            }
            else
            {
                ModelState.AddModelError("Error", "Incorrect Password");
            }
        }
    }
}
