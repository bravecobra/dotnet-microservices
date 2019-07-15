using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using App.Metrics;
using App.Metrics.AspNetCore;
using App.Metrics.Formatters.Prometheus;
using Microsoft.AspNetCore.Hosting;

namespace Metrics
{
    public static class MetricsWebHostBuilderExtensions
    {
        public static IMetricsRoot Metrics { get; set; }

        public static IWebHostBuilder ConfigurePrometheusEndpoint(this IWebHostBuilder builder)
        {
            Metrics = AppMetrics.CreateDefaultBuilder()
                .OutputMetrics.AsPrometheusPlainText()
                //.OutputMetrics.AsPrometheusProtobuf()
                .Build();
            return builder.ConfigureMetrics(Metrics)
                .UseMetrics(
                    options =>
                    {
                        options.EndpointOptions = endpointsOptions =>
                        {
                            endpointsOptions.MetricsTextEndpointOutputFormatter = Metrics.OutputMetricsFormatters
                                .OfType<MetricsPrometheusTextOutputFormatter>().First();
                            //endpointsOptions.MetricsEndpointOutputFormatter = Metrics.OutputMetricsFormatters.OfType<MetricsPrometheusProtobufOutputFormatter>().First();
                        };
                    });
        }
    }
}
