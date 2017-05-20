using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SwagApiMvcTest.Controllers {
    [ApiVersion("1.0")]
    [Route("api/v{api-version:apiVersion}/TestApi")]
    [Produces("application/json")]
    public class TestApiController : Controller {
        [Route("GetData")]
        [HttpGet]
        public IActionResult GetData() {
            string response = "This is a test message";
            return Ok(response);
        }
    }
}