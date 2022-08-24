using SEDC.WebApi.Workshop.Notes.DataModels.Models;

namespace SEDC.WebApi.Workshop.Notes.DataAccess.Repositories
{
    public class UserEFRepository : IRepository<User>
    {
        private readonly NotesDbContext _context;

        public UserEFRepository(NotesDbContext context)
        {
            _context = context;
        }

        public int Delete(User entity)
        {
            _context.Users.Remove(entity);
            _context.SaveChanges();
            return entity.Id;
        }

        public IEnumerable<User> FilterBy(Func<User, bool> filter)
        {
            return _context.Users.Where(filter);
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        public User GetById(int id)
        {
            return _context.Users.FirstOrDefault(x => x.Id == id);
        }

        public int Insert(User entity)
        {
            _context.Users.Add(entity);
            _context.SaveChanges();
            return entity.Id;
        }

        public int Update(User entity)
        {
            _context.Users.Update(entity);
            _context.SaveChanges();
            return entity.Id;
        }
    }
}
