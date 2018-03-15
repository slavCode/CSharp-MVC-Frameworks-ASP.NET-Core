namespace FluffyDuffyMunchkinCats.Handlers
{
    using Infrastructure;
    using Microsoft.AspNetCore.Http;

    public static class NotFoundHandler
    {
        public static RequestDelegate RequestHandler()
        {
            return async (context) =>
            {
                context.Response.StatusCode = HttpStatusCode.NotFound;
                await context.Response.WriteAsync("404 Page Was Not Found :/");
            };
        }
    }
}
