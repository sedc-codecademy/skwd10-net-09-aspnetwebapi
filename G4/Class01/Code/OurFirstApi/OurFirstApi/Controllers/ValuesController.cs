using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OurFirstApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet("strings")]
        public IEnumerable<string> GetStrings()
        {
            return new List<string> { "value1", "value2" };
        }
    }
}
