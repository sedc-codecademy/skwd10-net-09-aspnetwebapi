using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NotesApp.Configurations;
using NotesApp.InerfaceModels.Models;
using NotesApp.Services.Interfaces;
using System.Diagnostics;

namespace NotesApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExternalController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly IUserService _userService;
        private readonly INoteService _noteService;
        private readonly AppSettings _appsettings;

        public ExternalController(IUserService userService, 
                                  INoteService noteService,
                                  IOptions<AppSettings> options)
        {
            _httpClient = new HttpClient();
            _userService = userService;
            _noteService = noteService;
            _appsettings = options.Value;
        }

        [HttpGet("registerTestUser")]
        public IActionResult RegisterTestUser() 
        {
            HttpResponseMessage response = _httpClient.GetAsync(_appsettings.TestDataApi).Result;
            string responseBody = response.Content.ReadAsStringAsync().Result;
            RegisterModel model = JsonConvert.DeserializeObject<RegisterModel>(responseBody);
            _userService.Register(model);

            return Ok(model);
        }

        [HttpGet("performance/getnote")]
        public IActionResult GetNotePerformance() 
        {
            var stopwatch = Stopwatch.StartNew();

            for (int i = 0; i < 10000; i++)
            {
                _noteService.GetAll();
            }

            stopwatch.Stop();
            var elapsed = stopwatch.Elapsed.Seconds;
 
            return Ok(elapsed);
        }

    }
}
