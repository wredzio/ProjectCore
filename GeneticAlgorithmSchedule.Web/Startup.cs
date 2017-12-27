using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using GeneticAlgorithmSchedule.Web.ConfigureServices;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using GeneticAlgorithmSchedule.Database.Contexts.Schools;
using GeneticAlgorithmSchedule.Database.Models.Applications;
using GeneticAlgorithmSchedule.Database.Contexts.Applications;
using GeneticAlgorithmSchedule.Web.Middlewares;

namespace GeneticAlgorithmSchedule.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SchoolContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("ScheduleConnection"),
                b => b.MigrationsAssembly("GeneticAlgorithmSchedule.Database")));

            //services.AddDbContext<SchoolContext>(options => options.UseInMemoryDatabase());

            services.AddDbContext<IdentityContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection"),
                b => b.MigrationsAssembly("GeneticAlgorithmSchedule.Database")));

            //services.AddDbContext<IdentityContext>(options => options.UseInMemoryDatabase());

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 5;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.Cookie.Expiration = TimeSpan.FromDays(150);
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Login";
                options.SlidingExpiration = true;
            });

            services.AddMvc();
            services.AddAutoMapper();
            services.AddLogging();

            services.AddSchoolService();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, RoleManager<ApplicationRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStatusCodePagesWithReExecute("/error/{0}");
            app.UseStaticFiles();
            app.UseAuthentication();
            roleManager.SeedRoles().Wait();

            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseMiddleware(typeof(LoggerMiddleware));

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
