using System;
using System.ComponentModel.DataAnnotations;

namespace VINES.Data
{
    public class User
    {
        [Key]
        [Required]
        public int userID { get; set; }
        [Required]
        public int roleID { get; set; }
        [Required]
        public string firstName { get; set; }
        [Required]
        public string middleName { get; set; }
        [Required]
        public string lastName { get; set; }
        [Required]
        public string contactNumber { get; set; }
        [Required]
        public int genderID { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public DateTime dateOfBirth { get; set; }
        [Required]
        public bool emailAuth { get; set; }
        [Required]
        public bool isBlocked { get; set; }
        [Required]
        public bool isLocked { get; set; }

        [Required]
        public DateTime dateRegistered { get; set; }
        [Required]
        public DateTime lastModified { get; set; }

        [Required]
        public int failedAttempts { get; set; }
    
    }
}
