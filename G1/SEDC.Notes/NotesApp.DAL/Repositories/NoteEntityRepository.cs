using NotesApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp.DAL.Repositories
{
    public class NoteEntityRepository : IRepository<NoteDto>
    {
        private readonly NotesAppDbContext _context;
        public NoteEntityRepository(NotesAppDbContext context)
        {
            _context = context;
        }

        public void Add(NoteDto entity)
        {
            _context.Notes.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(NoteDto entity)
        {
            _context.Notes.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<NoteDto> GetAll()
        {
            return _context.Notes;
        }

        public NoteDto GetById(int id)
        {
            var note = _context.Notes.SingleOrDefault(u => u.Id == id);
            return note;
        }

        public void Update(NoteDto entity)
        {
            _context.Notes.Update(entity);
            _context.SaveChanges();
        }

    }
}
