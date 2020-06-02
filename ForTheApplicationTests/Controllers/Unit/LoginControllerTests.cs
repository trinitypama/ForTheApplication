using Microsoft.VisualStudio.TestTools.UnitTesting;
using ForTheApplication.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using ForTheApplication.Models;
using Moq;
using ForTheApplication.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq;
using ForTheApplication.Models.ViewModels;
using ForTheApplication.Services;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ForTheApplication.Controllers.Tests
{
    [TestClass()]
    public class LoginControllerTests
    {
        [TestMethod()]
        public void Attempt_ReturnsCorrectPhoneNumberInResponse_OnValidLoginAttempt()
        {
            var user1 = new UserModel { Email = "test1@email.com", Password = "Password1", PhoneNumber = "703-11111" };
            var user2 = new UserModel { Email = "test2@email.com", Password = "Password2", PhoneNumber = "703-22222" };

            var models = new List<UserModel> { user1, user2 };

            var userservice = new Mock<IUserService>();

            userservice.Setup(x => x.AuthenticateUser(user1.Email, user1.Password)).Returns(user1);

            var controller = new LoginController(userservice.Object);

            var result = controller.Attempt(new UserLoginModel { Email = user1.Email, Password = user1.Password });

            //dynamic data = JObject.Parse(result.Value.ToString());
            var serializedresult = JToken.Parse(JsonConvert.SerializeObject(result.Value));
            string phonenumber = ExtractKeyStringValue(serializedresult, "PhoneNumber");
            string email = ExtractKeyStringValue(serializedresult, "Email");

            Assert.AreEqual(user1.PhoneNumber, phonenumber);
            Assert.AreEqual(user1.Email, email);
        }

        private static string ExtractKeyStringValue(JToken serializedresult, string key)
        {
            return ((JValue)serializedresult[key]).Value.ToString();
        }
    }
}