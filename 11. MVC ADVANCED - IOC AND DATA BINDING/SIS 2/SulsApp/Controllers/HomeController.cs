using SIS.Http;
using SIS.Http.Logging;
using SIS.MvcFramework;
using SulsApp.Services;
using SulsApp.ViewModels;
using SulsApp.ViewModels.Home;
using System;

namespace SulsApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger logger;

        public HomeController(ILogger logger)
        {
            this.logger = logger;
        }
        [HttpGet("/")]
        public HttpResponse Index()
        {
            //logger.Log("Hello from Index");
            var viewModel = new IndexViewModel
            {
                Message = "Welcome to SULS Platform!",
                Year = DateTime.UtcNow.Year,
            };

            return View(viewModel);
        }
    }
}
