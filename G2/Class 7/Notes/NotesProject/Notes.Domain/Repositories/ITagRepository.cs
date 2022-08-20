using Notes.Domain.Entities;

namespace Notes.Domain.Repositories
{
    public interface ITagRepository
    {
        Task<IReadOnlyList<Tag>> GetAllTagsAsync();

        Task<Tag> GetTagAsync(int id);

        Task InsertTagAsync(Tag newTag);

        Task UpdateTagAsync(Tag existingTag);

        Task DeleteTagAsync(int id);
    }
}
