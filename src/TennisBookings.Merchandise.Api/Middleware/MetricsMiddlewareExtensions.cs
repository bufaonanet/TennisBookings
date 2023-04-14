using Microsoft.AspNetCore.Builder;

namespace TennisBookings.Metrics.Middleware
{
    public static class MetricsMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestMetrics(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MetricsMiddleware>();
        }
    }
}
