using Microsoft.EntityFrameworkCore;
using Notes.Domain.Entities;
using Notes.Domain.Repositories;
using Notes.Storage.Database;

namespace Notes.Storage.Repositories
{
    public class TagRepository : RepositoryBase<Tag>, ITagRepository
    {
        public TagRepository(INotesDbContext notesDbContext) : base(notesDbContext)
        {

        }

        public async Task DeleteTagAsync(int id)
        {
            DeleteEntity(id);
        }

        public async Task<IReadOnlyList<Tag>> GetAllTagsAsync()
        {
            IQueryable<Tag> tagQuery = GetAll();

            return await tagQuery.ToArrayAsync();
        }

        public async Task<Tag> GetTagAsync(int id)
        {
            return await GetById(id).SingleOrDefaultAsync();
        }

        public async Task InsertTagAsync(Tag newTag)
        {
            InsertEntity(newTag);
        }

        public async Task UpdateTagAsync(Tag existingTag)
        {
            UpdateEntity(existingTag);
        }
    }
}
