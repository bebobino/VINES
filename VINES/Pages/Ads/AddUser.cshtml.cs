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

namespace VINES.Pages.Ads
{
    [Authorize("AdminOnly")]
    public class AddUserModel : PageModel
    {
        private readonly DatabaseContext Db;

        public List<Gender> genders { get; set; }
        public List<Institutions> institutions { get; set; }

        public AddUserModel(DatabaseContext Db)
        {
            this.Db = Db;
            genders = Db.genders.ToList();
            institutions = Db.Institutions.ToList();
        }

        [BindProperty]
        public InputModel Input { get; set; }
        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

       public class InputModel
        {
            [Required]
            [Display(Name = "Institution")]
            public int institution { get; set; }
            [Required]
            [Display(Name = "First Name")]
            [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$",
            ErrorMessage = "Characters are not allowed.")]
            public string firstName { get; set; }
            [Display(Name = "Middle Name")]
            [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$",
            ErrorMessage = "Characters are not allowed.")]
            public string middleName { get; set; }
            [Required]
            [Display(Name = "Last Name")]
            [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$",
            ErrorMessage = "Characters are not allowed.")]
            public string lastName { get; set; }
            [Required]
            [Display(Name = "Gender")]
            public int gender { get; set; }

            [Required]
            [Display(Name ="Date of Birth")]
            [MinimumAge(18, ErrorMessage = "You Must Be At Least Eighteen (18) Years of Age")]
            public DateTime dateOfBirth { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string email { get; set; }

            [Required]
            [Display(Name = "Contact Nunber")]
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
            var help = new Help();

            if (ModelState.IsValid)
            {
                var user = Db.Users.Where(f => f.email == help.Encrypt(Input.email)).FirstOrDefault();
                if (user != null)
                {
                    ModelState.AddModelError("Error", "ERROR: Email already being used.");
                }
                else
                {
                    ModelState.AddModelError("Success", "SUCCESS: User created.");
                    
                    user = new User
                    {
                        firstName = help.Encrypt(Input.firstName),
                        middleName = help.Encrypt(Input.middleName),
                        lastName = help.Encrypt(Input.lastName),
                        genderID = Input.gender,
                        dateOfBirth = Input.dateOfBirth,
                        email = help.Encrypt(Input.email),
                        password = help.Encrypt(Input.password),
                        contactNumber = help.Encrypt(Input.contactNumber),
                        roleID = 2,
                        isBlocked = false,
                        isLocked = false,
                        emailAuth = false,
                        dateRegistered = DateTime.Now,
                        lastModified = DateTime.Now,
                        failedAttempts = 0
                    };
                    Db.Users.Add(user);
                    await Db.SaveChangesAsync();
                    var ad = new Advertisers
                    {
                        userID = user.userID,
                        institutionID = Input.institution,
                    };
                    Db.advertisers.Add(ad);
                    await Db.SaveChangesAsync();
                    help.sendEmail(Input.email, "Account Confirmation", "Here is your authentication link: " + AppSettings.Site.Url + "Account/RegisterConfirmation/?key1=" + user.userID + "&key2=" + user.email);
                    return Page();
                }
            }
            return Page();
        }

    }
}
