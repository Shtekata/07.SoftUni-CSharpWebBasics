using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace SIS.Http
{
    public class HttpRequest
    {
        public HttpRequest(string httpRequestAsString)
        {
            Headers = new List<Header>();
            Cookies = new List<Cookie>();
            SessionData = new Dictionary<string, string>();

            var lines = httpRequestAsString.Split(new string[] { HttpConstants.NewLine }, StringSplitOptions.None);
            var httpInfoHeader = lines[0];
            var infoHeaderParts = httpInfoHeader.Split(' ');
            if (infoHeaderParts.Length != 3)
            {
                throw new HttpServerException("Invalid HTTP header line.");
            }

            var httpMethod = infoHeaderParts[0];
            //Enum.TryParse(httpMethod, out HttpMethodType type);
            Method = httpMethod switch
            {
                "GET" => HttpMethodType.Get,
                "POST" => HttpMethodType.Post,
                "PUT" => HttpMethodType.Put,
                "DELETE" => HttpMethodType.Delete,
                _ => HttpMethodType.Unknown
            };

            Path = infoHeaderParts[1];

            var httpVersion = infoHeaderParts[2];
            Version = httpVersion switch
            {
                "Http10" => HttpVersionType.Http10,
                "Http11" => HttpVersionType.Http11,
                "Http20" => HttpVersionType.Http20,
                _ => HttpVersionType.Http11
            };

            bool isInHeader = true;
            var bodyBuilder = new StringBuilder();
            for (int i = 1; i < lines.Length; i++)
            {
                var line = lines[i];
                if (string.IsNullOrWhiteSpace(line))
                {
                    isInHeader = false;
                    continue;
                }

                if (isInHeader)
                {
                    var headerParts = line.Split(new string[] { ": " }, 2, StringSplitOptions.None);
                    if (headerParts.Length != 2)
                    {
                        throw new HttpServerException($"Invalid header: {line}");
                    }

                    var header = new Header(headerParts[0], headerParts[1]);
                    Headers.Add(header);

                    if (headerParts[0] == "Cookie")
                    {
                        var cookiesAsString = headerParts[1];
                        var cookies = cookiesAsString.Split(new string[] { "; " }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var cookieAsString in cookies)
                        {
                            var cookieParts = cookieAsString.Split(new char[] { '=' }, 2);
                            if (cookieParts.Length == 2)
                            {
                                Cookies.Add(new Cookie(cookieParts[0], cookieParts[1]));
                            }
                        }
                    }
                }
                else
                {
                    bodyBuilder.AppendLine(line);
                }
            }
            Body = bodyBuilder.ToString().Trim('\r', '\n');
            FormData = new Dictionary<string, string>();
            ParseData(FormData, Body);

            Query = string.Empty;
            if (Path.Contains("?"))
            {
                var parts = Path.Split(new char[] { '?' }, 2);
                Path = parts[0];
                Query = parts[1];
            }

            QueryData = new Dictionary<string, string>();
            ParseData(QueryData, Query);
        }

        private void ParseData(IDictionary<string, string> output, string input)
        {
            var dataParts = input.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var dataPart in dataParts)
            {
                var parameterParts = dataPart.Split(new char[] { '=' }, 2);
                output.Add(HttpUtility.UrlDecode(parameterParts[0]), HttpUtility.UrlDecode(parameterParts[1]));
            }
        }

        public HttpMethodType Method { get; set; }

        public string Path { get; set; }

        public HttpVersionType Version { get; set; }

        public IList<Header> Headers { get; set; }

        public IList<Cookie> Cookies { get; set; }

        public string Body { get; set; }

        public IDictionary<string, string> FormData { get; set; }

        public string Query { get; set; }

        public IDictionary<string, string> QueryData { get; set; }

        public IDictionary<string, string> SessionData { get; set; }
    }
}
