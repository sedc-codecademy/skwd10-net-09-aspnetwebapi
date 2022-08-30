using MovieManager.Application.Dto;
using MovieManager.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieManager.Application.Services
{
    public interface IMovieService
    {
        MovieDto GetMovie(int movieId, int userId);
        IEnumerable<MovieDto> GetUserMovies(int userId);
        IEnumerable<MovieDto> GetUserMoviesByGenre(MovieGenre genre, int userId);
        int CreateMovie(MovieDto movie, int userId);
        int DeleteMovie(int movieId, int userId);
    }
}
