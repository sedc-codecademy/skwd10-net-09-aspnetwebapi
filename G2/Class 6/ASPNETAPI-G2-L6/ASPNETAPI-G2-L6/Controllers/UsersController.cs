using ASPNETAPI_G2_L6.Database;
using ASPNETAPI_G2_L6.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPNETAPI_G2_L6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly NotesDbContext _notesDbContext;
        public UsersController(NotesDbContext notesDbContext)
        {
            _notesDbContext = notesDbContext;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUserInfo([FromRoute]int id)
        {
            User user = await _notesDbContext.Users
                                             .Include(x => x.Notes)
                                             .SingleOrDefaultAsync(x => x.Id == id);
            
            if (user is null)
            {
                return NotFound();
            }

            return Ok(user);
        
        }
    }
}
