using System;
using App.Metrics;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Metrics
{
    public static class MetricsExtensions
    {
        public static IServiceCollection AddMetricsServices(this IServiceCollection services)
        {
            var metrics = AppMetrics.CreateDefaultBuilder()
                .Build();
            services.AddMetrics(metrics);
            services.AddMetricsTrackingMiddleware();
            return services.AddMetricsReportingHostedService();
        }

        public static IApplicationBuilder UseMetricsServices(this IApplicationBuilder app)
        {
            return app.UseMetricsAllMiddleware();
        }
    }
}
