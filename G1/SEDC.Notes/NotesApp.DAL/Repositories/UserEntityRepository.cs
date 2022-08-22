using Microsoft.EntityFrameworkCore;
using NotesApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp.DAL.Repositories
{
    public class UserEntityRepository : IRepository<UserDto>
    {
        private readonly NotesAppDbContext _context;
        public UserEntityRepository(NotesAppDbContext context)
        {
            _context = context;
        }

        public void Add(UserDto entity)
        {
            _context.Users.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(UserDto entity)
        {
            _context.Users.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<UserDto> GetAll()
        {
            return _context.Users
                           .Include(x => x.NoteList);
        }

        public UserDto GetById(int id)
        {
            return _context.Users.SingleOrDefault(u => u.Id == id);
        }

        public void Update(UserDto entity)
        {
            _context.Users.Update(entity);
            _context.SaveChanges();
        }
    }
}
