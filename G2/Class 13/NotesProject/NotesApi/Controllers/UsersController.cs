using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Notes.Contracts.DTOs;
using Notes.Contracts.DTOs.UserDtos;
using Notes.Contracts.Services;
using Serilog;

namespace NotesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private INoteService _noteService;
        private readonly Microsoft.Extensions.Logging.ILogger _logger;

        public UsersController(IUserService userService, INoteService noteService, ILogger<UsersController> _logger)
        {
            _userService = userService;
            _noteService = noteService;
            this._logger = _logger;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public IActionResult RegisterUser([FromBody] RegisterUserDto registerUserDto)
        {
            if (registerUserDto == null)
            {
                return BadRequest();
            }

            _userService.RegisterUser(registerUserDto);

            Log.Information($"User with username {registerUserDto.Username} has registered.");
            return Ok(registerUserDto);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> LoginUser([FromBody] LoginUserDto loginUserDto)
        {
            //Log.Information($"User with username {loginUserDto.Username} has logged in.");
            _logger.LogWarning("This kind of log is not good");
            _logger.LogInformation($"About page visited: User with username {loginUserDto.Username} has logged in.",
            DateTime.UtcNow.ToLongTimeString());

            return Ok(await _userService.LoginUser(loginUserDto));
        }

        [HttpGet("notes/{id}")]
        public async Task<ActionResult<NoteDto>> GetNote(int id)
        {
            if (id < 1)
            {
                Log.Error($"Please insert valid ID. This {id} is not valid.");
                return BadRequest();
            }

            Log.Information($"Note with ID {id} was successfully found.");
            return Ok(await _noteService.GetNoteAsync(id));
        }
    }
}
