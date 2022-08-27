using Microsoft.AspNetCore.Mvc;
using SEDC.WebApi.Class08.Adonet.Services;

namespace SEDC.WebApi.Class08.Adonet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NoteController : ControllerBase
    {
        [HttpGet]
        public ActionResult GetAllNotes()
        {
            var notesService = new NoteService();
            notesService.Add("new Text", "Green", 1, 1);
            var notes = notesService.GetAllNotes();
            //var note = notesService.GetNoteByUserIdAndNoteId(1, 2);
            return Ok(notes);
        }

        [HttpGet("dapper")]
        public ActionResult GetAll()
        {
            var noteService = new NoteDapperService();
            noteService.Add("Dapper", "Black", 5, 1);
            var notes = noteService.GetAll();
            var note = noteService.GetByUserIdAndNoteId(1, 7);

            return Ok(notes);
        }
    }
}
