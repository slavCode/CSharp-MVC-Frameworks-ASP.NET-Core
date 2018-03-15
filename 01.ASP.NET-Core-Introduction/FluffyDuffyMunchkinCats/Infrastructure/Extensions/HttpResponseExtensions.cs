namespace FluffyDuffyMunchkinCats.Infrastructure.Extensions
{
    using Microsoft.AspNetCore.Http;

    public static class HttpResponseExtensions
    {
        public static void Redirect(this HttpResponse response, string url)
        {
            response.StatusCode = HttpStatusCode.Ok;
            response.Headers.Add(HttpHeader.Location, url);
        }
    }
}
