using SIS.Http;
using SIS.Http.Response;
using SIS.MvcFramework;
using SulsApp.Models;
using SulsApp.ViewModels;
using System;
using System.IO;
using System.Net.Mail;
using System.Security.Cryptography;

namespace SulsApp.Controllers
{
    public class UsersController : Controller
    {
        public HttpResponse Login()
        {
            return View();
        }

        [HttpPost("/Users/Login")]
        public HttpResponse DoLogin()
        {
            return View();
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

            var user = new User
            {
                Email = email,
                Username = username,
                Password = Hash(password),
            };

            var db = new ApplicationDbContext();
            db.Users.Add(user);
            db.SaveChanges();

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
