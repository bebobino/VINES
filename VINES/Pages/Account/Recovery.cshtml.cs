using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using VINES.Models;
using VINES.Processes;
using VINES.Data;
using System.Linq;
using System;
using System.Threading.Tasks;

namespace VINES.Pages.Account
{
    public class RecoveryModel : PageModel
    {
        private readonly DatabaseContext db;
        public RecoveryModel(DatabaseContext _db)
        {
            db = _db;
        }
        [BindProperty]
        public InputModel input { get; set; }
        public User user { get; set; }
        public bool G { get; set; } = false;
        Help help = new Help();
        public class InputModel
        {
            [EmailAddress]
            [Required]
            public string email { get; set; }
            [Required]
            [DataType(DataType.Password)]
            public string password { get; set; }
        }
        
        public void OnGet(int key1, string key2, string key3)
        {
            
            try { 
            user = db.Users.Find(key1);
            }
            catch(Exception e)
            {
                user = new User();
            }
            if (user == null)
            {
                Debug.WriteLine("Walang account");
            }
            else{
                var email = help.Encrypt(user.email);
                var password = help.Encrypt(user.password);
                key2 = key2.Replace(" ", "+");
                key3 = key3.Replace(" ", "+");
                Debug.WriteLine(help.Hash(user.email)+"     ,     "+key2);
                Debug.WriteLine(help.Hash(user.password) + "     ,     " + key3);

                if (email.Equals(key2) && password.Equals(key3))
                {
                    G = true;
                }
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var _user = db.Users.Where(f => f.email == input.email).FirstOrDefault();
                _user.password = help.Encrypt(input.password);
                _user.isLocked = false;
                _user.failedAttempts = 0;
                _user.lastModified = DateTime.Now;
                await db.SaveChangesAsync();
                Debug.WriteLine(_user.email);
            }
            return Page();
        }
    }
}
