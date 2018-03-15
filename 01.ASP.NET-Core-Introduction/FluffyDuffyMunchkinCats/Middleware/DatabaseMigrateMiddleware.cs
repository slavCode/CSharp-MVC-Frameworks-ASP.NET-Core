﻿namespace FluffyDuffyMunchkinCats.Middleware
{
    using Data;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    public class DatabaseMigrateMiddleware
    {
        private readonly RequestDelegate next;

        public DatabaseMigrateMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public Task Invoke(HttpContext context)
        {
            context.RequestServices.GetRequiredService<CatsDbContext>().Database.Migrate();

            return this.next(context);
        }
    }
}
