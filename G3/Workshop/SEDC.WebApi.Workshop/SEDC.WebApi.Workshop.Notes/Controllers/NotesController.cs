using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SEDC.WebApi.Workshop.Notes.Common.Exceptions;
using SEDC.WebApi.Workshop.Notes.ServiceModels.NotesModels;
using SEDC.WebApi.Workshop.Notes.Sevices.Interfaces;
using Serilog;
using System.Security.Claims;

namespace SEDC.WebApi.Workshop.Notes.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class NotesController : BaseController
    {
        private readonly INoteService _noteService;

        public NotesController(INoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpGet("get-all")]
        public ActionResult<IEnumerable<NoteDto>> GetNotes()
        {
            try
            {
                var notes = _noteService.GetUserNotes(UserId);
                return Ok(notes);
            }
            catch (UserException ex)
            {
                Log.Error("USER {userId}.{name}: {message}",
                    ex.UserId, ex.Name, ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return StatusCode(StatusCodes.Status400BadRequest,
                    "Something went wrong. Please contact support!");
            }
        }

        [HttpGet("get-by-id/{id}")]
        public ActionResult<NoteDto> Get(int id)
        {
            try
            {
                var note = _noteService.GetNote(id, UserId);
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
                var noteUrl = _noteService.AddNote(request, UserId);
                return Created(noteUrl, null);
                //return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete-note/{id}")]
        public ActionResult DeleteNote(int id)
        {
            try
            {
                _noteService.DeleteNote(id, UserId);
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
