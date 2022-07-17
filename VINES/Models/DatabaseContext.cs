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
        public DbSet<Vaccines> Vaccines { get; set; }
        public DbSet<Institutions> Institutions { get; set; }
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

    }
}
