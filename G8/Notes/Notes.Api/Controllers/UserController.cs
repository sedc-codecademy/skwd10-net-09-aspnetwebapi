using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Notes.Application.Models;
using Notes.Application.Services;

namespace Notes.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("register")]// api/v1/user/regiter
        public ActionResult Register(CreateUserModel model)
        {
            var user = userService.CreateUser(model);
            return Created("api/v1/user/login", user);
        }

        [HttpPost("{id}/change-password")]
        public ActionResult ChangePassword(ChangePasswordModel model, int id)
        {
            userService.ChangePassword(model, id);
            return Ok();
        }

        [HttpPost("forgot-password")]
        public ActionResult ForgotPassword(ForgotPasswordModel model)
        {
            var url = $"https://{Request.Host.Value}";
            userService.ForgotPassword(model, url);
            return Ok();
        }

        [HttpGet("code/{code}")] // api/v1/user/code/{code}
        public ActionResult GetUserByCode(string code)
        {
            var user = userService.GetUserByCode(code);
            return Ok(user);
        }

        [HttpPost("code/{code}")]
        public ActionResult UpdatePasswordByCode(UpdatePasswordModel model, string code)
        {
            userService.UpdatePasswordByCode(model, code);
            return RedirectToAction("login");
        }
    }
}
