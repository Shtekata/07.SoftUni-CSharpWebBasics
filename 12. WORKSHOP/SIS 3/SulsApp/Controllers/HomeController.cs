using SIS.Http;
using SIS.Http.Logging;
using SIS.MvcFramework;
using SulsApp.Services;
using SulsApp.ViewModels;
using SulsApp.ViewModels.Home;
using System;
using System.Linq;

namespace SulsApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger logger;
        private readonly ApplicationDbContext db;

        public HomeController(ILogger logger, ApplicationDbContext db)
        {
            this.logger = logger;
            this.db = db;
        }
        [HttpGet("/")]
        public HttpResponse Index()
        {
            //logger.Log("Hello from Index");
            if (IsUserLoggedIn())
            {
                var problems = db.Problems.Select(x => new IndexProblemViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Count = x.Submissions.Count(),
                }).ToList();

                var loggedInViewModel = new LoggedInViewModel
                {
                    Problems = problems
                };

                return View(loggedInViewModel, "IndexLoggedIn");
            }

            var viewModel = new IndexViewModel
            {
                Message = "Welcome to SULS Platform!",
                Year = DateTime.UtcNow.Year,
            };

            return View(viewModel);
        }
    }
}
