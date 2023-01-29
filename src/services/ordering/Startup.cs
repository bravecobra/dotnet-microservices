using System;
using Metrics;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ordering.Persistence;
using ordering.Persistence.Impl;

namespace ordering
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
//            services.AddConsulServices(Configuration.GetServiceConfig());
//            services.AddConsulConfiguration();
            services.AddMetricsServices();
            services.AddHealthChecks();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSingleton<IOrdersRepository, OrdersRepository>();
            services.AddMvc(options => options.EnableEndpointRouting = false)
                .AddMetrics();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();
            }

            //app.UseHttpsRedirection();
            //app.UseConsulConfiguration(lifetime);
            app.UseHealthChecks("/health");
            app.UseMetricsServices();
            app.UseMvc();
        }
    }
}
