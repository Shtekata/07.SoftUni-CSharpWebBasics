﻿using SIS.Http;
using SIS.Http.Response;
using SIS.Mvc.Framework;
using System.IO;

namespace SulsApp.Controllers
{
    public class StaticFilesController : Controller
    {
        public HttpResponse Bootstrap(HttpRequest request)
        {
            return new FileResponse(File.ReadAllBytes("wwwroot/css/bootstrap.min.css"), "text/css");
        }

        public HttpResponse Reset(HttpRequest request)
        {
            return new FileResponse(File.ReadAllBytes("wwwroot/css/reset-css.css"), "text/css");
        }

        public HttpResponse Site(HttpRequest request)
        {
            return new FileResponse(File.ReadAllBytes("wwwroot/css/site.css"), "text/css");
        }
    }
}