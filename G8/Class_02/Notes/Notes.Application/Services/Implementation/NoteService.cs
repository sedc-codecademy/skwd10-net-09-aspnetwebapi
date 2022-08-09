using Notes.Application.Exceptions;
using Notes.Application.Mapper;
using Notes.Application.Models;
using Notes.Application.Repositories;
using Notes.Domain.Models;

namespace Notes.Application.Services.Implementation
{
    public class NoteService : INoteService
    {
        private readonly IRepository<Note> repository;
        private readonly IRepository<User> userRepository;

        public NoteService(IRepository<Note> repository, IRepository<User> userRepository)
        {
            this.repository = repository;
            this.userRepository = userRepository;
        }

        public NoteModel CreateNote(CreateNoteModel model, int userId)
        {
            var user = userRepository.GetById(userId);
            if(user == null)
            {
                throw new NotFoundException("User doesn't exist");
            }

            var note = new Note(model.Text, model.Color, user)
            {
                Tag = model.Tag,
            };
            repository.Create(note);
            return note.ToModel();
        }

        public NoteModel GetNote(int id)
        {
            var note = repository.GetById(id);
            if(note == null)
            {
                throw new NotFoundException("Note was not found");
            }

            return note.ToModel();
        }

        public IEnumerable<NoteModel> GetNotes()
        {
            return repository.GetAll().Select(note => note.ToModel());
        }
    }
}
