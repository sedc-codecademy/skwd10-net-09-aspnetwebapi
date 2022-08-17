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

        public List<UserDto> GetAll()
        {
            return _context.Users
                           .Include(x => x.NoteList)
                           .ToList();
        }

        public UserDto GetById(int id)
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == id);
            return user;
        }

        public void Update(UserDto entity)
        {
            _context.Users.Update(entity);
            _context.SaveChanges();
        }
    }
}
