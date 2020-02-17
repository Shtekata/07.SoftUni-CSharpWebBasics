using SIS.Http;
using SIS.Http.Logging;
using SIS.Http.Response;
using SIS.MvcFramework;
using SulsApp.Models;
using SulsApp.Services;
using SulsApp.ViewModels;
using System;
using System.IO;
using System.Net.Mail;
using System.Security.Cryptography;

namespace SulsApp.Controllers
{
    public class UsersController : Controller
    {
        private IUsersService usersService;
        private ILogger logger;

        public UsersController(IUsersService usersService, ILogger logger)
        {
            this.usersService = usersService;
            this.logger = logger;
        }
        public HttpResponse Login()
        {
            return View();
        }

        [HttpPost("/Users/Login")]
        public HttpResponse DoLogin()
        {
            var username = Request.FormData["username"];
            var password = Request.FormData["password"];

            var userId = usersService.GetUserId(username, password);
            if (userId == null)
            {
                return Redirect("/Users/Login");
            }

            SignIn(userId);
            logger.Log("User logged in:" + username);
            return Redirect("/");
        }

        public HttpResponse Register()
        {
            return View();
        }

        [HttpPost("/Users/Register")]
        public HttpResponse DoRegister()
        {
            var username = Request.FormData["username"];
            var email = Request.FormData["email"];
            var password = Request.FormData["password"];
            var confirmPassword = Request.FormData["confirmPassword"];

            if (password != confirmPassword)
            {
                return Error("Passwords should be the same!");
            }

            if(username?.Length < 5 || username?.Length > 20)
            {
                return Error("Username should be between 5 and 20 characters.");
            }

            if (password?.Length < 6 || password?.Length > 20)
            {
                return Error("Password should be between 6 and 20 characters.");
            }

            if (!IsValid(email))
            {
                return Error("Invalid email!");
            }

            usersService.CreateUser(username, email, password);
            logger.Log("New user: " + username);
            return Redirect("/Users/Login");
        }

        public HttpResponse LogOut()
        {
            SignOut();
            return Redirect("/");
        }
       
        private bool IsValid(string emailaddress)
        {
            try
            {
                new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
