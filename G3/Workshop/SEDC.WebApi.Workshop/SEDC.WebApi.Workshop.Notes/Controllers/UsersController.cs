using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SEDC.WebApi.Workshop.Notes.ServiceModels.UserModels;
using SEDC.WebApi.Workshop.Notes.Sevices.Interfaces;

namespace SEDC.WebApi.Workshop.Notes.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult RegisterUser([FromBody]RegisterUser request)
        {
            try
            {
                _userService.Register(request);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
