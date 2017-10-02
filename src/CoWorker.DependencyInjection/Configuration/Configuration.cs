namespace CoWorker.DependencyInjection.Configuration
{
    using System.Linq;
    using Microsoft.Extensions.Configuration;
    using System;
    using Microsoft.Extensions.Primitives;
    using System.Collections.Generic;
    using Microsoft.Extensions.Options;
    using CoWorker.DependencyInjection.Abstractions;

    public class Configuration : IConfiguration
    {
        public IConfiguration _config;

        private readonly IEnumerable<IConfigureOptions<IConfigurationBuilder>> _configs;
        private readonly IConfigurationBuilder _builder;

        public Configuration(
            IConfigurationBuilder builder,
            IEnumerable<IConfigurationBuilderConfigureOptions> configs)
        {
            _builder = builder;
            _configs = configs;
            _configs.Aggregate(_builder,(seed,next) => next.Configure(seed));
            this._config = _builder.Build();
        }
        public String this[String key] { get => this._config[key]; set => this._config[key] = value; }

        public IEnumerable<IConfigurationSection> GetChildren() => this._config.GetChildren();
        public IChangeToken GetReloadToken() => this._config.GetReloadToken();
        public IConfigurationSection GetSection(String key) => this._config.GetSection(key);
    }
}