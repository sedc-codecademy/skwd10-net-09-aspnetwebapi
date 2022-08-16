using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SEDC.NotesApp.Models;

namespace SEDC.NotesApp.Controllers
{
    [Route("api/[controller]")] //http://localhost:[port]/api/notes
    [ApiController]
    public class NotesController : ControllerBase
    {
        [HttpGet] //http://localhost:[port]/api/notes
        public ActionResult<List<string>> Get()
        {
            //return StatusCode(StatusCodes.Status200OK, StaticDb.SimpleNotes);
            return Ok(StaticDb.SimpleNotes);
        }

        [HttpGet("{index}")] ////http://localhost:[port]/api/notes/1
        public ActionResult<string> GetByIndex(int index)
        {
            try
            {
                if (index < 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "The index has negative value");
                }

                //throw new Exception();

                if (index >= StaticDb.SimpleNotes.Count)
                {
                    return StatusCode(StatusCodes.Status404NotFound, $"There is no resource on index {index}");
                }

                return StatusCode(StatusCodes.Status200OK, StaticDb.SimpleNotes[index]);
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred. Contact the administrator");
            }
        }

        [HttpGet("{noteId}/user/{userId}")] //http://localhost:[port]/api/notes/1/user/2
        public ActionResult<string> GetNoteByIdAndUserId(int noteId, int userId)
        {
            //return StaticDb.SimpleNotes[8];
            if(noteId < 0 || userId < 0)
            {
                //return StatusCode(StatusCodes.Status400BadRequest, "The ids can not be negative!");
                return BadRequest("The ids can not be negative!");
                //return "Invalid values";
            }

            //return StatusCode(StatusCodes.Status200OK, $"Returning note with id {noteId} for user with id {userId}");
            return Ok($"Returning note with id {noteId} for user with id {userId}");
        }

        [HttpPost] //http://localhost:[port]/api/notes
        public IActionResult Post()
        {
            try
            {
                //Request (ready class) -> Http Request which was sent to the action
                using(StreamReader reader = new StreamReader(Request.Body))
                {
                    string newNote = reader.ReadToEnd();

                    if (string.IsNullOrEmpty(newNote))
                    {
                        return BadRequest("The body of the request can not be empty");
                    }

                    StaticDb.SimpleNotes.Add(newNote);
                    return StatusCode(StatusCodes.Status201Created, "The new note was added");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred. Contact the administrator");
            }
        }

        [HttpDelete] //http://localhost:[port]/api/notes
        public IActionResult Delete()
        {
            try
            {
                using (StreamReader reader = new StreamReader(Request.Body))
                {
                    //we read the request's body
                    string requestBody = reader.ReadToEnd();
                    //we need to parse the body, in order to get the index (int)
                    int index = Int32.Parse(requestBody);

                    if(index < 0)
                    {
                        return BadRequest("Index can not be less than zero");
                    }
                    if(index >= StaticDb.SimpleNotes.Count)
                    {
                        return NotFound($"The resource on index {index} was not  found");
                    }

                    StaticDb.SimpleNotes.RemoveAt(index);
                    return StatusCode(StatusCodes.Status204NoContent, "Note deleted");
                    //return Ok();
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred. Contact the administrator");
            }
        }

        [HttpPost("postNote")]
        public IActionResult PostNote(/*[FromBody] Note note*/)
        {
            try
            {
                using(StreamReader reader = new StreamReader(Request.Body))
                {
                    string requestBody = reader.ReadToEnd(); //jsonString
                    Note newNote = JsonConvert.DeserializeObject<Note>(requestBody);
                    if(newNote == null)
                    {
                        return BadRequest("Not able to deserialize to Note object");
                    }


                    StaticDb.Notes.Add(newNote);
                    return StatusCode(StatusCodes.Status201Created, "Note created");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred. Contact the administrator");
            }
        }
    }
}
