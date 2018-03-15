namespace FluffyDuffyMunchkinCats.Handlers
{
    using Microsoft.AspNetCore.Http;
    using System;

    public interface IHandler
    {
        int Order();

        Func<HttpContext, bool> Condition();

        RequestDelegate RequestHandler();
    }
}
