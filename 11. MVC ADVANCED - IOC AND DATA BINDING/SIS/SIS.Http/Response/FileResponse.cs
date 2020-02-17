namespace SIS.Http.Response
{
    public class FileResponse : HttpResponse
    {
        public FileResponse(byte[] fileContent, string contentType)
        {
            StatusCode = HttpResponseCode.Ok;
            Body = fileContent;
            Headers.Add(new Header("Content-Type", contentType));
            Headers.Add(new Header("Content-Length", Body.Length.ToString()));
        }
    }
}
