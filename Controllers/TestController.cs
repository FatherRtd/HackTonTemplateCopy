using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HackTonTemplate.Models;

namespace HackTonTemplate.Controllers
{
    public class TestController : Controller
    {
        private readonly ILogger<TestController> _logger;

        public TestController(ILogger<TestController> logger)
        {
            _logger = logger;
        }

        [Authorize]
        [Route("api/Test/GetTest")]
        [HttpGet]
        public async Task<IActionResult> GetTest()
        {
            var test = new Test
            {
                Value = "Памагите пожалуйста!"
            };

            return new JsonResult(test);
        }

    }
}
