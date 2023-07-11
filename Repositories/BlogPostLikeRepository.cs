using BloggieMvc.Data;
using BloggieMvc.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BloggieMvc.Repositories
{
    public class BlogPostLikeRepository : IBlogPostLikeRepository
    {
        private readonly BloggieMvcDbContext bloggieMvcDbContext;

        public BlogPostLikeRepository(BloggieMvcDbContext bloggieMvcDbContext)
        {
            this.bloggieMvcDbContext = bloggieMvcDbContext;
        }

        public async Task<BlogPostLike> AddLikeForBlog(BlogPostLike blogPostLike)
        {
            await bloggieMvcDbContext.BlogPostLike.AddAsync(blogPostLike);
            await bloggieMvcDbContext.SaveChangesAsync();
            return blogPostLike;
        }

        public async Task<IEnumerable<BlogPostLike>> GetLikesForBlog(Guid blogPostId)
        {
            return await bloggieMvcDbContext.BlogPostLike.Where(x => x.BlogPostId == blogPostId).ToListAsync();
        }

        public async Task<int> GetTotalLikes(Guid blogPostId)
        {
          return  await bloggieMvcDbContext.BlogPostLike.CountAsync(x => x.BlogPostId == blogPostId);
        }
    }
}
