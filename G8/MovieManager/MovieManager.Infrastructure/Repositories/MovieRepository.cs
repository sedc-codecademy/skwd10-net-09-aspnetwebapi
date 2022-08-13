using MovieManager.Application.Repositories;
using MovieManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieManager.Infrastructure.Repositories
{
    public class MovieRepository : IRepository<Movie>
    {
        public Movie Create(Movie entity)
        {
            var id = MovieManagerStaticDb.Movies.LastOrDefault()?.Id ?? 0;
            entity.Id = ++id;

            MovieManagerStaticDb.Movies.Add(entity);
            return entity;
        }

        public Movie Delete(Movie entity)
        {
            MovieManagerStaticDb.Movies.Remove(entity);
            return entity;
        }

        public IList<Movie> GetAll()
        {
            return MovieManagerStaticDb.Movies.ToList();
        }

        public Movie? GetById(int id)
        {
            return MovieManagerStaticDb.Movies.FirstOrDefault(x => x.Id == id);
        }

        public Movie Update(Movie entity)
        {
            var movie = GetById(entity.Id);

            if (movie != null)
                movie = entity;

            return entity;
        }
    }
}
