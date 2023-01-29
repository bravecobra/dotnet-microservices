using System;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Winton.Extensions.Configuration.Consul;

namespace ConsulConfiguration
{
    public class ConfigurationConsulExtender
    {
        private readonly IConfigurationBuilder _configurationBuilder;
        private readonly IConfigurationRoot _configuration;

        public ConfigurationConsulExtender(IConfigurationBuilder configurationBuilder)
        {
            _configurationBuilder = configurationBuilder ?? throw new ArgumentNullException(nameof(configurationBuilder));
            _configuration = _configurationBuilder.Build();
        }

        public void AddConfigToConsul(string key)
        {
            _configurationBuilder.AddConsul(
                $"{_configuration["ASPNETCORE_ENVIRONMENT"]}/{_configuration["ServiceConfig:serviceName"]}/{key}",
                source => {
                    source.ConsulConfigurationOptions = config =>
                    {
                        config.Address = new Uri(_configuration["ServiceConfig:serviceDiscoveryAddress"]);
                        config.Datacenter = _configuration["ServiceConfig:DataCenter"];
                        //config.Token = "";
                    };
                    source.ReloadOnChange = true;
                    source.Optional = true;
                }
                
            );
        }
    }
}