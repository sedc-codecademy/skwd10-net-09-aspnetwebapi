using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Notes.Application.Exceptions;
using Notes.Application.Models;
using Notes.Application.Services;

namespace Notes.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteService service;

        public NoteController(INoteService service)
        {
            this.service = service;
        }
        //api/v1/note/1
        [HttpGet("{id:int}")]
        public ActionResult<NoteModel> GetNote(int id)
        {
            try
            {
                return Ok(service.GetNote(id));
            }
            catch(NotFoundException)
            {
                return NotFound();
            }
        }

        // GetNotes
        [HttpGet]
        public ActionResult<IEnumerable<NoteModel>> GetNotes()
        {
            return Ok(service.GetNotes());
        }

        [HttpPost]
        public ActionResult<NoteModel> CreateNote([FromBody] CreateNoteModel model, [FromQuery] int? userId)
        {
            if(userId == null)
            {
                return Unauthorized(); // 403
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400
            }
            try
            {
                var noteModel = service.CreateNote(model, userId.Value);
                return Created($"api/v1/note/{noteModel.Id}", noteModel); // 201
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
}
