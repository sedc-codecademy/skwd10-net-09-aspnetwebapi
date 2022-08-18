using SEDC.WebApi.Workshop.Notes.DataModels.Models;

namespace SEDC.WebApi.Workshop.Notes.DataAccess.Repositories
{
    public class UserRepository : IRepository<User>
    {
        public int Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> FilterBy(Func<User, bool> filter)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User GetById(int id)
        {
            throw new NotImplementedException();
        }

        public int Insert(User entity)
        {
            throw new NotImplementedException();
        }

        public int Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
