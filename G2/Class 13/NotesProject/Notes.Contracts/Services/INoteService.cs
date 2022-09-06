using Notes.Contracts.DTOs;

namespace Notes.Contracts.Services
{
    public interface INoteService
    {
        Task<IReadOnlyList<NoteDto>> GetAllNotesAsync();

        Task<NoteDto> GetNoteAsync(int id);

        Task CreateNoteAsync(NoteDto newNote);

        Task UpdateNoteAsync(NoteDto existingNote);

        Task DeleteNoteAsync(int id);
    }
}
