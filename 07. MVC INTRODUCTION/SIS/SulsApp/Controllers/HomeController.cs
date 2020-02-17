using SIS.Http;
using SIS.Http.Response;
using SIS.Mvc.Framework;
using System.IO;

namespace SulsApp.Controllers
{
    class HomeController : Controller
    {
        public HttpResponse Index(HttpRequest request)
        {
            return View();
        }
    }
}
