using ASPNETAPI_G2_L3.DTOs;
using ASPNETAPI_G2_L3.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASPNETAPI_G2_L3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        [HttpGet("queryParameters")]
        public ActionResult<Note> GetNoteByQueryId(int id)
        {
            Note noteDb = StaticDb.Notes.FirstOrDefault(x => x.Id == id);

            if (noteDb == null)
            {
                return NotFound();
            }

            return StatusCode(StatusCodes.Status200OK, noteDb);
            //return Ok(noteDb);
        }

        [HttpGet("query2")]
        public ActionResult<Note> GetNoteByFromQueryId([FromQuery] int id)
        {
            Note noteDb = StaticDb.Notes.FirstOrDefault(x => x.Id == id);

            if (noteDb == null)
            {
                return NotFound();
            }

            return StatusCode(StatusCodes.Status200OK, noteDb);
            //return Ok(noteDb);
        }

        [HttpGet]
        [Route("{firstName}")]
        public ActionResult<User> GetUserByFirstName(string firstName)
        {
            User userDb = StaticDb.Users.FirstOrDefault(x => x.FirstName.ToLower() == firstName.ToLower());

            if (userDb == null)
            {
                return NotFound();
            }

            return Ok(userDb);
        }

        [HttpGet("getUserWithDto")]
        public IActionResult GetUserByIdDto([FromQuery] UserIdDto userIdDto)
        {
            User userDb = StaticDb.Users.FirstOrDefault(x => x.Id == userIdDto.Id);

            if (userDb == null)
            {
                return NotFound();
            }

            return Ok(userDb);
        }

        [HttpGet("filter")]
        public IActionResult FilterUsers(string? firstName, string? lastName)
        {
            User userDb = new User();

            if (string.IsNullOrEmpty(firstName))
            {
                userDb = StaticDb.Users.FirstOrDefault(x => x.LastName.ToLower() == lastName.ToLower());
                return Ok(userDb);
            }
            else if (string.IsNullOrEmpty(lastName))
            {
                userDb = StaticDb.Users.FirstOrDefault(x => x.FirstName.ToLower() == firstName.ToLower());
                return Ok(userDb);
            }
            else if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
            {
                userDb = StaticDb.Users.FirstOrDefault(x => x.LastName.ToLower() == lastName.ToLower() && x.FirstName.ToLower() == firstName.ToLower());
                return Ok(userDb);
            }

            return NotFound();
        }

        [HttpPost("UserDto")]
        public IActionResult CreateNewUser([FromBody] CreateUserDto userDto)
        {
            User newUser = new User();
            newUser.Id = userDto.Id;
            newUser.FirstName = userDto.FirstName;
            newUser.LastName = userDto.LastName;
            newUser.Username = userDto.Username;
            newUser.Password = userDto.Password;

            return Created("/api/Notes/getUserWithDto", newUser.Id);
        }

        [HttpPost("create/tag")]
        public IActionResult CreateNewTag([FromBody] CreateTagDto createTagDto)
        {
            Note noteDb = StaticDb.Notes.FirstOrDefault(x => x.Id == createTagDto.NoteId);

            if (noteDb == null)
            {
                return NotFound();
            }

            Tag tag = new Tag();
            tag.Id = createTagDto.Id;
            tag.Name = createTagDto.Name;
            tag.Color = createTagDto.Color;

            noteDb.Tags.Add(tag);

            return Ok();
        }

        [HttpPut("update/tag")]
        public IActionResult UpdateTag([FromBody] UpdateTagDto updateTagDto)
        {
            Tag tag = StaticDb.Notes.SelectMany(x => x.Tags).FirstOrDefault(x => x.Id == updateTagDto.Id);

            if (tag is null)
                return NotFound();


            tag.Name = updateTagDto.Name;
            tag.Color = updateTagDto.Color;

            return Ok();
        }

        //[HttpGet("tag/{id}")]
        //public IActionResult GetTag()
        //[HttpPost("tag")]
        //[HttpPatch("tag/{id}")]

        [HttpDelete("user/{id}")]
        public IActionResult DeleteUserById(int id)
        {
            User userDb = StaticDb.Users.FirstOrDefault(x => x.Id == id);

            if (userDb == null)
            {
                return NotFound();
            }

            StaticDb.Users.Remove(userDb);

            return Ok();
        }
    }
}
