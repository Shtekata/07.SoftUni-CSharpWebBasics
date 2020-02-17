namespace SIS.Http.Response
{
    public class RedirectResponse : HttpResponse
    {
        public RedirectResponse(string newLocation)
        {
            Headers.Add(new Header("Location", newLocation));
            StatusCode = HttpResponseCode.Found;
        }
    }
}
