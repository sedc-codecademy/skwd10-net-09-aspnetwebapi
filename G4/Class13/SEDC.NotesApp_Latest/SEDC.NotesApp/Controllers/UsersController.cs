using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEDC.NotesApp.Dtos.Users;
using SEDC.NotesApp.Services.Interfaces;
using SEDC.NotesApp.Shared.CustomExceptions;
using Serilog;

namespace SEDC.NotesApp.Controllers
{
    [Authorize] 
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous] //no token needed (we can not be logged in before registration)
        [HttpPost("register")] 
        public IActionResult Register([FromBody] RegisterUserDto registerUserDto)
        {
            try
            {
                Log.Information($"Registration model info: FirstName: {registerUserDto.FirstName}, LastName: {registerUserDto.LastName}");
                _userService.RegisterUser(registerUserDto);
                Log.Information($"Successfully registered {registerUserDto.Username}.");
                return StatusCode(StatusCodes.Status201Created, "User created!");
            }
            catch(UserDataException e)
            {
                Log.Error($"There was error registering user because of the following error: {e.Message}");
                return BadRequest(e.Message);
            }
            catch(Exception e)
            {
                Log.Error($"Internal exception: {e.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred!");
            }
        }

        [AllowAnonymous] //no token needed (we can not be logged in before login)
        [HttpPost("login")]
        public IActionResult LoginUser([FromBody] LoginUserDto loginDto)
        {
            try
            {
                string token = _userService.LoginUser(loginDto);
                return Ok(token);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred!");
            }
        }

    }
}
