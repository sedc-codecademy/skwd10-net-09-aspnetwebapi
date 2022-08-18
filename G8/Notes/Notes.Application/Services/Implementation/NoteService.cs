using AutoMapper;
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
                //Tag = model.Tag, TODO
            };
            repository.Create(note);
            return note.ToModel();
        }

        public void Delete(int id, int userId)
        {
            var note = repository.GetById(id);
            if(note == null)
            {
                throw new NotFoundException("Note was not found");
            }

            if(note.User.Id != userId)
            {
                throw new ExecutionNotAllowedException();
            }
        }

        public NoteModel EditNote(EditNoteModel model, int id, int userId)
        {
            var note = repository.GetById(id);
            if(note == null)
            {
                throw new NotFoundException("Note was not found");
            }

            if(note.User.Id != userId)
            {
                throw new ExecutionNotAllowedException();
            }

            note.Text = model.Text;
            note.Color = model.Color;
            //note.Tag = model.Tag; TODO:
            repository.Update(note);
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
