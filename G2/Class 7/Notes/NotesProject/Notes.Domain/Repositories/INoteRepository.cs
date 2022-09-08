using Notes.Domain.Entities;

namespace Notes.Domain.Repositories
{
    public interface INoteRepository
    {
        Task<IReadOnlyList<Note>> GetAllNotesAsync();

        Task<Note> GetNoteAsync(int id);

        Task InsertNoteAsync(Note newNote);

        Task UpdateNoteAsync(Note existingNote);

        Task DeleteNoteAsync(int id);
    }
}
