using SIS.Http;
using SIS.Http.Response;
using System.IO;
using System.Runtime.CompilerServices;

namespace SIS.Mvc.Framework
{
    public abstract class Controller
    {
        protected HttpResponse View([CallerMemberName]string viewName = null)
        {
            var layout = File.ReadAllText("Views/Shared/_Layout.html");
            var controllerName = GetType().Name.Replace("Controller", "/");
            var html = File.ReadAllText("Views/" + controllerName + viewName + ".html");
            var bodyWithLayout = layout.Replace("@RenderBody()", html);
            return new HtmlResponse(bodyWithLayout);
        }
    }
}
