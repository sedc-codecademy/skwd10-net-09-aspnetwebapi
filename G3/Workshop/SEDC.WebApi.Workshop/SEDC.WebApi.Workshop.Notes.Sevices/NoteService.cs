using SEDC.WebApi.Workshop.Notes.Common.MappingHelpers;
using SEDC.WebApi.Workshop.Notes.DataAccess;
using SEDC.WebApi.Workshop.Notes.DataModels.Models;
using SEDC.WebApi.Workshop.Notes.ServiceModels.NotesModels;
using SEDC.WebApi.Workshop.Notes.Sevices.Interfaces;

namespace SEDC.WebApi.Workshop.Notes.Sevices
{
    public class NoteService : INoteService
    {
        private readonly IRepository<Note> _noteRepository;
        private readonly IRepository<User> _userRepositry;

        public NoteService(IRepository<Note> noteRepository,
            IRepository<User> userRepositry)
        {
            _noteRepository = noteRepository;
            _userRepositry = userRepositry;
        }

        public void AddNote(CreateNote note)
        {
            var user = _userRepositry.GetById(note.UserId);
            
            if(user == null)
            {
                throw new Exception("User not found");
            }

            var newwNote = new Note
            {
                Text = note.Text,
                UserId = note.UserId,
                Color = note.Color,
                Tag = note.Tag
            };

            _noteRepository.Insert(newwNote);
        }

        public void DeleteNote(int id, int userId)
        {
            var note = _noteRepository
                .FilterBy(x => x.Id == id && x.UserId == userId)
                .FirstOrDefault();

            if(note == null)
            {
                throw new Exception("Note not found");
            }

            _noteRepository.Delete(note);
        }

        public NoteDto GetNote(int id, int userId)
        {
            var note = _noteRepository
                .FilterBy(x => x.Id == id && x.UserId == userId)
                .FirstOrDefault();

            if (note == null)
                throw new Exception("Note not found");

            return note.Map();
        }

        public IEnumerable<NoteDto> GetUserNotes(int userId)
        {
            return _noteRepository
                .FilterBy(x => x.UserId == userId)
                .Select(note => note.Map());
        }
    }
}
