using Notes.Application.Models;

namespace Notes.Application.Services
{
    public interface INoteService
    {
        public NoteModel GetNote(int id);

        public IEnumerable<NoteModel> GetNotes();

        public NoteModel CreateNote(CreateNoteModel model, int userId);

        public NoteModel EditNote(EditNoteModel model, int id, int userId);

        void Delete(int id, int userId);
    }
}
