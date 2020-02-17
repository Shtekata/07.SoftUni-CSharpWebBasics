using SIS.Http;
using SIS.Http.Logging;
using SIS.Http.Response;
using SIS.MvcFramework;
using SulsApp.Models;
using SulsApp.Services;
using SulsApp.ViewModels;
using SulsApp.ViewModels.Users;
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
            if (IsUserLoggedIn())
            {
                return Redirect("/");
            }

            return View();
        }

        [HttpPost]
        public HttpResponse Login(string username, string password)
        {
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

        [HttpPost]
        public HttpResponse Register(RegisterInputModel input)
        {
            if (input.Password != input.ConfirmPassword)
            {
                return Error("Passwords should be the same!");
            }

            if(input.Username?.Length < 5 || input.Username?.Length > 20)
            {
                return Error("Username should be between 5 and 20 characters.");
            }

            if (input.Password?.Length < 6 || input.Password?.Length > 20)
            {
                return Error("Password should be between 6 and 20 characters.");
            }

            if (!IsValid(input.Email))
            {
                return Error("Invalid email!");
            }

            if (usersService.IsUsernameUsed(input.Username))
            {
                return Error("Username already used!");
            }

            if (usersService.IsEmailUsed(input.Email))
            {
                return Error("Email already used!");
            }

            usersService.CreateUser(input.Username, input.Email, input.Password);
            logger.Log("New user: " + input.Username);
            return Redirect("/Users/Login");
        }

        public HttpResponse LogOut()
        {
            if (!IsUserLoggedIn())
            {
                return Redirect("/Users/Login");
            }

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
