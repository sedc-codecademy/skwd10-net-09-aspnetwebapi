using Notes.Application.Repositories;
using Notes.Domain.Models;

namespace Notes.Infrastracture.Repositories
{
    public class UserRepository : IRepository<User>
    {
        public User Create(User entity)
        {
            var id = NotesStaticDb.User.LastOrDefault()?.Id ?? 0;
            entity.Id = ++id;
            NotesStaticDb.User.Add(entity);
            return entity;
        }

        public User Delete(User entity)
        {
            NotesStaticDb.User.Remove(entity);
            return entity;
        }

        public List<User> GetAll()
        {
            return NotesStaticDb.User.ToList();
        }

        public User? GetById(int id)
        {
            return NotesStaticDb.User.FirstOrDefault(x => x.Id == id);
        }

        public User Update(User entity)
        {
            var user = GetById(entity.Id);
            user = entity;
            return entity;
        }
    }
}
