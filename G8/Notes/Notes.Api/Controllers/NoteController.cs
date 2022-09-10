using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Notes.Application;
using Notes.Application.Exceptions;
using Notes.Application.Models;
using Notes.Application.Models.External;
using Notes.Application.Services;
using System.Security.Claims;

namespace Notes.Api.Controllers
{
    // post- za kreiranje
    // put - za menuvanje na celiot model
    // patch - koga sakame da smenime direktva nekoe prop - retko se koristi
    // post za povikuvanje metodi 
    [Authorize(Policy = SystemPolicies.MustHaveId)]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteService service;
        private readonly Serilog.ILogger logger;

        public NoteController(INoteService service, Serilog.ILogger logger)
        {
            this.service = service;
            this.logger = logger;
            logger.Debug("");
            logger.Information("");
            logger.Warning("");
            logger.Error("");
        }
        //api/v1/note/1
        [HttpGet("{id:int}")]
        public ActionResult<NoteModel> GetNote(int id, CancellationToken token)
        {
            try
            {
                return Ok(service.GetNote(id));
            }
            catch (NotFoundException ex)
            {
                logger.Warning($"Note was not found id {id} for user {new ClaimsPrinicipalWrapper(User).Id}", ex);
                return NotFound();
            }
        }

        // GetNotes
        [HttpGet]
        public ActionResult<IEnumerable<NoteModel>> GetNotes()
        {
            return Ok(service.GetNotes());
        }
        // var url = 'https://localhost:7323/api/v1/note/by-super-admin/create?userId=[0-9]+' 
        // ?userId=1&noteId=1&tag=asdads&color=asdasd
        [HttpPost("by-super-admin")]// ?userId=[0-9]+
        public ActionResult<NoteModel> CreateNote([FromBody] CreateNoteModel model, [FromQuery] int? userId)
        {
            if (userId == null)
            {
                return Unauthorized(); // 401
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400
            }

            var noteModel = service.CreateNote(model, userId.Value);
            return Created($"api/v1/note/{noteModel.Id}", noteModel); // 201

        }
        //note/{123123123}
        [HttpPut("{id:int}")]
        public ActionResult<EditNoteModel> EditNote([FromBody] EditNoteModel model, int id)
        {
            var userIdString = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)!.Value;

            if (!int.TryParse(userIdString, out int userId))
            {
                throw new Exception("");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }

            var note = service.EditNote(model, id, userId);
            return Ok(note);
            //return StatusCode(StatusCodes.Status200OK, note);

        }

        // DeleteNote -> params id & userId?

        [HttpDelete("{id:int}")]
        public ActionResult DeleteNote(int id, int? userId)
        {
            if (!userId.HasValue)
            {
                return Unauthorized();
            }

            service.Delete(id, userId.Value);
            return Ok();
        }
    }
}
