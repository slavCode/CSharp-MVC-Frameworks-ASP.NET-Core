namespace LearningSystem.Web.Infrastructure.Extensions
{
    using Data;
    using Data.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System.Threading.Tasks;

    using static WebGlobulConstants;

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
                    .GetService<LearningSystemDbContext>();
                context.Database.Migrate();

                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
                var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();

                Task.Run(async () =>
                {
                    var roleNames = new[]
                    {
                        AdministratorRole,
                        AuthorRole,
                        TrainerRole
                    };

                    foreach (var roleName in roleNames)
                    {
                        var roleExists = await roleManager.RoleExistsAsync(roleName);

                        if (!roleExists)
                        {
                            await roleManager.CreateAsync(new IdentityRole
                            {
                                Name = roleName
                            });
                        }
                    }

                    var adminUser = await userManager.FindByNameAsync(AdminEmail);

                    if (adminUser == null)
                    {
                        adminUser = new User
                        {
                            Email = AdminEmail,
                            UserName = AdminUsername,
                            Name = AdminName
                        };

                        await userManager.CreateAsync(adminUser, "admin123");

                        await userManager.AddToRoleAsync(adminUser, AdministratorRole);
                    }
                }).Wait();
            }

            return app;
        }
    }
}
