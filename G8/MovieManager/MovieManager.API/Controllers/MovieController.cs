using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieManager.Application.Dto;
using MovieManager.Application.Services;

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
        public ActionResult AddMovie(MovieDto movie)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            int movieId = _service.CreateMovie(movie);
            return Created($"api/v1/movie/{movieId}", movie);
        }
    }
}
