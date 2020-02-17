using SIS.Http;
using SIS.MvcFramework;
using SulsApp.Services;

namespace SulsApp.Controllers
{
    public class ProblemsController : Controller
    {
        private readonly IProblemService problemService;

        public ProblemsController(IProblemService problemService)
        {
            this.problemService = problemService;
        }
        public HttpResponse Create()
        {
            if (!IsUserLoggedIn())
            {
                return Redirect("/Users/Login");
            }

            return View();
        }

        [HttpPost]
        public HttpResponse Create(string name, int points)
        {
            if (!IsUserLoggedIn())
            {
                return Redirect("/Users/Login");
            }

            if (string.IsNullOrEmpty(name))
            {
                return Error("Invalid name!");
            }

            if (points <= 0 || points > 100)
            {
                return Error("Points range [1-100]");
            }

            problemService.CreateProblem(name, points);
            return Redirect("/");
        }

    }

}
