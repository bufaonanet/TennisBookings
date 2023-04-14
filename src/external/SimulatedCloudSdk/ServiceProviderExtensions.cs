using System;
using Microsoft.Extensions.DependencyInjection;
using SimulatedCloudSdk.Database;
using SimulatedCloudSdk.Queue;

namespace SimulatedCloudSdk
{
    public static class ServiceProviderExtensions
    {
        public static IServiceCollection AddQueueClient(this IServiceCollection services)
        {
            services.AddSingleton<IQueueClient, QueueClient>();
            return services;
        }

        public static IServiceCollection AddDatabaseClient(this IServiceCollection services)
        {
            services.AddSingleton(typeof(IDatabaseClient<>), typeof(DatabaseClient<>));
            return services;
        }
    }
}
