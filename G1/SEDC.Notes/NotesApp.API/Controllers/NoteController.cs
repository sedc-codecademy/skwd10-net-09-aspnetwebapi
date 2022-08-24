using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotesApp.Services.Implementations;
using NotesApp.Services.Interfaces;
using SEDC.Notes.InerfaceModels.Models;
using System.Security.Claims;

namespace NotesApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteService _noteService;

        public NoteController(INoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAllNotes() 
        {
            var response = _noteService.GetAll();
            return Ok(response);
        }

        [HttpGet("GetAllByUser")]
        public IActionResult GetAllNotesByUser() 
        {
            //TODO: refactor this in separate private method!
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var response = _noteService.GetAll(userId);
            return Ok(response);
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetNoteById([FromRoute] int id) 
        {
            var response = _noteService.GetById(id);
            return Ok(response);
        }

        [HttpPost("Create")]
        public IActionResult CreateNote([FromBody] NoteModel model) 
        {
            _noteService.Create(model);
            return Ok();
        }

        [HttpDelete("DeleteById/{id}")]
        public IActionResult DeleteNoteById([FromRoute] int id) 
        {
            _noteService.Delete(id);
            return Ok();
        }

    }
}
