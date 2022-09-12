using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotesApp.Exceptions;
using NotesApp.InerfaceModels.Models;
using NotesApp.Services.Implementations;
using NotesApp.Services.Interfaces;
using Serilog;
using Serilog.Events;

namespace NotesApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public IActionResult Authenticate([FromBody] LoginModel model)
        {
            var response = _userService.Authenticate(model.Username, model.Password);
            Log.Information($"User {response.FirstName} {response.LastName} has been succesfully authenticate.");
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public IActionResult Register([FromBody] RegisterModel model)
        {

            _userService.Register(model);
            Log.Information($"User {model.FirstName} {model.LastName} has been succesfully registered.");
            return Ok("User successfully registered.");
        }

        [HttpGet("GetAllUsers")]
        public IActionResult GetAllRegisteredUsers()
        {
            return Ok(_userService.GetAllUsers());
        }
    }
}
