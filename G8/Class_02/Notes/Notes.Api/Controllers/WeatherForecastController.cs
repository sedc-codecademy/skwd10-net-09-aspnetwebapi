using Microsoft.AspNetCore.Mvc;

namespace Notes.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IServiceA serviceA;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IServiceA serviceA)
        {
            _logger = logger;
            this.serviceA = serviceA;
        }
        //resource/{id}/sub-resource/{subId}/{child-resource}/{id}

        //resource => vraka site resource
        //resource/{id} => vraka eden resource
        //resource/{id}/sub-resource => vraka za resourse so id site sub resource -> pizza/1/slice

        //[HttpGet()] // api/v1/WeatherForecast
        //[HttpGet("{id:int}")] // api/v1/WeatherForecast/1
        //[HttpGet("{id:string}")] // api/v1/WeatherForecast/asdasd
        //[HttpGet("{id:int}/city/{cityId:int}")] // api/v1/WeatherForecast/1/city/2

        //public IEnumerable<WeatherForecast> Get()
        //{
        //    if (broj < 0)
        //    {
        //        return BadRequest();
        //    }
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Date = DateTime.Now.AddDays(index),
        //        TemperatureC = Random.Shared.Next(-20, 55),
        //        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        //    })
        //    .ToArray();
        //}

        [HttpPatch(Name = "GetWeatherForecast2")]
        public ActionResult<IEnumerable<WeatherForecast>> Get2(int broj)
        {
            if(broj < 0)
            {
                return BadRequest();
            }
        }
    }
}