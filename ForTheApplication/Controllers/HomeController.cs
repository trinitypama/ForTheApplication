using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using ForTheApplication.Models;

namespace LoginApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (SessionActive() == true)
            {
                return View();
            }

            return RedirectToAction("Logout", "Session ");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private bool SessionActive()
        {
            var session = false;

            if (HttpContext.Session.GetString("SessionState") != null)
            {
                session = true;
            }

            return session;
        }
    }
}
