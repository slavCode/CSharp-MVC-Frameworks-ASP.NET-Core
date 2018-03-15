namespace FluffyDuffyMunchkinCats.Handlers
{
    using Data;
    using Infrastructure;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Linq;

    public class HomeHandler : IHandler
    {
        int IHandler.Order()
        {
            return 1;
        }

        Func<HttpContext, bool> IHandler.Condition()
        {
            return ctx => ctx.Request.Path.Value == "/"
                    && ctx.Request.Method == HttpMethod.Get;
        }

        RequestDelegate IHandler.RequestHandler()
        {
            return
             async (context) =>
             {
                 var env = context.RequestServices.GetRequiredService<IHostingEnvironment>();

                 await context.Response.WriteAsync($"<h1>{env.ApplicationName}</h1>");

                 var db = context.RequestServices.GetRequiredService<CatsDbContext>();

                 using (db)
                 {
                     var catsData = db
                         .Cats
                         .Select(c => new
                         {
                             c.Id,
                             c.Name
                         })
                         .ToList();

                     await context.Response.WriteAsync("<ul>");

                     foreach (var cat in catsData)
                     {
                         await context.Response.WriteAsync($@"<li><a href=""/cat/{cat.Id}"">{cat.Name}</a></li>");
                     }
                 }

                 await context.Response.WriteAsync("</ul>");

                 await context.Response.WriteAsync($@"<form action=""/cat/add"">
                                                                <input type=""submit"" value=""Add Cat""/>
                                                            </form>");
             };
        }
    }
}
