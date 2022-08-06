using ASPNETAPI_G2_L2.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASPNETAPI_G2_L2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {

        [HttpGet]
        [Route("GetNote/{guid:guid}")]
        public IActionResult GetNote([FromRoute] Guid guid, [FromQuery] int id)
        {
            return StatusCode(StatusCodes.Status200OK, $"request was successful, id: {id}, GUID: {guid}");
        }

        [HttpPost]
        [Route("InsertNote/{id:int}/something/{id1:int}/{id2:int}")]
        public IActionResult InsertNote([FromRoute] int id, [FromRoute] int id1, [FromRoute] int id2, [FromBody] CreateNoteRequest request)
        {
            Console.WriteLine(Request.Body);

            return Ok(request.ToString() + id + id1 + id2);
        }
    }
}
