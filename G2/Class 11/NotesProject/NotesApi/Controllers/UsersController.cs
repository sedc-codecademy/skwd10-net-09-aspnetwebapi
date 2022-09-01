using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Notes.Contracts.DTOs;
using Notes.Contracts.DTOs.UserDtos;
using Notes.Contracts.Services;

namespace NotesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private INoteService _noteService;

        public UsersController(IUserService userService, INoteService noteService)
        {
            _userService = userService;
            _noteService = noteService;
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

            return Ok(registerUserDto);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> LoginUser([FromBody] LoginUserDto loginUserDto)
        {
            return Ok(await _userService.LoginUser(loginUserDto));
        }

        [HttpGet("notes/{id}")]
        public async Task<ActionResult<NoteDto>> GetNote(int id)
        {
            return Ok(await _noteService.GetNoteAsync(id));
        }
    }
}
