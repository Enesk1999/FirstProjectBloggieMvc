using BloggieMvc.Models.Domain;

namespace BloggieMvc.Repositories
{
    public interface ITagInterface
    {
        Task<IEnumerable<Tag>> GetAllTagAsync();
        Task<Tag?> GetSingleTagAsync(Guid id);
        Task<Tag> AddTagAsync(Tag tag);
        Task<Tag?> DeleteTagAsync(Guid id);
        Task<Tag?> UpdateTagAsync(Tag tag);
    }
}
