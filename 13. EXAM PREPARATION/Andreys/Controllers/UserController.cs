using Andreys.Services;
using Andreys.ViewModels.Users;
using SIS.HTTP;
using SIS.MvcFramework;
using System;
using System.Net.Mail;

namespace Andreys.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }
        public HttpResponse Login()
        {
            return View();
        }

        [HttpPost]
        public HttpResponse Login(LoginInputModel input)
        {
            var userId = usersService.GetUserId(input.Username, input.Password);
            if (userId != null)
            {
                SignIn(userId);
                return Redirect("/");
            }

            return Redirect("/Users/Login");
        }

        [HttpGet("/Logout")]
        public HttpResponse Logout()
        {
            SignOut();
            return Redirect("/");
        }

        public HttpResponse Register()
        {
            return View();
        }

        [HttpPost]
        public HttpResponse Register(RegisterInputModel input)
        {
            if (input.Username.Length < 4 || input.Username.Length > 10)
            {
                return Redirect("/Users/Register");
            }

            if (input.Password.Length < 6 || input.Password.Length > 20)
            {
                return Redirect("/Users/Register");
            }

            //if (!IsValid(input.Email))
            //{
            //    return Error("Invalid email!");
            //}

            //if (string.IsNullOrWhiteSpace(input.Email))
            //{
            //    return Error("Email can not be empty!");
            //}

            if (input.Password != input.ConfirmPassword)
            {
                return Redirect("/Users/Register");
            }

            if (usersService.UsernameExists(input.Username))
            {
                return Redirect("/Users/Register");
            }

            if (usersService.EmailExists(input.Email))
            {
                return Redirect("/Users/Register");
            }

            usersService.Register(input.Username, input.Email, input.Password);
            return Redirect("/Users/Login");
        }

        //private bool IsValid(string emailaddress)
        //{
        //    try
        //    {
        //        new MailAddress(emailaddress);

        //        return true;
        //    }
        //    catch (FormatException)
        //    {
        //        return false;
        //    }
        //}
    }
}
