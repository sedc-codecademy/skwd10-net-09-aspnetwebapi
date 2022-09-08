using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Notes.Application;
using Notes.Application.Models;
using Notes.Application.Services;
using System.Security.Claims;

namespace Notes.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public UserController(IUserService userService, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userService = userService;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpPost("register")]// api/v1/user/regiter
        public async Task<ActionResult> Register(CreateUserModel model)
        {
            var user = new IdentityUser();
            var result = await userManager.CreateAsync(user, model.Password);
            await userManager.AddToRoleAsync(user, NoteRoles.User);
            //var user = userService.CreateUser(model);
            return Created("api/v1/user/login", user);
        }

        [HttpPost("{id}/change-password")]// api/v1/user/1/change-password
        public ActionResult ChangePassword(ChangePasswordModel model, int id)
        {
            userService.ChangePassword(model, id);
            return Ok();
        }

        [HttpPost("forgot-password")] // api/v1/user/forgot-password
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

        [HttpPost("login")]
        public async Task<ActionResult> Login(UserLoginModel model)
        {
            //var user = userService.PasswordLogin(model);
            //var identities = new List<ClaimsIdentity> // 
            //{
            //    new ClaimsIdentity(new List<Claim>
            //        {
            //            new Claim(ClaimTypes.Name, user.Name),
            //            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            //            new Claim(ClaimTypes.Role, NoteRoles.User)
            //        },
            //        CookieAuthenticationDefaults.AuthenticationScheme)
            //};
            //var principal = new ClaimsPrincipal(identities); // <-- userot
            //await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            Microsoft.AspNetCore.Identity.SignInResult? result = await signInManager.PasswordSignInAsync(model.UsernameOrEmail, model.Password, model.RememberMe, lockoutOnFailure: true);
            if (result.Succeeded)
            {
                return Ok();
            }
            return Unauthorized();
        }
    }
}
