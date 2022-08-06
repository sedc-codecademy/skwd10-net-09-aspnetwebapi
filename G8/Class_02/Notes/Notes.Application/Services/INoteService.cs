using Notes.Application.Models;

namespace Notes.Application.Services
{
    public interface INoteService
    {
        public NoteModel GetNote(int id);
    }
}
