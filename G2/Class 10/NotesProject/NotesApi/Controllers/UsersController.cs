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

        [HttpGet("notes/{id}")]
        public ActionResult<NoteDto> GetNote(int id)
        {
            return Ok(_noteService.GetNoteAsync(id));
        }
    }
}
