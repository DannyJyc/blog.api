using Microsoft.EntityFrameworkCore;
using Blog.API.Entity.Fluents;
using Blog.API.Entity.Models;

namespace Blog.API.Entity
{
    public class EFCoreContext : DbContext
    {
        public EFCoreContext(DbContextOptions<EFCoreContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _ = new UserFluenter(modelBuilder.Entity<User>());
            _ = new BlogFluenter(modelBuilder.Entity<BlogModel>());
            _ = new TagFluenter(modelBuilder.Entity<Tag>());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<BlogModel> Blogs { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Blog_Tag> Blog_Tags { get; set; }
    }
}
