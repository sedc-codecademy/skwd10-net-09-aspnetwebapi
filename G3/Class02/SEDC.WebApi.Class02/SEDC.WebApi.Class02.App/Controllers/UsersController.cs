using Microsoft.AspNetCore.Mvc;
using SEDC.WebApi.Class02.App.Models;

namespace SEDC.WebApi.Class02.App.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly List<User> _users = new List<User>()
        {
            new User
            {
                Id = 1,
                Name = "Trajan",
                Age = 33,
                Gender = "m"
            },
            new User
            {
                Id = 2,
                Name = "Vlatko",
                Age = 23,
                Gender = "m"
            },
            new User
            {
                Id = 3,
                Name = "Stefan",
                Age = 29
            },
            new User
            {
                Id = 4,
                Name = "Aneta",
                Age = 31,
                Gender = "F"
            },
            new User
            {
                Id = 5,
                Name = "Aleksandar",
                Age = 18,
                Gender = "m"
            }
        };

        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            if (id < 1)
            {
                return BadRequest(id);
            }

            var user = _users.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return NotFound($"The user with id {id} does not exists");
            }

            return Ok(user);
        }

        [HttpGet("{name}/age/{age}")]
        public ActionResult<IEnumerable<User>> GetUsersByNameAndAge(string name, int age)
        {
            var users = _users
                .Where(x => x
                            .Name
                            .Equals(name, StringComparison.InvariantCultureIgnoreCase))
                .Where(x => x.Age == age);

            return Ok(users);
        }

        // this is the same route as the one above
        //[HttpGet("{gender}/age/{age}")]
        [HttpGet("gender/{gender}/age/{age}")]
        public ActionResult GetUsersByGenderAndAge(string gender, int age)
        {
            return Ok();
        }
    }
}

// movie model and movie controller
// id, title, year, genre, rating
// routes
// id
// title
// year from-to
// genre, year
// rating from-to