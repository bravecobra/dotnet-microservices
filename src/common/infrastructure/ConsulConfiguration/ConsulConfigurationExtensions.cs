using System.Threading;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ConsulConfiguration
{
    public static class ConsulConfigurationExtensions
    {
        private static readonly CancellationTokenSource ConsulCancellationTokenSource = new CancellationTokenSource();

        public static void ConfigureConsulSettings(this IConfigurationBuilder configurationBuilder, string key = "configuration")
        {
            new ConfigurationConsulExtender(configurationBuilder).AddConfigToConsul(key, ConsulCancellationTokenSource);
        }

        public static IServiceCollection AddConsulConfiguration(this IServiceCollection services)
        {
            
            return services;
        }

        public static IApplicationBuilder UseConsulConfiguration(this IApplicationBuilder app, IApplicationLifetime lifetime)
        {
            lifetime.ApplicationStopping.Register(ConsulCancellationTokenSource.Cancel);
            return app;
        }
    }
}
