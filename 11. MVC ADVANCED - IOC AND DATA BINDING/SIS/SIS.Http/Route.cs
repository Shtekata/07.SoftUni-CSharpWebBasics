using System;

namespace SIS.Http
{
    public class Route
    {
        public Route(HttpMethodType httpMethod, string path, Func<HttpRequest, HttpResponse> action)
        {
            HttpMethod = httpMethod;
            Path = path;
            Action = action;
        }
        public string Path { get; set; }
        public HttpMethodType HttpMethod { get; set; }
        public Func<HttpRequest, HttpResponse> Action { get; set; }

        public override string ToString()
        {
            return $"{HttpMethod} => {Path}";
        }
    }
}