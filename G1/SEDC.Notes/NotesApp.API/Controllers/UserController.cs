using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotesApp.Services.Implementations;
using NotesApp.Services.Interfaces;
using SEDC.Notes.InerfaceModels.Models;

namespace NotesApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Authenticate")]
        public IActionResult Authenticate([FromBody] LoginModel model) 
        {
            var response = _userService.Authenticate(model.Username, model.Password);
            return Ok(response);
        }

        [HttpPost("Register")]
        public IActionResult Register([FromBody] RegisterModel model) 
        {
            _userService.Register(model);
            return Ok();
        }

        [HttpGet("GetAllUsers")]
        public IActionResult GetAllRegisteredUsers() 
        {
            return Ok(_userService.GetAllUsers());
        }
    }
}
