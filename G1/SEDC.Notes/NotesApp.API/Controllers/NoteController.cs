﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotesApp.Services.Implementations;
using SEDC.Notes.InerfaceModels.Models;

namespace NotesApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly NoteService _noteService;

        public NoteController()
        {
            _noteService = new NoteService();
        }

        [HttpGet("GetAll")]
        public IActionResult GetAllNotes() 
        {
            var response = _noteService.GetAll();
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
