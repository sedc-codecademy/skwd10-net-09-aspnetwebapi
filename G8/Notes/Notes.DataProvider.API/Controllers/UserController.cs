using AutoBogus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Notes.DataProvider.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet("random")]
        public ActionResult GetRandomUsers()
        {
            var random = new Random();
            var number = random.Next(5, 10);
            var faker = new AutoFaker<Models.User>();
            var users = Enumerable.Range(0, number)
                .Select(x => faker.Generate())
                .ToList();
            return Ok(users);
        }
    }
}
