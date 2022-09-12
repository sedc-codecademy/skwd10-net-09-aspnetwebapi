using SEDC.WebApi.Workshop.Notes.Common.Exceptions;
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

        public string AddNote(CreateNote note, int userId)
        {
            var user = _userRepositry.GetById(userId);
            
            if(user == null)
            {
                throw new UserException(userId, null, "User not found");
            }

            var newwNote = new Note
            {
                Text = note.Text,
                UserId = userId,
                Color = note.Color,
                Tag = note.Tag
            };

            _noteRepository.Insert(newwNote);

            var url = 
                $"http://localhost:5277/api/v1/notes/get-all";
            return url;
        }

        public void DeleteNote(int id, int userId)
        {
            var note = _noteRepository
                .FilterBy(x => x.Id == id && x.UserId == userId)
                .FirstOrDefault();

            if(note == null)
            {
                throw new NoteException(id, userId, "Note not found");
            }

            _noteRepository.Delete(note);
        }

        public NoteDto GetNote(int id, int userId)
        {
            var note = _noteRepository
                .FilterBy(x => x.Id == id && x.UserId == userId)
                .FirstOrDefault();

            if (note == null)
            {
                throw new NoteException(id, userId, "Note not found");
            }

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
