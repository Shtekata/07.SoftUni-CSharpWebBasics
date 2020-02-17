using System.Text;

namespace SIS.Http.Response
{
    public class HtmlResponse : HttpResponse
    {
        public HtmlResponse(string html) 
            : base()
        {
            StatusCode = HttpResponseCode.Ok;
            byte[] byteData = Encoding.UTF8.GetBytes(html);
            Body = byteData;
            Headers.Add(new Header("Content-Type", "text/html"));
            Headers.Add(new Header("Content-Length", Body.Length.ToString()));
        }
    }
}
