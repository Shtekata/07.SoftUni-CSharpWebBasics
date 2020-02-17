using SIS.Http;
using SIS.Http.Response;
using SIS.Mvc.Framework;
using System.IO;

namespace SulsApp.Controllers
{
    class UsersController : Controller
    {
        public HttpResponse Login(HttpRequest request)
        {
            return View();
        }

        public HttpResponse Register(HttpRequest request)
        {
            return View();
        }
    }
}
