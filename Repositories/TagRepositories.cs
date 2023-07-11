using BloggieMvc.Data;
using BloggieMvc.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BloggieMvc.Repositories
{
    public class TagRepositories : ITagInterface
    {
        private readonly BloggieMvcDbContext db;
        public TagRepositories(BloggieMvcDbContext mvcDbContext)
        {
            db = mvcDbContext;
        }
        public async Task<Tag> AddTagAsync(Tag tag)
        {
            var add = new Tag
            {
                Id = tag.Id,
                Name = tag.Name,
                DisplayName = tag.DisplayName
            };
            if (add != null)
            {
                await db.Tags.AddAsync(add);
                await db.SaveChangesAsync();
            }
            return add;
        }

        public async Task<Tag?> DeleteTagAsync(Guid id)
        {
            var findTag = await db.Tags.FindAsync(id);
            if (findTag != null)
            {
                db.Tags.Remove(findTag);
                await db.SaveChangesAsync();
                return findTag;
            }
            return null;
        }

        public async Task<IEnumerable<Tag>> GetAllTagAsync()
        {
            return await db.Tags.ToListAsync();
            
        }

        public async Task<Tag?> GetSingleTagAsync(Guid id)
        {
           return await db.Tags.FirstOrDefaultAsync(t => t.Id==id);
            
        }

        public async Task<Tag?> UpdateTagAsync(Tag tag)
        {
            var existingTag = await db.Tags.FindAsync(tag.Id);
            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;
                await db.SaveChangesAsync();
                return existingTag;
            }
            return null;
        }
    }
}
