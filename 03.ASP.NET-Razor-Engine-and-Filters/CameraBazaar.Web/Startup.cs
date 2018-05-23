namespace CameraBazaar.Web
{
    using AutoMapper;
    using Data;
    using Data.Models;
    using Extensions;
    using Infrastructure.Filters;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Rewrite;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Services;
    using Services.Implementations;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CameraBazaarDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, IdentityRole>(options =>
                    {
                        options.Password.RequireDigit = false;
                        options.Password.RequireUppercase = false;
                        options.Password.RequireNonAlphanumeric = false;
                    })
                    .AddEntityFrameworkStores<CameraBazaarDbContext>()
                    .AddDefaultTokenProviders();

            services.AddAuthentication().AddFacebook(fo =>
            {
                fo.AppId = "1986966801553347";
                fo.AppSecret = "fa7a6c08ec1120d7ec4b8e938d3f6d54";
            });

            services.AddTransient<ICameraService, CameraService>();
            services.AddTransient<IUserService, UserService>();

            services.AddAutoMapper();

            services.AddMvc(options =>
                {
                    options.Filters.Add<ValidateAntiForgeryTokenAttribute>();
                    options.Filters.Add<LogAttribute>();
                    options.Filters.Add<TimerAttribute>();
                }
            );
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDatabaseMigration();

            var options = new RewriteOptions()
                .AddRedirectToHttps();

            app.UseRewriter(options);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvcWithDefaultRoute();
        }
    }
}
