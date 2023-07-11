using BloggieMvc.Data;
using BloggieMvc.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace BloggieMvc.Repositories
{
    public class BlogPostRepositories : IBlogPostInterface
    {
        private readonly BloggieMvcDbContext db;
        public BlogPostRepositories(BloggieMvcDbContext db)
        {
            this.db = db;
        }

        public async Task<BlogPost> AddBlogPostAsync(BlogPost blogPost)
        {
            await db.BlogPosts.AddAsync(blogPost);
            await db.SaveChangesAsync();
            return blogPost;
        }

        public async Task<BlogPost?> DeleteBlogPostAsync(Guid id)
        {
            var existingPost = await db.BlogPosts.FindAsync(id);
            if(existingPost != null)
            {
                db.BlogPosts.Remove(existingPost);
                await db.SaveChangesAsync();
                return existingPost;
            }
            return null;
        }

        public async Task<IEnumerable<BlogPost>> GetAllBlogPostsAsync()
        {
           return await db.BlogPosts.Include(z=>z.Tags).ToListAsync();
        }

        public async Task<BlogPost?> GetBlogPostAsync(Guid id)
        {
            return await db.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<BlogPost?> GetByUrlHandleAsync(string urlHandle)
        {
            return await db.BlogPosts.Include(a => a.Tags).FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);
        }

        public async Task<BlogPost?> UpdateBlogPostAsync(BlogPost blogPost)
        {
            var existingBlogPost = await db.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(c => c.Id == blogPost.Id);
            if(existingBlogPost != null)
            {
                existingBlogPost.Id = blogPost.Id;
                existingBlogPost.Author = blogPost.Author;
                existingBlogPost.ShortDescription = blogPost.ShortDescription;
                existingBlogPost.Content = blogPost.Content;
                existingBlogPost.FeaturedImageUrl = blogPost.FeaturedImageUrl;
                existingBlogPost.Heading=blogPost.Heading;
                existingBlogPost.PageTitle = blogPost.PageTitle;
                existingBlogPost.PublishedDate  = blogPost.PublishedDate;
                existingBlogPost.UrlHandle = blogPost.UrlHandle;
                existingBlogPost.Visible= blogPost.Visible;
                existingBlogPost.Tags = blogPost.Tags;

                await db.SaveChangesAsync();
                return existingBlogPost;
            }
            return null;
        }
    }
}
