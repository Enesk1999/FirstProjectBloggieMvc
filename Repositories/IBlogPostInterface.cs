using BloggieMvc.Models.Domain;
using BloggieMvc.Models.ViewModels;

namespace BloggieMvc.Repositories
{
    public interface IBlogPostInterface
    {
        Task<IEnumerable<BlogPost>> GetAllBlogPostsAsync();
        Task<BlogPost?> GetBlogPostAsync(Guid id);
        Task<BlogPost> AddBlogPostAsync(BlogPost blogPost);
        Task<BlogPost?> UpdateBlogPostAsync(BlogPost blogPost);
        Task<BlogPost?> DeleteBlogPostAsync(Guid id);

        Task<BlogPost?> GetByUrlHandleAsync(string urlHandle);
    }
}
