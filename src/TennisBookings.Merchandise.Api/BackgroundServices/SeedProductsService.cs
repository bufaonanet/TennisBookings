using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TennisBookings.Merchandise.Api.External.Database;

namespace TennisBookings.Merchandise.Api.BackgroundServices
{
    public class SeedProductsService : IHostedService
    {
        private readonly ICloudDatabase _cloudDatabase;
        private readonly ILogger<SeedProductsService> _logger;
        private readonly IHostEnvironment _environment;

        public SeedProductsService(ICloudDatabase cloudDatabase, ILogger<SeedProductsService> logger, IHostEnvironment environment)
        {
            _cloudDatabase = cloudDatabase;
            _logger = logger;
            _environment = environment;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            if (_environment.IsProduction())
                return; // don't add default products in production

            try
            {
                if (_cloudDatabase is CloudDatabase cloudDatabase)
                {
                    await cloudDatabase.InitialiseDefaultProducts();
                }
            }
            catch (Exception ex)
            {
                // log and allow the app continue running
                _logger.LogError(ex, "Failed to initialise the Product Data Repository.");                      
            }            
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            // Nothing to do here!
            return Task.CompletedTask;
        }
    }
}
