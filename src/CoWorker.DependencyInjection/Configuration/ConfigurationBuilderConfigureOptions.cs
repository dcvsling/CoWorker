namespace CoWorker.DependencyInjection.Configuration
{
    using Microsoft.Extensions.Configuration;
    using System;
    using Microsoft.Extensions.Options;
    using CoWorker.DependencyInjection.Abstractions;

    public class ConfigurationBuilderConfigureOptions : IConfigurationBuilderConfigureOptions
    {
        private readonly Action<IConfigurationBuilder> _config;

        public ConfigurationBuilderConfigureOptions(Action<IConfigurationBuilder> config)
        {
            _config = config;
        }
        public void Configure(IConfigurationBuilder options)
            => _config(options);
    }
}