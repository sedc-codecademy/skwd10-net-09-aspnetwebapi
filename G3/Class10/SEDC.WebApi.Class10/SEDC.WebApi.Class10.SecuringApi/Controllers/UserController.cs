using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SEDC.WebApi.Class10.SecuringApi.Models;
using SEDC.WebApi.Class10.SecuringApi.Services;

namespace SEDC.WebApi.Class10.SecuringApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public ActionResult Register([FromBody]RegisterModel request)
        {
            try
            {
                _userService.Register(request);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult<UserDto> Login(LoginModel request)
        {
            try
            {
                var user = _userService.Login(request);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }
    }
}
