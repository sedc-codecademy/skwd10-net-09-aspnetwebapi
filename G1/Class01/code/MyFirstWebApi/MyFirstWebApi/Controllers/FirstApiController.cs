using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyFirstWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FirstApiController : ControllerBase
    {

        [HttpGet("GetFlavours")]
        public IActionResult GetAllFlavours() 
        {
            var flavours = new List<string> { "Vanilla", "Banana", "Orange", "Strawberry" };
            return Ok(flavours);
        }

        [HttpGet("GetFlavourDetails")]
        public IActionResult GetDetails([FromQuery] int iceCreamId)
        {
            var flavour1 = new IceCream
            {
                Id = 1,
                Flavour = "Vanilla",
                Quantity = 10
            };

            var flavour2 = new IceCream
            {
                Id = 2,
                Flavour = "Banana",
                Quantity = 3
            };

            if (iceCreamId == 1) 
            {
                return Ok(flavour1);
            }

            if (iceCreamId == 2)
            {
                return Ok(flavour2);
            }

            return NotFound("There is no flavour with this id!");

        }

    }

    public class IceCream
    {
        public int Id { get; set; }
        public string? Flavour { get; set; }
        public int Quantity { get; set; }
    }

}


