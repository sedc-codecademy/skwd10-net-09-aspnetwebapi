using ASPNETAPI_G2_L6.Contracts;
using ASPNETAPI_G2_L6.Database;
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
        public async Task<IActionResult> GetUserBaseInfo([FromRoute] int id)
        {
            UserDto? userDto = await _notesDbContext.Users.Select(x => new UserDto()
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName
            }).SingleOrDefaultAsync(x => x.Id == id);

            if (userDto is null)
            {
                return NotFound();
            }

            return Ok(userDto);
        }

        [HttpGet]
        [Route("{id}/details")]
        public async Task<IActionResult> GetUserDetails([FromRoute] int id)
        {
            //User? user = await _notesDbContext.Users
            //                                 .Include(x => x.Notes)
            //                                 .SingleOrDefaultAsync(x => x.Id == id);

            var user = await (from dbUser in _notesDbContext.Users
                              join dbNote in _notesDbContext.Notes
                              on dbUser.Id equals dbNote.UserId
                              where dbUser.Id == id
                              select new
                              {
                                  dbNote,
                                  dbUser
                              }).SingleOrDefaultAsync();

            if (user is null)
            {
                return NotFound();
            }

            return Ok(user);

        }
    }
}
