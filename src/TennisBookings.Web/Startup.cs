using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TennisBookings.Web.Core.DependencyInjection;
using TennisBookings.Web.Data;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using TennisBookings.Web.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using TennisBookings.Web.BackgroundServices;
using TennisBookings.Web.Core;

namespace TennisBookings.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            WebHostEnvironment = webHostEnvironment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment WebHostEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {           
            services.AddDbContext<TennisBookingDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<TennisBookingsUser>()
                .AddRoles<TennisBookingsRole>()
                .AddEntityFrameworkStores<TennisBookingDbContext>()
                .AddDefaultUI();

            services.AddScoped<IUserClaimsPrincipalFactory<TennisBookingsUser>, CustomClaimsPrincipalFactory>();

            services.AddControllersWithViews();

            services.AddRazorPages(options =>
            {
                options.Conventions.AuthorizePage("/FindAvailableCourts");
                options.Conventions.AuthorizePage("/BookCourt");
                options.Conventions.AuthorizePage("/Bookings");
            });

            services.Configure<HomePageConfiguration>(Configuration.GetSection("Features:HomePage"));

            services.TryAddEnumerable(ServiceDescriptor.Singleton<IValidateOptions<HomePageConfiguration>, HomePageConfigurationValidation>());
            services.TryAddEnumerable(ServiceDescriptor.Singleton<IValidateOptions<ExternalServicesConfig>, ExternalServicesConfigurationValidation>());
            services.AddHostedService<ValidateOptionsService>();

            services.Configure<GreetingConfiguration>(Configuration.GetSection("Features:Greeting"));
            services.Configure<WeatherForecastingConfiguration>(Configuration.GetSection("Features:WeatherForecasting"));
            services.Configure<ExternalServicesConfig>(ExternalServicesConfig.WeatherApi, Configuration.GetSection("ExternalServices:WeatherApi"));
            
            services.Configure<ScoreProcesingConfiguration>(Configuration.GetSection("ScoreProcessing"));

            services.Configure<ContentConfiguration>(Configuration.GetSection("Content"));
            services.AddSingleton<IContentConfiguration>(sp =>
                sp.GetRequiredService<IOptions<ContentConfiguration>>().Value);

            services.AddSingleton<IEmailService, EmailService>();

            services
                .AddAppConfiguration(Configuration)
                .AddBookingServices()
                .AddBookingRules()
                .AddCourtUnavailability()
                .AddMembershipServices()
                .AddStaffServices()
                .AddCourtServices()
                .AddWeatherForecasting(Configuration)
                .AddNotifications()
                .AddGreetings()
                .AddCaching()
                .AddTimeServices()
                .AddAuditing()
                .AddContentServices();
            
            if (Configuration.IsWeatherForecastEnabled())
                services.AddHostedService<WeatherCacheService>();            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
