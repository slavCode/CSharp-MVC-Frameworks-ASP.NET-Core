namespace FluffyDuffyMunchkinCats.Handlers
{
    using Data;
    using Infrastructure;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using System;

    public class AddCatHandler: IHandler
    {
        public int Order()
        {
            return 2;
        }

        public Func<HttpContext, bool> Condition()
        {
            return req => req.Request.Path.Value == "/cat/add";
        }

        public RequestDelegate RequestHandler()
        {
            return async context =>
            {
                if (context.Request.Method == HttpMethod.Get)
                {
                    context.Response.Redirect("/cats-add-form.html");
                }
                else if (context.Request.Method == HttpMethod.Post)
                {


                    var formData = context.Request.Form;

                    var db = context
                        .RequestServices
                        .GetRequiredService<CatsDbContext>();

                    using (db)
                    {
                        try
                        {
                            db.Cats.Add(new Cat
                            {
                                Name = formData["Name"],
                                Age = int.Parse(formData["Age"]),
                                Breed = formData["Breed"],
                                ImageUrl = formData["ImageUrl"]
                            });

                            await db.SaveChangesAsync();

                            context.Response.Redirect("/");
                        }
                        catch
                        {
                            await context.Response.WriteAsync("<h2>Invalid Cat Data!</h2>");
                            await context.Response.WriteAsync($@"<h2><a href=""/"">Back to Home</a></h2>");
                        }
                    }
                }
            };
        }
    }
}
