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
        public DbSet<ForumPost> ForumPosts { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<CommunityPostCategoriesModel> CommunityPostCategories { get; set; }

        public DbSet<Gender> genders { get; set; }
    }
}
