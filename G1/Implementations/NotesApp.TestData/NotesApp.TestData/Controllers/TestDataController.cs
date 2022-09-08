using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotesApp.TestData.Models;

namespace NotesApp.TestData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestDataController : ControllerBase
    {
        [HttpGet("testuser")]
        public IActionResult GetTestUser() 
        {
            return Ok(new TestUser
            {
                FirstName = "TestFirst",
                LastName = "TestLast",
                Username = "TestUser",
                Password = "P@ss0wrd123",
                ConfirmPassword = "P@ss0wrd123"
            });
        }
    }
}
