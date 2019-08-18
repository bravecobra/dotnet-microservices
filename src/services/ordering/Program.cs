using ConsulConfiguration;
using Metrics;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace ordering
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .ConfigurePrometheusEndpoint()
                .ConfigureAppConfiguration((context, builder) => builder.ConfigureConsulSettings())
                .UseStartup<Startup>();
        }
    }
}
