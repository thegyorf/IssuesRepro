using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MvcApiTest.Controllers {
    [ApiVersionNeutral]
    [Route("")]
    [Route("home")]
    public class HomeController : Controller {
        [Route("")]
        [Route("index")]
        public IActionResult Index() {
            return View();
        }

        [Route("about")]
        public IActionResult About() {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [Route("contact")]
        public IActionResult Contact() {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        [Route("error")]
        public IActionResult Error() {
            return View();
        }
    }
}
