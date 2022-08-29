using Microsoft.EntityFrameworkCore;
using SEDC.WebApi.Workshop.Notes.DataModels.Models;

namespace SEDC.WebApi.Workshop.Notes.DataAccess.Repositories
{
    public class NoteEFRepository : IRepository<Note>
    {
        private readonly NotesDbContext _context;

        public NoteEFRepository(NotesDbContext context)
        {
            _context = context;
        }

        public int Delete(Note entity)
        {
            _context.Notes.Remove(entity);
            _context.SaveChanges();
            return entity.Id;
        }

        public IEnumerable<Note> FilterBy(Func<Note, bool> filter)
        {
            return _context.Notes.Where(filter);
        }

        public IEnumerable<Note> GetAll()
        {
            return _context.Notes;
        }

        public Note GetById(int id)
        {
            return _context.Notes.FirstOrDefault(x => x.Id == id);
        }

        public int Insert(Note entity)
        {
            _context.Notes.Add(entity);
            _context.SaveChanges();
            return entity.Id;
        }

        public int Update(Note entity)
        {
            _context.Notes.Update(entity);
            _context.SaveChanges();
            return entity.Id;
        }
    }
}
