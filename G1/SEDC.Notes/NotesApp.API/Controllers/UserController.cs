using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotesApp.Exceptions;
using NotesApp.Services.Implementations;
using NotesApp.Services.Interfaces;
using SEDC.Notes.InerfaceModels.Models;
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
            try
            {
                var response = _userService.Authenticate(model.Username, model.Password);
                Log.Information($"User {response.FirstName} {response.LastName} has been succesfully authenticate.");
                return Ok(response);
            }
            catch (UserException ex)
            {
                Log.Error("USER {userId}.{name}: {message}", ex.UserId, ex.Name, ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Write(LogEventLevel.Fatal, ex.Message);
                return BadRequest("Something went wrong. Please contact customer support.");
            }
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public IActionResult Register([FromBody] RegisterModel model) 
        {
            try
            {
                _userService.Register(model);
                Log.Information($"User {model.FirstName} {model.LastName} has been succesfully registered.");
                return Ok("User successfully registered.");
            }
            catch (UserException ex)
            {
                Log.Error("USER {userId}.{name}: {message}", ex.UserId, ex.Name, ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Write(LogEventLevel.Fatal, ex.Message);
                return BadRequest("Something went wrong. Please contact customer support.");
            }
        }

        [HttpGet("GetAllUsers")]
        public IActionResult GetAllRegisteredUsers() 
        {
            try
            {
                return Ok(_userService.GetAllUsers());
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong. Please contact customer support.");
            }
        }
    }
}
