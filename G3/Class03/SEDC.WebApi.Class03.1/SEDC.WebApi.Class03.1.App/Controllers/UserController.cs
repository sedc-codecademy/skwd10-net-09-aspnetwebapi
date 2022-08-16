using Microsoft.AspNetCore.Mvc;
using SEDC.WebApi.Class03._1.App.Models;

namespace SEDC.WebApi.Class03._1.App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IEnumerable<User> _users = new List<User>
        {
            new User
            {
                Id = 1,
                Name = "Trajan"
            }
        };

        /// <summary>
        /// Returns all users
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            return Ok(_users);
        }

        [HttpGet("{name}")]
        public ActionResult<IEnumerable<UserDto>> GetUsersByName(string name)
        {
            return Ok(_users.Select(x =>
                new UserDto { UserId = x.Id, FullName = x.Name })
                );
        }

        [HttpGet("filter")]
        [ProducesResponseType(typeof(int), 200)]
        [ProducesResponseType(typeof(int), 404)]
        [ProducesResponseType(typeof(int), 400)]
        [ProducesResponseType(typeof(int), 100)]
        [ProducesErrorResponseType(typeof(User))]
        public ActionResult<IEnumerable<UserDto>> GetFilteredUsers([FromQuery]SearchInput input)
        {
            return Ok(_users.Select(x =>
                new UserDto { UserId = x.Id, FullName = x.Name })
                );
        }
    }
}
