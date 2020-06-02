using System.Threading.Tasks;
using ForTheApplication.Data;
using ForTheApplication.Models;
using ForTheApplication.Models.ViewModels;
using ForTheApplication.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ForTheApplication.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserService _userservice;

        public LoginController(IUserService userservice)
        {
            _userservice = userservice;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Attempt(UserLoginModel user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //Todo: Of course this is not the way to find the user in the database
                    /* For testing
                    var finduser = new UserModel { Email = user.Email, PhoneNumber = "703-111111" };
                    if (user.Email.Equals("e@e.com")){ return Json(new { HasError = true, ErrorMessage = "Invalid login attempt." }); }
                    */

                    //var finduser = await _context.Users.FirstOrDefaultAsync(x => x.Email == user.Email && x.Password == user.Password);
                    var finduser =  _userservice.AuthenticateUser(user.Email, user.Password);


                    if (finduser != null)
                    {
                        return Json(new { HasError = false, finduser.Email, finduser.PhoneNumber });
                    }
                    else
                    {
                        return Json(new { HasError = true, ErrorMessage = "Invalid login attempt." });
                    }
                }
                catch
                {
                    //Todo: Log exception, Audit event
                    return Json(new { HasError = true, ErrorMessage = "Application Exception. Please try again." });
                }

            }
            return Json(new { HasError = true, ErrorMessage = "Invalid login attempt." });
        }


        [HttpPost]
        public JsonResult Verify(string email, string code)
        {

            try
            {
                //Todo: Call service to verify
                var validcode = true;

                if (validcode)
                {
                    //Start Session
                    HttpContext.Session.SetString("SessionState", email);

                    return Json(new { HasError = false, Email = email, Code = code});

                }
                else
                {
                    return Json(new { HasError = true, ErrorMessage = "Invalid login attempt." });
                }
            }
            catch
            {
                //Todo: Log exception, Audit event
                return Json(new { HasError = true, ErrorMessage = "Application Exception. Please try again." });
            }

        }
    }
}
