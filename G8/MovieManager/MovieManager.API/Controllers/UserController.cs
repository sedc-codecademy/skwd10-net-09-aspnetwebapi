using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieManager.Application.Dto;
using MovieManager.Application.Services;

namespace MovieManager.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        //api/v1/user/register
        [HttpPost("register")]
        public ActionResult RegisterUser([FromBody] RegisterDto user)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            _service.Register(user);
            return Created("api/v1/user/login", user);
        }
    }
}
