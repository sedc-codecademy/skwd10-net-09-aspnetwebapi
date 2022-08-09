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

        public NoteService(IRepository<Note> repository)
        {
            this.repository = repository;
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
