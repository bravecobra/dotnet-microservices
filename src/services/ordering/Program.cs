using System;
using System.Diagnostics;
using System.IO;
using ConsulConfiguration;
using Metrics;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace ordering
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Serilog.Debugging.SelfLog.Enable(Console.Error);
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                .AddJsonFile("appsettings.logs.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            // Lets make sure that if creating web host fails, we can log that error.
            var loggerConfiguration = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .Enrich.WithProperty("ApplicationName", typeof(Program).Assembly.GetName().Name);

#if DEBUG
            // Used to filter out potentially bad data due debugging.
            // Very useful when doing Seq dashboards and want to remove logs under debugging session.
            loggerConfiguration.Enrich.WithProperty("DebuggerAttached", Debugger.IsAttached);
#endif
            Log.Logger = loggerConfiguration.CreateLogger();
            try
            {
                // In some rare cases, creating web host can fail.
                // This style of logging increases the chances of logging this issue.
                Log.Logger.Information("Bootstrapping ordering app...");
                using var host = CreateWebHostBuilder(args).Build();
                host.Run();
            }
            catch (Exception e)
            {
                // Happens rarely but when it does, you'll thank me. :)
                Log.Logger.Fatal(e, "Unable to bootstrap ordering app.");
            }

            // Make sure all the log sinks have processed the last log before closing the application.
            Log.CloseAndFlush();
        }

        public static IHostBuilder CreateWebHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHost(builder =>
                {
                    builder.ConfigurePrometheusEndpoint();
                    builder.UseStartup<Startup>();
                })
                .ConfigureAppConfiguration((context, builder) =>
                {
                    builder.ConfigureConsulSettings();
                    builder.AddJsonFile("appsettings.local.json", optional: true);
                })
                .UseSerilog();
        }
    }
}
