using System;
using KooliProjekt.Controllers;
using KooliProjekt.Data;
using KooliProjekt.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KooliProjekt.IntegrationTests.Helpers
{
    public class FakeStartup //: Startup
    {
        public FakeStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            // Use an in-memory database for tests instead of SQL Server
            var dbGuid = Guid.NewGuid().ToString();  // Unique name for in-memory DB
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseInMemoryDatabase(dbGuid);  // Use in-memory DB for isolation between tests
            });

            // Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITeamsService, TeamService>();
            services.AddScoped<ITournamentsService, TournamentService>();
            services.AddScoped<IPredictionService, PredictionsService>();
            services.AddScoped<IMatchesService, MatchService>();
            services.AddScoped<ILeaderboardService, LeaderboardService>();
            // Register identity services for authentication
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<ApplicationDbContext>();

            // Register controllers and views
            services.AddControllersWithViews()
                    .AddApplicationPart(typeof(HomeController).Assembly);

            // Add any other services required for the tests (e.g., logging, other services)
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure the middleware pipeline for testing
            app.UseStaticFiles();
            app.UseRouting();

            // Authentication and authorization
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}/{pathStr?}");
            });

            // Optional: Run any database initialization or seeding logic here for the test context
            var serviceScopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using (var serviceScope = serviceScopeFactory.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                if (dbContext == null)
                {
                    throw new NullReferenceException("Cannot get instance of dbContext");
                }

                // Ensure that the database is created and optionally seed it for tests
                dbContext.Database.EnsureCreated();
                // You can call a method to seed the database if necessary, but usually for integration tests, we use fresh data each time
            }
        }

        // You can optionally add seeding functionality, if needed, for the tests
        // private void EnsureDatabase(ApplicationDbContext dbContext)
        // {
        //     dbContext.Database.EnsureDeleted();
        //     dbContext.Database.EnsureCreated();
        //
        //     if (!dbContext.Users.Any())
        //     {
        //         SeedData.Initialize(dbContext);  // Optionally seed test data
        //     }
        // }
    }
}