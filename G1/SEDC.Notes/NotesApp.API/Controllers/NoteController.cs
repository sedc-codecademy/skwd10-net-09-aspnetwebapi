using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotesApp.Exceptions;
using NotesApp.Services.Implementations;
using NotesApp.Services.Interfaces;
using SEDC.Notes.InerfaceModels.Models;
using System.Security.Claims;
using Serilog;
using Serilog.Events;

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
            try
            {
                var response = _noteService.GetAll();
                return Ok(response);
            }
            catch (NoteException ex)
            {
                Log.Error("USER {userId}.{noteId}: {message}", ex.UserId, ex.NoteId, ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Write(LogEventLevel.Fatal, ex.Message);
                return BadRequest("Something went wrong. Please contact customer support.");
            }
        }

        [HttpGet("GetAllByUser")]
        public IActionResult GetAllNotesByUser() 
        {
            try
            {
                var userId = GetAuthorizedUserId();
                var response = _noteService.GetAll(userId);
                return Ok(response);
            }
            catch (NoteException ex)
            {
                Log.Error("USER {userId}.{noteId}: {message}", ex.UserId, ex.NoteId, ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Write(LogEventLevel.Fatal, ex.Message);
                return BadRequest("Something went wrong. Please contact customer support.");
            }
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetNoteById([FromRoute] int id) 
        {
            try
            {
                var userId = GetAuthorizedUserId();
                var response = _noteService.GetById(id, userId);
                return Ok(response);
            }
            catch (NoteException ex)
            {
                Log.Error("USER {userId}.{noteId}: {message}", ex.UserId, ex.NoteId, ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Write(LogEventLevel.Fatal, ex.Message);
                return BadRequest("Something went wrong. Please contact customer support.");
            }
        }

        [HttpPost("Create")]
        public IActionResult CreateNote([FromBody] NoteModel model) 
        {
            try
            {
                model.UserId = GetAuthorizedUserId();
                _noteService.Create(model);
                return Ok();
            }
            catch (NoteException ex)
            {
                Log.Error("USER {userId}.{noteId}: {message}", ex.UserId, ex.NoteId, ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Write(LogEventLevel.Fatal, ex.Message);
                return BadRequest("Something went wrong. Please contact customer support.");
            }
        }

        [HttpDelete("DeleteById/{id}")]
        public IActionResult DeleteNoteById([FromRoute] int id) 
        {
            try
            {
                _noteService.Delete(id);
                return Ok();
            }
            catch (NoteException ex)
            {
                Log.Error("USER {userId}.{noteId}: {message}", ex.UserId, ex.NoteId, ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Write(LogEventLevel.Fatal, ex.Message);
                return BadRequest("Something went wrong. Please contact customer support.");
            }
        }

        private int GetAuthorizedUserId() 
        {
            if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userId)) 
            {
                string name = User.FindFirst(ClaimTypes.Name)?.Value;
                throw new UserException(userId, name, "Name identifier claim does not exist!");
            }
            return userId;
        }

    }
}
