using SIS.Http;
using SIS.MvcFramework;
using SulsApp.Services;
using SulsApp.ViewModels.Problems;
using System.Linq;

namespace SulsApp.Controllers
{
    public class ProblemsController : Controller
    {
        private readonly IProblemService problemService;
        private readonly ApplicationDbContext db;

        public ProblemsController(IProblemService problemService, ApplicationDbContext db)
        {
            this.problemService = problemService;
            this.db = db;
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

        public HttpResponse Details(string id)
        {
            if (!IsUserLoggedIn())
            {
                return Redirect("/Users/Login");
            }

            var viewModel = db.Problems.Where(x => x.Id == id).Select(x => new DetailsViewModel
            {
                Name = x.Name,
                Problems = x.Submissions.Select(y => new ProblemDetailsSubmissionViewModel
                {
                    CreatedOn = y.CreatedOn,
                    AchievedResult = y.AchievedResult,
                    SubmissionId = y.Id,
                    MaxPoints = x.Points,
                    Username = y.User.Username
                })
            }).FirstOrDefault();

            return View(viewModel);
        }

      
    }

}
