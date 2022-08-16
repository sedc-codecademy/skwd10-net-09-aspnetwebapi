using AutoMapper;
using MovieManager.Application.Dto;
using MovieManager.Application.Repositories;
using MovieManager.Domain.Enums;
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

        public int CreateMovie(MovieDto movie)
        {
            var user = _userRepository.GetById(movie.UserId);

            if (user == null)
                throw new Exception("User not found!");

            var mappedMovie = mapper.Map<Movie>(movie);
            _repository.Create(mappedMovie);
            return mappedMovie.Id;
        }

        public void DeleteMovie(int movieId, int userId)
        {
            throw new NotImplementedException();
        }

        public MovieDto GetMovie(int movieId, int userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MovieDto> GetUserMovies(int userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MovieDto> GetUserMoviesByGenre(MovieGenre genre, int userId)
        {
            throw new NotImplementedException();
        }
    }
}
