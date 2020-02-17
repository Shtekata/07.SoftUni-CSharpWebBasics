using IRunes.App.ViewModels.Home;
using IRunes.Services;
using SIS.HTTP;
using SIS.MvcFramework;

namespace IRunes.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUsersService usersService;

        public HomeController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        [HttpGet("/")]
        public HttpResponse Index()
        {
            if (IsUserLoggedIn())
            {
                var viewModel = new IndexViewModel();
                viewModel.Username= usersService.GetUserName(User);
                return View(viewModel, "Home");
            }
            return View();
        }

        [HttpGet("/Home/Index")]
        public HttpResponse IndexFullPage()
        {
            return Index();
        }

    }
}
