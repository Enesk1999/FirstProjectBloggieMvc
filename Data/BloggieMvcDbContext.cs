using BloggieMvc.Models.Domain;
using Microsoft.EntityFrameworkCore;
namespace BloggieMvc.Data
{
    public class BloggieMvcDbContext:DbContext
    {
        public BloggieMvcDbContext(DbContextOptions<BloggieMvcDbContext> options) : base(options) 
        {
        
        }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<BlogPostLike> BlogPostLike { get; set; }
    }
}
