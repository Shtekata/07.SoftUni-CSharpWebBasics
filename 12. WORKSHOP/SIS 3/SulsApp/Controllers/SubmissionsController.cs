using SIS.Http;
using SIS.MvcFramework;
using SulsApp.Models;
using SulsApp.Services;
using SulsApp.ViewModels.Submissions;
using System;
using System.Linq;

namespace SulsApp.Controllers
{
    public class SubmissionsController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly ISubmissionsService submissionsService;

        public SubmissionsController(ApplicationDbContext db, ISubmissionsService submissionsService)
        {
            this.db = db;
            this.submissionsService = submissionsService;
        }
        public HttpResponse Create(string id)
        {
            if (!IsUserLoggedIn())
            {
                return Redirect("/Users/Login");
            }

            var problem = db.Problems
                .Where(x => x.Id == id)
                .Select(x => new CreateFormViewModel
                {
                    Name = x.Name,
                    ProblemId = x.Id
                }).FirstOrDefault();
            if (problem == null)
            {
                return Error("Problem not found!");
            }

            return View(problem);
        }

        [HttpPost]
        public HttpResponse Create(string problemId, string code)
        {
            if (!IsUserLoggedIn())
            {
                return Redirect("/Users/Login");
            }

            if (code == null || code.Length < 30)
            {
                return Error("Please provide code with at least 30 characters.");
            }

            submissionsService.Create(User, problemId, code);

            return Redirect("/");
        }
        public HttpResponse Delete(string id)
        {
            if (!IsUserLoggedIn())
            {
                return Redirect("/Users/Login");
            }

            submissionsService.Delete(id);
           
            return Redirect("/");
        }
    }
}
