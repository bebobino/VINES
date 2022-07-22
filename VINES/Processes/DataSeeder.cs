using System.Collections.Generic;
using System.Linq;
using VINES.Data;
using VINES.Models;

namespace VINES.Processes
{
    public class DataSeeder
    {
        private readonly DatabaseContext Db;

        public DataSeeder(DatabaseContext _Db)
        {
            Db = _Db;
        }

        public void Seed()
        {
            if(!Db.Users.Any())
            {
                var users = new List<User>()
                {
                    new User()
                    {
                        userID = 1,
                        roleID = 1,
                        firstName = "User",
                        lastName = "Admin",
                        genderID = 1,
                        dateOfBirth = System.DateTime.Now,
                        contactNumber = "01234567890",
                        email = "admin@admin.com",
                        password = "adminAdmin",
                        emailAuth = true,
                        isBlocked = false,
                        isLocked = false,
                        failedAttempts = 0,
                        lastModified = System.DateTime.Now,
                        dateRegistered = System.DateTime.Now


                    },

                new User()
                    {
                        userID = 2,
                        roleID = 2,
                        firstName = "User",
                        lastName = "Advertiser",
                        genderID = 1,
                        dateOfBirth = System.DateTime.Now,
                        contactNumber = "01234567890",
                        email = "advert@advert.com",
                        password = "advertAdvert",
                        emailAuth = true,
                        isBlocked = false,
                        isLocked = false,
                        failedAttempts = 0,
                        lastModified = System.DateTime.Now,
                        dateRegistered = System.DateTime.Now


                    },

                 new User()
                    {
                        userID = 3,
                        roleID = 3,
                        firstName = "User",
                        lastName = "Patient",
                        genderID = 1,
                        dateOfBirth = System.DateTime.Now,
                        contactNumber = "01234567890",
                        email = "patient@patient.com",
                        password = "patientPatient",
                        emailAuth = true,
                        isBlocked = false,
                        isLocked = false,
                        failedAttempts = 0,
                        lastModified = System.DateTime.Now,
                        dateRegistered = System.DateTime.Now


                    },
                };

                Db.Users.AddRange(users);
                Db.SaveChanges();
            }

            if(!Db.CommunityPostCategories.Any())
            {
                var category = new List<CommunityPostCategoriesModel>()
                {
                    new CommunityPostCategoriesModel()
                    {
                        communityPostCategoryID = 1,
                        communityPostCategory = "System Update"
                    },

                    new CommunityPostCategoriesModel()
                    {
                        communityPostCategoryID = 2,
                        communityPostCategory = "Community Update"
                    },
                };
                Db.CommunityPostCategories.AddRange(category);
                Db.SaveChanges();
            }

            if(!Db.genders.Any())
            {
                var gender = new List<Gender>()
                {
                    new Gender()
                    {
                        genderID = 1,
                        gender = "Male"
                    },

                    new Gender()
                    {
                        genderID = 2,
                        gender = "Female"
                    },

                    new Gender()
                    {
                        genderID = 3,
                        gender = "Other"
                    },
                };

                Db.genders.AddRange(gender);
                Db.SaveChanges();
            }
        }
    }
}
