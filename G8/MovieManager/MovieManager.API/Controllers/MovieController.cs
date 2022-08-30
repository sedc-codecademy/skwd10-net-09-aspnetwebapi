using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieManager.Application.Dto;
using MovieManager.Application.Services;
using MovieManager.Domain.Enums;
using MovieManager.Domain.Exceptions;

namespace MovieManager.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _service;

        public MovieController(IMovieService service)
        {
            _service = service;
        }

        [HttpPost]
        public ActionResult AddMovie(MovieDto movie,[FromQuery] int userId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            int movieId = _service.CreateMovie(movie, userId);
            return Created($"api/v1/movie/{movieId}", movie);
        }

        //api/v1/Movie/1?userId=1
        [HttpDelete("{movieId:int}")]
        public ActionResult DeleteMovieForUser(int movieId,[FromQuery] int userId)
        {
            try
            {
                int deletedMovie = _service.DeleteMovie(movieId, userId);
                return Ok(deletedMovie);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet()]
        public ActionResult GetMoviesForUser(int userId)
        {
            try
            {
                var movies = _service.GetUserMovies(userId);
                return Ok(movies);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet("genre/{genre}")]
        public ActionResult GetMoviesForUserByGenre([FromQuery]int userId, [FromRoute] MovieGenre genre)
        {
            var movies = _service.GetUserMoviesByGenre(genre, userId);
            return Ok(movies);
        }

        [HttpGet("{movieId:int}")]
        public ActionResult GetMovieForUser(int movieId, [FromQuery] int userId)
        {
            try
            {
                var movie = _service.GetMovie(movieId, userId);
                return Ok(movie);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (UserIsNotOwnerException)
            {
                return Forbid();
            }
        }
    }
}
