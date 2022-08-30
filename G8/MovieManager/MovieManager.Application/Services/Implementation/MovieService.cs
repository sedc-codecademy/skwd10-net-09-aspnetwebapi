using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieManager.Application.Dto;
using MovieManager.Application.Repositories;
using MovieManager.Domain.Enums;
using MovieManager.Domain.Exceptions;
using MovieManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieManager.Application.Services.Implementation
{
    public class MovieService : IMovieService
    {
        private readonly IRepository<Movie> _repository;
        private readonly IRepository<User> _userRepository;
        private readonly IMapper mapper;

        public MovieService(IRepository<Movie> repository, IRepository<User> userRepository, IMapper mapper)
        {
            _repository = repository;
            _userRepository = userRepository;
            this.mapper = mapper;
        }

        public int CreateMovie(MovieDto movie, int userId)
        {
            var user = _userRepository.GetById(userId);

            if (user == null)
                throw new NotFoundException("User not found!");

            var mappedMovie = new Movie(movie.Title, movie.Description, movie.Year, movie.Genre, movie.Rating, user);
            
            _repository.Create(mappedMovie);
            return mappedMovie.Id;
        }

        public int DeleteMovie(int movieId, int userId)
        {
            var user = _userRepository.Query().Include(x => x.Movies).FirstOrDefault(x => x.Id == userId);

            if (user == null)
                throw new NotFoundException("User not found!");

            Movie deletedMovie = user.RemoveMovie(movieId);
            _userRepository.Update(user);
            return deletedMovie.Id;
        }

        public MovieDto GetMovie(int movieId, int userId)
        {
            var movie = _repository.Query().Include(x => x.User).FirstOrDefault(x => x.Id == movieId);

            if (movie == null)
                throw new NotFoundException("Movie does not exist!");

            movie.ValidateOwner(userId);

            return mapper.Map<MovieDto>(movie);
        }

        public IEnumerable<MovieDto> GetUserMovies(int userId)
        {
            var user = _userRepository.Query().Include(x => x.Movies).FirstOrDefault(x => x.Id == userId);

            if (user == null)
                throw new NotFoundException("User not found!");

            return user.Movies.Select(x => mapper.Map<MovieDto>(x));
        }

        public IEnumerable<MovieDto> GetUserMoviesByGenre(MovieGenre genre, int userId)
        {
            var movies = _repository.Query().Where(x => x.Genre == genre && x.User.Id == userId);
            return movies.Select(x => mapper.Map<MovieDto>(x));
        }
    }
}
