using System.Threading.Tasks;
using ForTheApplication.Data;
using ForTheApplication.Models;
using ForTheApplication.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ForTheApplication.Services;

namespace ForTheApplication.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly IUserService _userservice;

        public RegistrationController(IUserService userservice)
        {

            _userservice = userservice;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Register(UserRegistrationModel user)
        {

            var newuser = new UserModel { Email = user.Email, Password = user.Password, PhoneNumber = user.PhoneNumber };

            try
            {
                //var finduser = await _context.Users.FirstOrDefaultAsync(x => x.Email == user.Email);
                var finduser = _userservice.FindUser(user.Email);

                if (finduser != null)
                {
                    return Json(new { hasError = true, errorMessage = "Email already exists. Please choose a different one to register." });
                }

                return Json(new { hasError = false, newuser.Email, newuser.PhoneNumber });
            }
            catch
            {
                //Todo: Log exception, Audit event
                return Json(new { hasError = true, errorMessage = "Application Exception. Please try again." });
            }

        }

        [HttpPost]
        public JsonResult Verify(UserRegistrationModel user, string code)
        {
            var newuser = new UserModel { Email = user.Email, Password = user.Password, PhoneNumber = user.PhoneNumber };

            try
            {
                //Todo: Call service to verify
                var validcode = true;

                if (validcode)
                {
                    //Todo: Save to db, save in memory for now.
                    //_context.Add(newuser);
                    //await _context.SaveChangesAsync();
                    var createduser = _userservice.CreateUser(newuser);

                    //Start Session
                    HttpContext.Session.SetString("SessionState", createduser.Email);

                    return Json(new { HasError = false, Email = createduser.Email, Code = code });

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
