using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEDC.NotesAndTagsApp.Models;

namespace SEDC.NotesAndTagsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        [HttpGet]   //http://localhost:[port]/api/notes 
        public ActionResult<List<Note>> Get()
        {
            try
            {
                return Ok(StaticDb.Notes);
            }
            catch (Exception e)
            {
                //log
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred! Contact the admin!");
            }
        }

        [HttpGet("{index}")] //http://localhost:[port]/api/notes/1
        public ActionResult<Note> GetNoteByIndex(int index)
        {
            try
            {
                if(index < 0)
                {
                    return BadRequest("The index can not be negative!");
                }
                if(index >= StaticDb.Notes.Count)
                {
                    return NotFound($"There is no resource on index {index}");
                }

                return Ok(StaticDb.Notes[index]);
            }
            catch (Exception e)
            {
                //log
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred! Contact the admin!");
            }
        }

        [HttpGet("queryString")] //http://localhost:[port]/api/notes/queryString?index=1
        public ActionResult<Note> GetByQueryString(int? index) //optional parameter
        {
            try
            {
                if(index == null)
                {
                    return BadRequest("Index is a required parameter");
                }
                if (index < 0)
                {
                    return BadRequest("The index can not be negative!");
                }
                if (index >= StaticDb.Notes.Count)
                {
                    return NotFound($"There is no resource on index {index}");
                }

                return Ok(StaticDb.Notes[index.Value]);
            }
            catch (Exception e)
            {
                //log
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred! Contact the admin!");
            }
        }

        [HttpGet("{text}/priority/{priority}")] //http://localhost:[port]/api/notes/gym/priority/2 (find all notes whose text contains gym and priority is medium)
        public ActionResult<List<Note>> FilterNotes(string text, int priority)
        {
            try
            {
                if(string.IsNullOrEmpty(text) || priority <= 0)
                {
                    return BadRequest("Filter parameteres are required");
                }
                if(priority > 3)
                {
                    return BadRequest("Invalid value for priority");
                }

                List<Note> notesDb = StaticDb.Notes.Where(x => x.Text.ToLower().Contains(text.ToLower()) && (int)x.Priority == priority).ToList();
                return Ok(notesDb);
            }
            catch (Exception e)
            {
                //log
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred! Contact the admin!");
            }
        }

        [HttpGet("multipleQueryParams")]
        //http://localhost:5203/api/Notes/multipleQueryParams?text=gym&priority=2 (text = gym priority = 2)
        //http://localhost:5203/api/Notes/multipleQueryParams?priority=2&text=water (text = water priority = 2)
        //http://localhost:5203/api/Notes/multipleQueryParams?priority=2 (text = null priority = 2)
        public ActionResult<List<Note>> FilterNotesByMultipleParams(string? text, int? priority)
        {
            try
            {
                if(string.IsNullOrEmpty(text) && priority == null)
                {
                    return BadRequest("You have to send at least filter parameter");
                    //return Ok(StaticDb.Notes);
                }

                if (string.IsNullOrEmpty(text))
                {
                    //priority has a value
                    List<Note> filteredNotes = StaticDb.Notes.Where(x => (int)x.Priority == priority).ToList();
                    return Ok(filteredNotes);
                }

                if(priority == null)
                {
                    //text has value
                    List<Note> filteredNotes = StaticDb.Notes.Where(x => x.Text.ToLower().Contains(text.ToLower())).ToList();
                    return Ok(filteredNotes);
                }

                //text and priority have values
                //filter the notes whose text contains the text given as parameter and whose priority is equal to the parameter
                //we must cast priority because we must compare same types (x.Priority is Enum)
                List<Note> notesDb = StaticDb.Notes.Where(x => x.Text.ToLower().Contains(text.ToLower()) && (int)x.Priority == priority).ToList();
                return Ok(notesDb);

            }
            catch (Exception e)
            {
                //log
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred! Contact the admin!");
            }
        }

        [HttpGet("header")]
        public IActionResult GetHeader([FromHeader(Name ="TestHeader")] string testHeader)
        {
            return Ok(testHeader);
        }

        [HttpGet("userAgent")]
        public IActionResult GetUserAgentHeader([FromHeader(Name = "User-Agent")] string userAgent)
        {
            return Ok(userAgent);
        }

        [HttpPost]
        public IActionResult PostNote([FromBody] Note note)
        {
            try
            {
                if (string.IsNullOrEmpty(note.Text))
                {
                    return BadRequest("Note text must not be empty");
                }
                if(note.Tags == null || note.Tags.Count == 0)
                {
                    return BadRequest("Note must contain tags");
                }
                StaticDb.Notes.Add(note);
                return StatusCode(StatusCodes.Status201Created, "Note created");
            }
            catch (Exception e)
            {
                //log
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred! Contact the admin!");
            }
        }

        [HttpPut("updateNote/{index}")]
        public IActionResult UpdateNote(int index, [FromBody] Tag tag)
        {
            try
            {
                if (index < 0)
                {
                    return BadRequest("The index can not be negative!");
                }
                if (index >= StaticDb.Notes.Count)
                {
                    return NotFound($"There is no resource on index {index}");
                }

                Note noteDb = StaticDb.Notes[index];

                if(noteDb.Tags == null)
                {
                    noteDb.Tags = new List<Tag>();
                }
                noteDb.Tags.Add(tag);
                return StatusCode(StatusCodes.Status204NoContent, "Note updated!");
            }
            catch (Exception e)
            {
                //log
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred! Contact the admin!");
            }
        }


    }
}
