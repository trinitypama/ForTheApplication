using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ForTheApplication.Controllers
{
    public class SessionController : Controller
    {

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return View();
        }
    }
}
