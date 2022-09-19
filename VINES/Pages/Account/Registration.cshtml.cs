using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using System.ComponentModel.DataAnnotations;
using VINES.Models;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.Linq;
using VINES.Data;
using VINES.Processes;

namespace VINES.Pages.Account
{
    [AllowAnonymous]
    public class RegistrationModel : PageModel
    {
        private readonly DatabaseContext Db;

        public List<Gender> genders { get; set; }

        public RegistrationModel(DatabaseContext Db)
        {
            this.Db = Db;
            genders = Db.genders.ToList();
        }

        [BindProperty]
        public InputModel Input { get; set; }
        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

       public class InputModel
        {
            [Required]
            [Display(Name = "First Name")]
            public string firstName { get; set; }

            [Display(Name = "Middle Name")]
            public string middleName { get; set; }

            [Required]
            [Display(Name = "Last Name")]
            public string lastName { get; set; }

            [Required]
            [Display(Name = "Gender")]
            public int gender { get; set; }

            [Required]
            [Display(Name ="Date of Birth")]
            [MinimumAge(18, ErrorMessage = "You Must Be Eighteen (18) Years of Age")]
            public DateTime dateOfBirth { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string email { get; set; }

            [Required]
            [Display(Name = "Contact Nunber")]
            [RegularExpression(@"^[0-9]{11}$", ErrorMessage = "Only Numeric Inputs Allowed")]
            [DataType(DataType.PhoneNumber)]
            [StringLength(11, ErrorMessage = "Invalid Phone Number", MinimumLength = 11)]
            public string contactNumber { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 8)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm Password")]
            [Compare("password", ErrorMessage = "Your password does not match")]
            public string confirmPassword { get; set; }

            [Required]
            public DateTime dateRegistered { get; set; }
            [Required]
            public DateTime lastModified { get; set;}

            [Required]
            public int failedAttempts { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (ModelState.IsValid)
            {
                var user = Db.Users.Where(f => f.email == Input.email).FirstOrDefault();
                if (user != null)
                {
                    ModelState.AddModelError("Error", "ERROR: Email already being used.");
                }
                else
                {
                    ModelState.AddModelError("Success", "SUCCESS: User created.");
                    var help = new Help();
                    user = new User { firstName = Input.firstName, 
                        middleName = Input.middleName, 
                        lastName = Input.lastName, 
                        genderID = Input.gender, 
                        dateOfBirth = Input.dateOfBirth, 
                        email = Input.email, 
                        password = help.Hash(Input.password), 
                        contactNumber = Input.contactNumber, 
                        roleID = 3, isBlocked = false, 
                        isLocked = false, emailAuth = false, 
                        dateRegistered = DateTime.Now, 
                        lastModified = DateTime.Now, 
                        failedAttempts = 0};
                    Db.Users.Add(user);
                    await Db.SaveChangesAsync();
                    var patient = new Patients
                    {
                        userID = user.userID,
                        isSubscribed = false,
                        showAds = true,
                        subStart = DateTime.Now,
                        subEnd = DateTime.Now,

                    };
                    Db.Patients.Add(patient);
                    await Db.SaveChangesAsync();

                    help.sendEmail(Input.email, "Account Confirmation", "Here is your authentication link: "+AppSettings.Site.Url+"Account/RegisterConfirmation/?key1="+user.userID+"&key2="+help.Hash(user.email));
                    return RedirectToPage("/Account/Login");

                }
            }
            return Page();
        }
        public async Task<IActionResult> OnPostBookmark()
        {

            return Page();
        }
    }
}
