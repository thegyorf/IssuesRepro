using Microsoft.AspNetCore.Mvc;

namespace MvcApiTest.Controllers {
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/")]
    public class ApiController : Controller {
        [Route("test")]
        [HttpGet]
        public IActionResult Test() {
            var message = "Test Message";
            return Ok(message);
        }
    }
}