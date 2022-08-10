using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UserApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //https://localhost:7023/api/user/getall
        [HttpGet("GetAll")]
        public IActionResult GetAllUsers() 
        {
            var response = StaticDB.Users;

            if (response.Count < 1) 
            {
                return BadRequest("There are 0 users in the databse!");
            }

            return Ok(response);
        }
        //from route
        //https://localhost:7023/api/user/getuser/1
        [HttpGet("GetUser/{id}")]
        public IActionResult GetUserByIdRoute([FromRoute] int id) 
        {
            var user = StaticDB.Users.FirstOrDefault(user => user.Id == id);

            if (user == null) 
            {
                return NotFound($"User with Id {id} was not found in the database!");
            }

            return Ok(user);
        }

        //from query
        //https://localhost:7023/api/user/getuser?id=2
        [HttpGet("GetUser")]
        public IActionResult GetUserByIdQuery([FromQuery] int id) 
        {
            var user = StaticDB.Users.FirstOrDefault(user => user.Id == id);

            if (user == null)
            {
                return NotFound($"User with Id {id} was not found in the database!");
            }

            return Ok(user);
        }


        //https://localhost:7023/api/user/test/route1/viktor/route2/radmila?query1=igor&query2=123
        [HttpGet("test/route1/{route1}/route2/{route2}")]
        public IActionResult TestRoutAndQueryParameters([FromRoute] string route1,
                                                        [FromRoute] string route2,
                                                        [FromQuery] string query1,
                                                        [FromQuery] int query2) 
        {
            var response = $"{route1} - {route2} - {query1} - {query2}";
            return Ok(response);
        }
    }
}
