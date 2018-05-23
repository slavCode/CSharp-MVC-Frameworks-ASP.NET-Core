namespace CameraBazaar.Web.Extensions
{
    using Data;
    using Data.Models;
    using Infrastructure;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System.Threading.Tasks;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder app)
        {
            using (var serviceScope = app
                .ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                var context = serviceScope
                    .ServiceProvider
                    .GetService<CameraBazaarDbContext>();
                context.Database.Migrate();

                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
                var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();

                Task.Run(async () =>
                {
                    var roleName = GlobalConstants.AdministratorRole;

                    var roleExists = await roleManager.RoleExistsAsync(roleName);

                    if (!roleExists)
                    {
                        await roleManager.CreateAsync(new IdentityRole
                        {
                            Name = roleName
                        });
                    }

                    var adminUser = new User
                    {
                        Email = "admin@admin.bg",
                        UserName = "admin@admin.bg"
                    };

                    await userManager.CreateAsync(adminUser, "admin123");

                    await userManager.AddToRoleAsync(adminUser, roleName);

                }).Wait();
            }

            return app;
        }
    }
}
