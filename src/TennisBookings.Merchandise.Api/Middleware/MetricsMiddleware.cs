using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using TennisBookings.Merchandise.Api.Diagnostics;

namespace TennisBookings.Metrics.Middleware
{
    public class MetricsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMetricRecorder _metricRecorder;
        private readonly ILogger<MetricsMiddleware> _logger;

        public MetricsMiddleware(RequestDelegate next, IMetricRecorder metricRecorder, ILogger<MetricsMiddleware> logger)
        {
            _next = next;
            _metricRecorder = metricRecorder;
            _logger = logger;
        }

        public object HttpRequestMillisecondsMetric { get; private set; }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var userAgent = "Unknown";
            if (httpContext.Request.Headers.TryGetValue("User-Agent", out var userAgentValue))
            {
                userAgent = userAgentValue.First();
            }

            var correlationId = "Not Set";
            if (httpContext.Request.Headers.TryGetValue("Correlation-Id", out var correlationIdValue))
            {
                correlationId = correlationIdValue.First();
            }

            using var loggingScope = _logger.BeginScope(new Dictionary<string, object>
            {              
                ["UserAgent"] = userAgent,
                ["CorrelationId"] = correlationId
            });
            
            try
            {
                await _next(httpContext);
            }          
            catch (Exception e)
            {
                _logger.LogError(e, "An unhandled exception was thrown by the application for request.");

                _metricRecorder.RecordMetric("unhandled-exception", 1, new[]
                {
                    $"correlation_id:{correlationId}",
                    $"user_agent:{userAgent}"
                });

                throw; // Let ASP.NET Core handle this
            }

            _metricRecorder.RecordMetric("sending-response", 1, new[] 
            { 
                $"status_code:{httpContext.Response.StatusCode}",
                $"user_agent:{userAgent}"
            });
        }
    }
}
