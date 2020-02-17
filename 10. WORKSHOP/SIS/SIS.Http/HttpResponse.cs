using System.Collections.Generic;
using System.Text;

namespace SIS.Http
{
    public class HttpResponse
    {
        public HttpResponse(HttpResponseCode statusCode, byte[] body)
            :this()
        {
            StatusCode = statusCode;
            Body = body;
            if (body?.Length > 0)
            {
                Headers.Add(new Header("Content-Length", body.Length.ToString()));
            }
        }

        internal HttpResponse()
        {
            Version = HttpVersionType.Http10;
            Headers = new List<Header>();
            Cookies = new List<ResponseCookie>();
        }
        public HttpVersionType Version { get; set; }

        public HttpResponseCode StatusCode { get; set; }

        public IList<Header> Headers { get; set; }

        public IList<ResponseCookie> Cookies { get; set; }

        public byte[] Body { get; set; }

        public override string ToString()
        {
            var responseAsString = new StringBuilder();
            var httpVersionAsStrind = Version switch
            {
                HttpVersionType.Http10 => "HTTP/1.0",
                HttpVersionType.Http11 => "HTTP/1.1",
                HttpVersionType.Http20 => "HTTP/2.0",
                _ => "HTTP/1.1",
            };

            responseAsString.Append($"{httpVersionAsStrind} {(int)StatusCode} {StatusCode}" + HttpConstants.NewLine);
            foreach (var header in Headers)
            {
                responseAsString.Append(header.ToString() + HttpConstants.NewLine);
            }

            foreach (var cookie in Cookies)
            {
                responseAsString.Append($"Set-Cookie: " + cookie.ToString() + HttpConstants.NewLine);
            }

            responseAsString.Append(HttpConstants.NewLine);

            return responseAsString.ToString();
        }
    }
}
