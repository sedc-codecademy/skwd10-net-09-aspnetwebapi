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
                throw new Exception("Note was not found");
            }

            return new NoteModel
            {
                Id = note.Id,
                Color = note.Color,
                Tag = note.Tag,
                Text = note.Text,
                UserName = note.User.Username
            };
        }
    }
}
