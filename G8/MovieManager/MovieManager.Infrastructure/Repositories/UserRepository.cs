using MovieManager.Application.Repositories;
using MovieManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieManager.Infrastructure.Repositories
{
    public class UserRepository : IRepository<User>
    {
        public User Create(User entity)
        {
            var id = MovieManagerStaticDb.Users.LastOrDefault()?.Id ?? 0;
            entity.Id = ++id;

            MovieManagerStaticDb.Users.Add(entity);
            return entity;
        }

        public User Delete(User entity)
        {
            MovieManagerStaticDb.Users.Remove(entity);
            return entity;
        }

        public IList<User> GetAll()
        {
            return MovieManagerStaticDb.Users.ToList();
        }

        public User? GetById(int id)
        {
            return MovieManagerStaticDb.Users.FirstOrDefault(x => x.Id == id);
        }

        public User Update(User entity)
        {
            var user = GetById(entity.Id);
            if (user != null)
                user = entity;

            return entity;

        }
    }
}
