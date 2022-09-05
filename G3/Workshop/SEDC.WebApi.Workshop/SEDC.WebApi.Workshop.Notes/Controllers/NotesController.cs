using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SEDC.WebApi.Workshop.Notes.ServiceModels.NotesModels;
using SEDC.WebApi.Workshop.Notes.Sevices.Interfaces;

namespace SEDC.WebApi.Workshop.Notes.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class NotesController : ControllerBase
    {
        private readonly INoteService _noteService;

        public NotesController(INoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpGet("/user/{userId}")]
        public ActionResult<IEnumerable<NoteDto>> GetNotes(int userId)
        {
            try
            {
                var notes = _noteService.GetUserNotes(userId);
                return Ok(notes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}/user/{userId}")]
        public ActionResult<NoteDto> Get(int id, int userId)
        {
            try
            {
                var note = _noteService.GetNote(id, userId);
                return Ok(note);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("create-note")]
        public ActionResult CreateNote(CreateNote request)
        {
            try
            {
                var noteUrl = _noteService.AddNote(request);
                return Created(noteUrl, null);
                //return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete-note/{id}/user/{userId}")]
        public ActionResult DeleteNote(int id, int userId)
        {
            try
            {
                _noteService.DeleteNote(id, userId);
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
