using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SEDC.WebApi.Workshop.Notes.ServiceModels.UserModels;
using SEDC.WebApi.Workshop.Notes.Sevices.Interfaces;

namespace SEDC.WebApi.Workshop.Notes.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("register")]
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

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public ActionResult<UserLoginDto> Login([FromBody]LoginModel request)
        {
            try
            {
                var user = _userService.Login(request);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
