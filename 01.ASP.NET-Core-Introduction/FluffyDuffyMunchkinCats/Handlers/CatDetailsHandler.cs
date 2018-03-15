namespace FluffyDuffyMunchkinCats.Handlers
{
    using Infrastructure;
    using Microsoft.AspNetCore.Http;
    using System;
    using Data;
    using Microsoft.Extensions.DependencyInjection;

    public class CatDetailsHandler: IHandler
    {
        public int Order()
        {
            return 3;
        }

        public Func<HttpContext, bool> Condition()
        {
            return ctx => ctx.Request.Path.Value.StartsWith("/cat/")
                          && ctx.Request.Method == HttpMethod.Get;
        }

        public RequestDelegate RequestHandler()
        {
            return async (contex) =>
            {
                var urlParts = contex.Request.Path.Value.Split('/', StringSplitOptions.RemoveEmptyEntries);
                if (urlParts.Length < 2)
                {

                    contex.Response.Redirect("/");
                }
                else
                {

                    var catId = 0;
                    int.TryParse(urlParts[1], out catId);

                    if (catId == 0)
                    {
                        contex.Response.Redirect("/");
                        return;
                    }

                    var db = contex
                        .RequestServices
                        .GetRequiredService<CatsDbContext>();
                    using (db)
                    {
                        var cat = await db.Cats.FindAsync(catId);
                        if (cat == null)
                        {
                            contex.Response.Redirect("/");
                            return;
                        }

                        await contex.Response.WriteAsync($"<h1>{cat.Name}</h1>");
                        await contex.Response.WriteAsync($@"<img src=""{cat.ImageUrl}"" width=""400"">");
                        await contex.Response.WriteAsync($"<h2>Age: {cat.Age}</h2>");
                        await contex.Response.WriteAsync($"<h2>Breed: {cat.Breed}</h2>");
                    }
                }
            };
        }
    }
}
