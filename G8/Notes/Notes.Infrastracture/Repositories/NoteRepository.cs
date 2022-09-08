using Notes.Application.Repositories;
using Notes.Domain.Models;

namespace Notes.Infrastracture.Repositories
{
    public class NoteRepository : IRepository<Note>
    {
        public Note? GetById(int id)
        {
            return NotesStaticDb.Notes.FirstOrDefault(x => x.Id == id);
        }

        public Note Create(Note entity)
        {
            var id = NotesStaticDb.Notes.LastOrDefault(x => x.Id == entity.Id)?.Id ?? 0;
            entity.Id = ++id;

            NotesStaticDb.Notes.Add(entity);
            return entity;
        }

        public Note Delete(Note entity)
        {
            NotesStaticDb.Notes.Remove(entity);
            return entity;
        }

        public IQueryable<Note> GetAll()
        {
            return NotesStaticDb.Notes.AsQueryable();
        }

        public Note Update(Note entity)
        {
            var note = GetById(entity.Id);
            if(note != null)
            {
                note = entity;
            }

            return entity;
        }
    }
}
