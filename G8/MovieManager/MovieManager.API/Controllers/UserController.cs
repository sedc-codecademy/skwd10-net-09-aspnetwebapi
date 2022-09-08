using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieManager.Application.Dto;
using MovieManager.Application.Services;
using MovieManager.Domain.Exceptions;
using System.Security.Claims;

namespace MovieManager.API.Controllers
{
    [Authorize]
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
        [AllowAnonymous]
        [HttpPost("register")]
        public ActionResult RegisterUser([FromBody] RegisterDto user)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            _service.Register(user);
            return Created("api/v1/user/login", user.Username);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult<TokenDto> Login([FromBody] LoginDto login)
        {
            try
            {
                TokenDto token = _service.Autenticate(login);
                return Ok(token.Token);
            }
            catch (NotFoundException)
            {
                return BadRequest();
            }
        }

        [HttpGet("whoami")]
        public ActionResult WhoAmI()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            string firstname = User.FindFirstValue(ClaimTypes.Name);
            string lastname = User.FindFirstValue(ClaimTypes.Surname);
            string username = User.FindFirstValue("CustomClaimTypeUsername");

            return Ok( new { userId, username, firstname, lastname });
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet()]
        public ActionResult GetAllUsers()
        {
            var users = _service.GetUsers();
            return Ok(users);
        }
    }
}
