using Microsoft.EntityFrameworkCore;
using VINES.Data;

namespace VINES.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public DbSet<CommunityPost> CommunityPosts { get; set; }
        public DbSet<Posts> Post { get; set; }
        public DbSet<WebPages> WebPages { get; set; }
        public DbSet<Vaccines> vaccines { get; set; }
        public DbSet<Diseases> Diseases { get; set; }
        public DbSet<InstitutionVaccines> InstitutionVaccines { get; set; }
        public DbSet<ForumCategory> ForumCategories { get; set; }
        public DbSet<ForumComment> ForumComments { get; set; }
        public DbSet<ForumPost> ForumPosts { get; set; }
        public DbSet<InstitutionTypes> InstitutionTypes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<CommunityPostCategoriesModel> CommunityPostCategories { get; set; }
        public DbSet<Gender> genders { get; set; }
        public DbSet<Patients> Patients { get; set; }

        public DbSet<Diseases> diseases { get; set; }

        public DbSet<Advertisement> advertisements { get; set; }

        public DbSet<AdvertisementType> advertisementTypes { get; set; }
        public DbSet<Advertisers> advertisers {get;set;}
        public DbSet<AuditCategories> auditCategories { get; set; }
        public DbSet<Bookmarks> bookmarks { get; set; }
        public DbSet<IPAddresses> iPAddresses { get; set; }
        public DbSet<PatientPreferences> patientPreferences { get; set; }
        public DbSet<Payment> payments { get; set; }
        public DbSet<RecommendedInstitutions> recommendedInstitutions { get; set; }
        public DbSet<Roles> roles { get; set; }
        public DbSet<VaccinePreference> vaccinePreferences { get; set; }

        public DbSet<Institutions> Institutions { get; set; }


    }
}
