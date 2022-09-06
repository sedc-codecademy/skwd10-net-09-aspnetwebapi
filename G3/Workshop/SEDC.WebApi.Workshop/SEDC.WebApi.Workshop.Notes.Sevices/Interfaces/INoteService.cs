using SEDC.WebApi.Workshop.Notes.ServiceModels.NotesModels;

namespace SEDC.WebApi.Workshop.Notes.Sevices.Interfaces
{
    public interface INoteService
    {
        IEnumerable<NoteDto> GetUserNotes(int userId);
        NoteDto GetNote(int id, int userId);
        string AddNote(CreateNote note, int userId);
        void DeleteNote(int id, int userId);
    }
}
