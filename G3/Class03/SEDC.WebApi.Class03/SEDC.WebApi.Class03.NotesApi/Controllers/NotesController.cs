using Microsoft.AspNetCore.Mvc;
using SEDC.WebApi.Class03.NotesApi.Models;

namespace SEDC.WebApi.Class03.NotesApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class NotesController : ControllerBase
    {
        private readonly List<Note> _notes = new List<Note>
        {
            new Note
            {
                Id = 1,
                Text = "Buy milk",
                Color = "Green",
                UserId = 1
            },
            new Note
            {
                Id = 2,
                Text = "Walk Dog",
                Color = "Orange",
                UserId = 1

            },
            new Note
            {
                Id = 3,
                Text = "Clean floor",
                Color = "Orange",
                UserId = 1

            }
        };

        [HttpGet]
        [Route("{id}")]
        public ActionResult<Note> GetById(int id)
        {
            var note = _notes.FirstOrDefault(x => x.Id == id);
            if(note is null)
            {
                return NotFound("Note doesnt exist");
            }
            return Ok(note);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Note>> GetList([FromQuery]string? orderBy)
        {
            IEnumerable<Note> notes = _notes;
            switch (orderBy)
            {
                case "Text":
                    notes = notes.OrderBy(x => x.Text);
                    break;
                case "Color":
                    notes = notes.OrderBy(x => x.Color);
                    break;
            }
            return Ok(notes);
        }
        [HttpGet("user/{userId}/notes")]
        public ActionResult<IEnumerable<Note>> GetNotesForUser(
            [FromRoute]int userId, [FromQuery]SearchNotesInput input)
        {
            var notes = _notes.Where(x => x.UserId == userId);
            if(!string.IsNullOrWhiteSpace(input.Color))
            {
                notes = notes.Where(x => x.Color == input.Color);
            }
            switch (input.OrderBy)
            {
                case "Text":
                    notes = notes.OrderBy(x => x.Text);
                    break;
                case "Color":
                    notes = notes.OrderBy(x => x.Color);
                    break;
            }
            return Ok(notes);
        }
        [HttpGet("user/{userId}/notesForUser")]
        public ActionResult<IEnumerable<Note>> GetNotesForLoggedUser(
            [FromRoute]int userId)
        {

            var authenticatedUser = Request.Headers["authenticatedUser"].ToString();
            //if (authenticatedUser is null)
            //{
            //    return Unauthorized();
            //}
            //if(authenticatedUser != userId)
            //{
            //    return StatusCode(403,"you cant acces notes for this user");
            //}
            return Ok(_notes.Where(x => x.UserId == userId));
        }

    }
}
