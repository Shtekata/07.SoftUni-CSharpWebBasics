using IRunes.App.ViewModels.Users;
using IRunes.Services;
using SIS.HTTP;
using SIS.MvcFramework;

namespace IRunes.App.Controllers
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
            string userId = string.Empty;

            if (!input.Username.Contains("@"))
            {
                userId = usersService.GetUserIdUsername(input.Username, input.Password);
            }
            else
            {
                userId = usersService.GetUserIdEmail(input.Username, input.Password);

            }

            if (userId != null)
            {
                SignIn(userId);
                return Redirect("/");
            }

                return Redirect("/Users/Login");
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
                return Error("Password mast be at least 4 characters and at most 10.");
            }

            if (input.Password.Length < 6 || input.Password.Length > 20)
            {
                return Error("Password mast be at least 6 characters and at most 20.");
            }

            if (string.IsNullOrWhiteSpace(input.Email))
            {
                return Error("Email can not be empty!");
            }

            if (input.Password != input.ConfirmPassword)
            {
                return Error("Password should match.");
            }

            if (usersService.UsernameExists(input.Username))
            {
                return Error("Username already in use.");
            }

            if (usersService.EmailExists(input.Email))
            {
                return Error("Email already in use.");
            }

            usersService.Register(input.Username, input.Email, input.Password);
            return Redirect("/Users/Login");
        }

        public HttpResponse Logout()
        {
            SignOut();
            return Redirect("/");
        }
    }
}
