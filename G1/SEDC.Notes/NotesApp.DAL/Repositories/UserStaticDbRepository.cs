using NotesApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp.DAL.Repositories
{
    public class UserStaticDbRepository : IRepository<UserDto>
    {
        public void Add(UserDto entity)
        {
            entity.Id = ++StaticDB.UserIdCounter;
            StaticDB.Users.Add(entity);
        }

        public void Delete(UserDto entity)
        {
            StaticDB.Users.Remove(entity);
        }

        public List<UserDto> GetAll()
        {
            return StaticDB.Users;
        }

        public UserDto GetById(int id)
        {
            var result = StaticDB.Users.FirstOrDefault(user => user.Id == id);
            return result;
        }

        public void Update(UserDto entity)
        {
            var result = StaticDB.Users.FirstOrDefault(user => user.Id == entity.Id);
            result.Username = entity.Username;
            result.Password = entity.Password;
            result.FirstName = entity.FirstName;
            result.LastName = entity.LastName;
        }
    }
}
