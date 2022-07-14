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

        public DbSet<User> Users { get; set; }
        public DbSet<CommunityPostCategories> CommunityPostCategories { get; set; } 
        public DbSet<WebPage> WebPages { get; set; }
        public DbSet<Post> Posts { get; set; }
    }
}
