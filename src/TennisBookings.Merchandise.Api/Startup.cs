using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimulatedCloudSdk;
using TennisBookings.Merchandise.Api.BackgroundServices;
using TennisBookings.Merchandise.Api.Data;
using TennisBookings.Merchandise.Api.Diagnostics;
using TennisBookings.Merchandise.Api.External.Database;
using TennisBookings.Merchandise.Api.External.Queue;
using TennisBookings.Merchandise.Api.Stock;
using TennisBookings.Metrics.Middleware;

namespace TennisBookings.Merchandise.Api
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
            services.AddControllers();
            services.AddHealthChecks();

            services.AddQueueClient();
            services.AddSingleton<ICloudQueue, CloudQueue>();
            services.AddDatabaseClient();
            services.AddSingleton<ICloudDatabase, CloudDatabase>();

            services.AddSingleton<IMetricRecorder, MetricRecorder>();
            services.AddSingleton<IProductDataRepository, CloudBasedProductDataRepository>();
            services.AddSingleton<IProductValidator, ProductValidator>();
            services.AddSingleton<ICategoryProvider, CategoryProvider>();
            services.AddSingleton<IStockCalculator, StockCalculator>();

            services.AddHostedService<SeedProductsService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRequestMetrics();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/healthcheck");
                endpoints.MapControllers();
            });
        }
    }
}
